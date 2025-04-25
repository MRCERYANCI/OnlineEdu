using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEdu.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class create_table_coursevideo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseVideos",
                columns: table => new
                {
                    CourseVideoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    VideoNumber = table.Column<int>(type: "int", nullable: false),
                    Thumbnails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseVideos", x => x.CourseVideoId);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseVideo");

            migrationBuilder.DropTable(
                name: "CourseVideos");
        }
    }
}
