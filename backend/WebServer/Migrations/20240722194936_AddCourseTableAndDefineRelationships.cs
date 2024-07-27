    using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangLearner.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseTableAndDefineRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    ReportsCount = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    TargetLanguageName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false),
                    LikesCount = table.Column<int>(type: "int", nullable: false),
                    DislikesCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Languages_TargetLanguageName",
                        column: x => x.TargetLanguageName,
                        principalTable: "Languages",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseUsers",
                columns: table => new
                {
                    EnrolledCoursesId = table.Column<int>(type: "int", nullable: false),
                    EnrolledUsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseUsers", x => new { x.EnrolledCoursesId, x.EnrolledUsersId });
                    table.ForeignKey(
                        name: "FK_CourseUsers_Courses_EnrolledCoursesId",
                        column: x => x.EnrolledCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseUsers_Users_EnrolledUsersId",
                        column: x => x.EnrolledUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CreatorId",
                table: "Courses",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TargetLanguageName",
                table: "Courses",
                column: "TargetLanguageName");

            migrationBuilder.CreateIndex(
                name: "IX_CourseUsers_EnrolledUsersId",
                table: "CourseUsers",
                column: "EnrolledUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseUsers");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
