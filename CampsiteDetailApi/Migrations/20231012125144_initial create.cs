using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CampsiteDetailApi.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            
            migrationBuilder.CreateTable(
                name: "CampsiteDetail",
                schema: "dbo",
                columns: table => new
                {
                    CampsiteDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampsiteId = table.Column<long>(type: "bigint", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateTimeCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateTimeUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampsiteDetail", x => x.CampsiteDetailId);
                    table.ForeignKey(
                        name: "FK_CampsiteDetail_Campsites_CampsiteId",
                        column: x => x.CampsiteId,
                        principalSchema: "dbo",
                        principalTable: "Campsites",
                        principalColumn: "CampsiteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampsiteDetail_CampsiteId",
                schema: "dbo",
                table: "CampsiteDetail",
                column: "CampsiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampsiteDetail",
                schema: "dbo");
        }
    }
}
