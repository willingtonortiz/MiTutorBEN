using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MiTutorBEN.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "plans",
                columns: table => new
                {
                    PlanId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plans", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "universities",
                columns: table => new
                {
                    UniversityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_universities", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    UniversityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_courses_universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "people",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Semester = table.Column<int>(nullable: false),
                    UniversityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_people", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_people_universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topics",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topics", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_topics_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    Points = table.Column<double>(nullable: false),
                    QualificationCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_students_people_StudentId",
                        column: x => x.StudentId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "suscriptions",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    PlanId = table.Column<int>(nullable: false),
                    SuscriptionId = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_suscriptions", x => new { x.PersonId, x.PlanId });
                    table.UniqueConstraint("AK_suscriptions_SuscriptionId", x => x.SuscriptionId);
                    table.ForeignKey(
                        name: "FK_suscriptions_people_PersonId",
                        column: x => x.PersonId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_suscriptions_plans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutors",
                columns: table => new
                {
                    TutorId = table.Column<int>(nullable: false),
                    QualificationCount = table.Column<int>(nullable: false),
                    Points = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutors", x => x.TutorId);
                    table.ForeignKey(
                        name: "FK_tutors_people_TutorId",
                        column: x => x.TutorId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_users_people_UserId",
                        column: x => x.UserId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourse",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourse", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentCourse_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "availabilitydays",
                columns: table => new
                {
                    AvailabilityDayId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    TutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_availabilitydays", x => x.AvailabilityDayId);
                    table.ForeignKey(
                        name: "FK_availabilitydays_tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "tutors",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorCourse",
                columns: table => new
                {
                    TutorId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorCourse", x => new { x.TutorId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_TutorCourse_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorCourse_tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "tutors",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tutoringoffers",
                columns: table => new
                {
                    TutoringOfferId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    UniversityId = table.Column<int>(nullable: true),
                    CourseId = table.Column<int>(nullable: true),
                    TutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutoringoffers", x => x.TutoringOfferId);
                    table.ForeignKey(
                        name: "FK_tutoringoffers_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tutoringoffers_tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "tutors",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tutoringoffers_universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "universities",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tutoringsessions",
                columns: table => new
                {
                    TutoringSessionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Place = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: true),
                    TutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tutoringsessions", x => x.TutoringSessionId);
                    table.ForeignKey(
                        name: "FK_tutoringsessions_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tutoringsessions_tutors_TutorId",
                        column: x => x.TutorId,
                        principalTable: "tutors",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicTutoringOffer",
                columns: table => new
                {
                    TutoringOfferId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTutoringOffer", x => new { x.TopicId, x.TutoringOfferId });
                    table.ForeignKey(
                        name: "FK_TopicTutoringOffer_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTutoringOffer_tutoringoffers_TutoringOfferId",
                        column: x => x.TutoringOfferId,
                        principalTable: "tutoringoffers",
                        principalColumn: "TutoringOfferId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "qualifications",
                columns: table => new
                {
                    QualificationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rate = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    AdresserRole = table.Column<string>(nullable: true),
                    AdresserId = table.Column<int>(nullable: false),
                    AdresseeId = table.Column<int>(nullable: false),
                    TutoringSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualifications", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_qualifications_people_AdresseeId",
                        column: x => x.AdresseeId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_qualifications_people_AdresserId",
                        column: x => x.AdresserId,
                        principalTable: "people",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_qualifications_tutoringsessions_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "tutoringsessions",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentTutoringSession",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    TutoringSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTutoringSession", x => new { x.StudentId, x.TutoringSessionId });
                    table.ForeignKey(
                        name: "FK_StudentTutoringSession_students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTutoringSession_tutoringsessions_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "tutoringsessions",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicTutoringSession",
                columns: table => new
                {
                    TutoringSessionId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTutoringSession", x => new { x.TopicId, x.TutoringSessionId });
                    table.ForeignKey(
                        name: "FK_TopicTutoringSession_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTutoringSession_tutoringsessions_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "tutoringsessions",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_availabilitydays_TutorId",
                table: "availabilitydays",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_UniversityId",
                table: "courses",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_people_UniversityId",
                table: "people",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_qualifications_AdresseeId",
                table: "qualifications",
                column: "AdresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_qualifications_AdresserId",
                table: "qualifications",
                column: "AdresserId");

            migrationBuilder.CreateIndex(
                name: "IX_qualifications_TutoringSessionId",
                table: "qualifications",
                column: "TutoringSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourse_CourseId",
                table: "StudentCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTutoringSession_TutoringSessionId",
                table: "StudentTutoringSession",
                column: "TutoringSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_suscriptions_PlanId",
                table: "suscriptions",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_topics_CourseId",
                table: "topics",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTutoringOffer_TutoringOfferId",
                table: "TopicTutoringOffer",
                column: "TutoringOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTutoringSession_TutoringSessionId",
                table: "TopicTutoringSession",
                column: "TutoringSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorCourse_CourseId",
                table: "TutorCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tutoringoffers_CourseId",
                table: "tutoringoffers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tutoringoffers_TutorId",
                table: "tutoringoffers",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_tutoringoffers_UniversityId",
                table: "tutoringoffers",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_tutoringsessions_CourseId",
                table: "tutoringsessions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_tutoringsessions_TutorId",
                table: "tutoringsessions",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "availabilitydays");

            migrationBuilder.DropTable(
                name: "qualifications");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentTutoringSession");

            migrationBuilder.DropTable(
                name: "suscriptions");

            migrationBuilder.DropTable(
                name: "TopicTutoringOffer");

            migrationBuilder.DropTable(
                name: "TopicTutoringSession");

            migrationBuilder.DropTable(
                name: "TutorCourse");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "plans");

            migrationBuilder.DropTable(
                name: "tutoringoffers");

            migrationBuilder.DropTable(
                name: "topics");

            migrationBuilder.DropTable(
                name: "tutoringsessions");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "tutors");

            migrationBuilder.DropTable(
                name: "people");

            migrationBuilder.DropTable(
                name: "universities");
        }
    }
}
