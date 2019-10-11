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
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "University",
                columns: table => new
                {
                    UniversityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_University", x => x.UniversityId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Topic",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topic", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topic_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Semester = table.Column<int>(nullable: false),
                    UniversityId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_Person_University_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "University",
                        principalColumn: "UniversityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Person_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Student_Person_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    TutorId = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.TutorId);
                    table.ForeignKey(
                        name: "FK_Tutor_Person_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Person",
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
                        name: "FK_StudentCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourse_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new
                {
                    AvailabilityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TutorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availability", x => x.AvailabilityId);
                    table.ForeignKey(
                        name: "FK_Availability_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
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
                        name: "FK_TutorCourse_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorCourse_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutoringOffer",
                columns: table => new
                {
                    TutoringOfferId = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_TutoringOffer", x => x.TutoringOfferId);
                    table.ForeignKey(
                        name: "FK_TutoringOffer_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutoringOffer_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutoringSession",
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
                    table.PrimaryKey("PK_TutoringSession", x => x.TutoringSessionId);
                    table.ForeignKey(
                        name: "FK_TutoringSession_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TutoringSession_Tutor_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Tutor",
                        principalColumn: "TutorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvailabilityDay",
                columns: table => new
                {
                    AvailabilityDayId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<string>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    AvailabilityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailabilityDay", x => x.AvailabilityDayId);
                    table.ForeignKey(
                        name: "FK_AvailabilityDay_Availability_AvailabilityId",
                        column: x => x.AvailabilityId,
                        principalTable: "Availability",
                        principalColumn: "AvailabilityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
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
                    table.PrimaryKey("PK_Qualification", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_Qualification_Person_AdresseeId",
                        column: x => x.AdresseeId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Qualification_Person_AdresserId",
                        column: x => x.AdresserId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Qualification_TutoringSession_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "TutoringSession",
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
                        name: "FK_StudentTutoringSession_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentTutoringSession_TutoringSession_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "TutoringSession",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicTutoringOffer",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false),
                    TutoringOfferId = table.Column<int>(nullable: false),
                    TutoringSessionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTutoringOffer", x => new { x.TopicId, x.TutoringOfferId });
                    table.ForeignKey(
                        name: "FK_TopicTutoringOffer_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTutoringOffer_TutoringOffer_TutoringOfferId",
                        column: x => x.TutoringOfferId,
                        principalTable: "TutoringOffer",
                        principalColumn: "TutoringOfferId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTutoringOffer_TutoringSession_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "TutoringSession",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicTutoringSession",
                columns: table => new
                {
                    TopicId = table.Column<int>(nullable: false),
                    TutoringSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicTutoringSession", x => new { x.TopicId, x.TutoringSessionId });
                    table.ForeignKey(
                        name: "FK_TopicTutoringSession_Topic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topic",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicTutoringSession_TutoringSession_TutoringSessionId",
                        column: x => x.TutoringSessionId,
                        principalTable: "TutoringSession",
                        principalColumn: "TutoringSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_TutorId",
                table: "Availability",
                column: "TutorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AvailabilityDay_AvailabilityId",
                table: "AvailabilityDay",
                column: "AvailabilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UniversityId",
                table: "Person",
                column: "UniversityId");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                table: "Person",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_AdresseeId",
                table: "Qualification",
                column: "AdresseeId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_AdresserId",
                table: "Qualification",
                column: "AdresserId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_TutoringSessionId",
                table: "Qualification",
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
                name: "IX_Topic_CourseId",
                table: "Topic",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTutoringOffer_TutoringOfferId",
                table: "TopicTutoringOffer",
                column: "TutoringOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTutoringOffer_TutoringSessionId",
                table: "TopicTutoringOffer",
                column: "TutoringSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicTutoringSession_TutoringSessionId",
                table: "TopicTutoringSession",
                column: "TutoringSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorCourse_CourseId",
                table: "TutorCourse",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringOffer_CourseId",
                table: "TutoringOffer",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringOffer_TutorId",
                table: "TutoringOffer",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSession_CourseId",
                table: "TutoringSession",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TutoringSession_TutorId",
                table: "TutoringSession",
                column: "TutorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailabilityDay");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "StudentCourse");

            migrationBuilder.DropTable(
                name: "StudentTutoringSession");

            migrationBuilder.DropTable(
                name: "TopicTutoringOffer");

            migrationBuilder.DropTable(
                name: "TopicTutoringSession");

            migrationBuilder.DropTable(
                name: "TutorCourse");

            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TutoringOffer");

            migrationBuilder.DropTable(
                name: "Topic");

            migrationBuilder.DropTable(
                name: "TutoringSession");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "University");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
