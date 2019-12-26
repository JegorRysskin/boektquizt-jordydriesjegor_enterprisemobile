using Microsoft.EntityFrameworkCore.Migrations;

namespace BoektQuiz.Migrations
{
    public partial class AddedAnswersAndTeams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: 1);

            /*migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");*/

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(nullable: false),
                    AnswerString = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new { x.Id, x.TeamId });
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rounds",
                columns: new[] { "Id", "Text" },
                values: new object[] { -1, "Ronde 0" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "TeamName" },
                values: new object[] { -1, "Dummy Team" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -1, -1, "Vraag 1" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -2, -1, "Vraag 2" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -3, -1, "Vraag 3" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -4, -1, "Vraag 4" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -5, -1, "Vraag 5" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -6, -1, "Vraag 6" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -7, -1, "Vraag 7" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -8, -1, "Vraag 8" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -9, -1, "Vraag 9" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "RoundId", "Text" },
                values: new object[] { -10, -1, "Vraag 10" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -1, -1, "Dummy Antwoord 1", -1 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -2, -1, "Dummy Antwoord 2", -2 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -3, -1, "Dummy Antwoord 3", -3 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -4, -1, "Dummy Antwoord 4", -4 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -5, -1, "Dummy Antwoord 5", -5 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -6, -1, "Dummy Antwoord 6", -6 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -7, -1, "Dummy Antwoord 7", -7 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -8, -1, "Dummy Antwoord 8", -8 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -9, -1, "Dummy Antwoord 9", -9 });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "TeamId", "AnswerString", "QuestionId" },
                values: new object[] { -10, -1, "Dummy Antwoord 10", -10 });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_TeamId",
                table: "Answers",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.DeleteData(
                table: "Rounds",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Questions",
                type: "TEXT",
                nullable: true);

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
        }
    }
}
