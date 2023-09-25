using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Danweyne_Real_estate.Data.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyFeatures");

            migrationBuilder.DropColumn(
                name: "FeaturesName",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserImageUrl",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Ceilings",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DoubleSinks",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmergencyExit",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FirePlace",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HurricaneShutters",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "JogPath",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LaundryRoom",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Stories",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SwimmingPool",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Features_PropertyId",
                table: "Features",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_properties_PropertyId",
                table: "Features",
                column: "PropertyId",
                principalTable: "properties",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_properties_PropertyId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_PropertyId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Ceilings",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "DoubleSinks",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "EmergencyExit",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "FirePlace",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "HurricaneShutters",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "JogPath",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "LaundryRoom",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Stories",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "SwimmingPool",
                table: "Features");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesName",
                table: "Features",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PropertyFeatures",
                columns: table => new
                {
                    PropertyFeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeaturesId = table.Column<int>(type: "int", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyFeatures", x => x.PropertyFeatureId);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_Features_FeaturesId",
                        column: x => x.FeaturesId,
                        principalTable: "Features",
                        principalColumn: "FeaturesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyFeatures_properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_FeaturesId",
                table: "PropertyFeatures",
                column: "FeaturesId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyFeatures_PropertyId",
                table: "PropertyFeatures",
                column: "PropertyId");
        }
    }
}
