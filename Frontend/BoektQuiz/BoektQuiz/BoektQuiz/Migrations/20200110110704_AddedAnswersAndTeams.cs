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

            //SQLite doesn't support the DropColumn operation

            /*migrationBuilder.DropColumn(
                name: "Text",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Questions");*/

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Rounds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rounds",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionString",
                table: "Questions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Scores = table.Column<double>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false)
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
                    QuestionId = table.Column<int>(nullable: false),
                    AnswerString = table.Column<string>(nullable: true),
                    TeamId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => new { x.Id, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_Answers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -1, -1, "Dummy Antwoord 1", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -2, -2, "Dummy Antwoord 2", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -3, -3, "Dummy Antwoord 3", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -4, -4, "Dummy Antwoord 4", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -5, -5, "Dummy Antwoord 5", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -6, -6, "Dummy Antwoord 6", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -7, -7, "Dummy Antwoord 7", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -8, -8, "Dummy Antwoord 8", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -9, -9, "Dummy Antwoord 9", null });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "QuestionId", "AnswerString", "TeamId" },
                values: new object[] { -10, -10, "Dummy Antwoord 10", null });

            migrationBuilder.InsertData(
                table: "Rounds",
                columns: new[] { "Id", "Enabled", "Name" },
                values: new object[] { -1, false, "Ronde 0" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Enabled", "Name", "Scores" },
                values: new object[] { -1, true, "Dummy Team", 0.0 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -1, "Vraag 1", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -2, "Vraag 2", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -3, "Vraag 3", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -4, "Vraag 4", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -5, "Vraag 5", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -6, "Vraag 6", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -7, "Vraag 7", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -8, "Vraag 8", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -9, "Vraag 9", -1 });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "QuestionString", "RoundId" },
                values: new object[] { -10, "Vraag 10", -1 });

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

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rounds");

            migrationBuilder.DropColumn(
                name: "QuestionString",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Rounds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Questions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
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
