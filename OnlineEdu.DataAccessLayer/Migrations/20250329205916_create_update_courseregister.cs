﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineEdu.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class create_update_courseregister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseRegisters",
                columns: table => new
                {
                    CourseRegisterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRegisters", x => x.CourseRegisterId);
                    table.ForeignKey(
                        name: "FK_CourseRegisters_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRegisters_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AppUserId",
                table: "Courses",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegisters_AppUserId",
                table: "CourseRegisters",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRegisters_CourseId",
                table: "CourseRegisters",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserId",
                table: "Courses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AppUserId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseRegisters");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AppUserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Courses");
        }
    }
}
