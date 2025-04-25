using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEdu.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class update_coursevideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseVideo");

            migrationBuilder.CreateIndex(
                name: "IX_CourseVideos_CourseId",
                table: "CourseVideos",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseVideos_Courses_CourseId",
                table: "CourseVideos");

            migrationBuilder.DropIndex(
                name: "IX_CourseVideos_CourseId",
                table: "CourseVideos");

            migrationBuilder.CreateTable(
                name: "CourseCourseVideo",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CourseVideosCourseVideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseVideo", x => new { x.CourseId, x.CourseVideosCourseVideoId });
                    table.ForeignKey(
                        name: "FK_CourseCourseVideo_CourseVideos_CourseVideosCourseVideoId",
                        column: x => x.CourseVideosCourseVideoId,
                        principalTable: "CourseVideos",
                        principalColumn: "CourseVideoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseVideo_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseVideo_CourseVideosCourseVideoId",
                table: "CourseCourseVideo",
                column: "CourseVideosCourseVideoId");
        }
    }
}
