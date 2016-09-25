using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalJournals.Entities.Migrations
{
    public partial class JournalImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OverviewThumbnailPath",
                table: "Journals");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Journals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Journals");

            migrationBuilder.AddColumn<string>(
                name: "OverviewThumbnailPath",
                table: "Journals",
                nullable: true);
        }
    }
}
