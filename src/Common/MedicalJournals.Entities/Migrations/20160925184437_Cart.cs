using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MedicalJournals.Entities.Migrations
{
    public partial class Cart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Subscriptions");

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CartId = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    JournalId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_CartItems_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDetails",
                columns: table => new
                {
                    SubscriptionDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JournalId = table.Column<Guid>(nullable: false),
                    SubscriptionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDetails", x => x.SubscriptionDetailId);
                    table.ForeignKey(
                        name: "FK_SubscriptionDetails_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "JournalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubscriptionDetails_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "SubscriptionId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.AddColumn<bool>(
                name: "HasExpired",
                table: "Subscriptions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Subscriptions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Journals",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_JournalId",
                table: "CartItems",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDetails_JournalId",
                table: "SubscriptionDetails",
                column: "JournalId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDetails_SubscriptionId",
                table: "SubscriptionDetails",
                column: "SubscriptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasExpired",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Journals");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "SubscriptionDetails");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Subscriptions",
                nullable: false,
                defaultValue: false);
        }
    }
}
