using Microsoft.EntityFrameworkCore.Migrations;

namespace BoektQuiz.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoundId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Rounds_RoundId",
                        column: x => x.RoundId,
                        principalTable: "Rounds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rounds",
                columns: new[] { "Id", "Text" },
                values: new object[] { 1, "Ronde 0" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 1, "Dummy Antwoord 1", 1, "Vraag 1" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 2, "Dummy Antwoord 2", 1, "Vraag 2" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 3, "Dummy Antwoord 3", 1, "Vraag 3" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 4, "Dummy Antwoord 4", 1, "Vraag 4" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 5, "Dummy Antwoord 5", 1, "Vraag 5" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 6, "Dummy Antwoord 6", 1, "Vraag 6" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 7, "Dummy Antwoord 7", 1, "Vraag 7" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 8, "Dummy Antwoord 8", 1, "Vraag 8" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 9, "Dummy Antwoord 9", 1, "Vraag 9" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Answer", "RoundId", "Text" },
                values: new object[] { 10, "Dummy Antwoord 10", 1, "Vraag 10" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_RoundId",
                table: "Questions",
                column: "RoundId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Rounds");
        }
    }
}
