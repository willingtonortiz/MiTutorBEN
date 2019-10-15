using System;
using Microsoft.EntityFrameworkCore;
using MiTutorBEN.Models;

namespace MiTutorBEN.Data
{
    public class MiTutorContext : DbContext
    {
        public DbSet<AvailabilityDay> AvailabilityDays { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<TutoringOffer> TutoringOffers { get; set; }

        internal University asnotracking()
        {
            throw new NotImplementedException();
        }

        public DbSet<TutoringSession> TutoringSessions { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<User> Users { get; set; }

        public MiTutorContext(DbContextOptions<MiTutorContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Qualification-Person relationship
            modelBuilder.Entity<Qualification>()
                .HasOne(q => q.Adresser)
                .WithMany(a => a.QualificationsReceived).OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(q => q.AdresserId).IsRequired();

            modelBuilder.Entity<Qualification>()
                .HasOne(q => q.Adressee)
                .WithMany(a => a.QualificationsGiven).OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(q => q.AdresseeId).IsRequired();


            //Many to many relationshp	
            modelBuilder.Entity<StudentCourse>()
                .HasKey(ss => new { ss.StudentId, ss.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne<Student>(ss => ss.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(ss => ss.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne<Course>(ss => ss.Course)
                .WithMany(sub => sub.StudentCourses)
                .HasForeignKey(ss => ss.CourseId);


            modelBuilder.Entity<TutorCourse>()
                .HasKey(ts => new { ts.TutorId, ts.CourseId });
            modelBuilder.Entity<TutorCourse>()
                .HasOne<Tutor>(ts => ts.Tutor)
                .WithMany(t => t.TutorCourses)
                .HasForeignKey(ts => ts.TutorId);
            modelBuilder.Entity<TutorCourse>()
                .HasOne<Course>(ts => ts.Course)
                .WithMany(t => t.TutorCourses)
                .HasForeignKey(ts => ts.CourseId);


            modelBuilder.Entity<StudentTutoringSession>().HasKey(sts => new { sts.StudentId, sts.TutoringSessionId });
            modelBuilder.Entity<StudentTutoringSession>()
                .HasOne<Student>(sts => sts.Student)
                .WithMany(s => s.StudentTutoringSessions)
                .HasForeignKey(sts => sts.StudentId);
            modelBuilder.Entity<StudentTutoringSession>()
                .HasOne<TutoringSession>(sts => sts.TutoringSession)
                .WithMany(ts => ts.StudentTutoringSessions)
                .HasForeignKey(sts => sts.TutoringSessionId);


            modelBuilder.Entity<TopicTutoringOffer>()
                .HasKey(tto => new { tto.TopicId, tto.TutoringOfferId });
            modelBuilder.Entity<TopicTutoringOffer>()
                .HasOne<Topic>(tto => tto.Topic)
                .WithMany(t => t.TopicTutoringOffers)
                .HasForeignKey(tto => tto.TopicId);
            modelBuilder.Entity<TopicTutoringOffer>()
                .HasOne<TutoringOffer>(tto => tto.TutoringOffer)
                .WithMany(t => t.TopicTutoringOffers)
                .HasForeignKey(tto => tto.TutoringOfferId);


            modelBuilder.Entity<TopicTutoringSession>()
                .HasKey(tts => new { tts.TopicId, tts.TutoringSessionId });
            modelBuilder.Entity<TopicTutoringSession>()
                .HasOne<Topic>(tts => tts.Topic)
                .WithMany(t => t.TopicTutoringSessions)
                .HasForeignKey(tts => tts.TopicId);
            modelBuilder.Entity<TopicTutoringSession>()
                .HasOne<TutoringSession>(tts => tts.TutoringSession)
                .WithMany(t => t.TopicTutoringSessions)
                .HasForeignKey(tts => tts.TutoringSessionId);


            modelBuilder.Entity<Suscription>()
            .HasKey(s => new { s.PersonId, s.PlanId });
            modelBuilder.Entity<Suscription>()
                .HasOne<Person>(s => s.Person)
                .WithMany(p => p.Suscriptions)
                .HasForeignKey(p => p.PersonId);
            modelBuilder.Entity<Suscription>()
                .HasOne<Plan>(s => s.Plan)
                .WithMany(p => p.Suscriptions)
                .HasForeignKey(s => s.PlanId);


            modelBuilder.Entity<Course>()
                .ToTable("courses");

            modelBuilder.Entity<Topic>()
                .ToTable("topics");

            modelBuilder.Entity<AvailabilityDay>()
                .ToTable("availabilitydays");

            modelBuilder.Entity<Person>()
                .ToTable("people");

            modelBuilder.Entity<Qualification>()
                .ToTable("qualifications");

            modelBuilder.Entity<Tutor>()
                .ToTable("tutors");

            modelBuilder.Entity<Student>()
                .ToTable("students");

            modelBuilder.Entity<TutoringOffer>()
                .ToTable("tutoringoffers");

            modelBuilder.Entity<TutoringSession>()
                .ToTable("tutoringsessions");

            modelBuilder.Entity<University>()
                .ToTable("universities");

            modelBuilder.Entity<User>()
                .ToTable("users");

			modelBuilder.Entity<Plan>()
                .ToTable("plans");

			modelBuilder.Entity<Suscription>()
                .ToTable("suscriptions");

        }
    }
}