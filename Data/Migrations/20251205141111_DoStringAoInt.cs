using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcEcocycle.Data.Migrations
{
    /// <inheritdoc />
    public partial class DoStringAoInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Colaboradores_ColaboradoresId1",
                table: "Movimentacoes");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacoes_ColaboradoresId1",
                table: "Movimentacoes");

            migrationBuilder.DropColumn(
                name: "ColaboradoresId1",
                table: "Movimentacoes");

            migrationBuilder.AlterColumn<int>(
                name: "ColaboradoresId",
                table: "Movimentacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ColaboradoresId",
                table: "Movimentacoes",
                column: "ColaboradoresId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Colaboradores_ColaboradoresId",
                table: "Movimentacoes",
                column: "ColaboradoresId",
                principalTable: "Colaboradores",
                principalColumn: "ColaboradoresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimentacoes_Colaboradores_ColaboradoresId",
                table: "Movimentacoes");

            migrationBuilder.DropIndex(
                name: "IX_Movimentacoes_ColaboradoresId",
                table: "Movimentacoes");

            migrationBuilder.AlterColumn<string>(
                name: "ColaboradoresId",
                table: "Movimentacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ColaboradoresId1",
                table: "Movimentacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacoes_ColaboradoresId1",
                table: "Movimentacoes",
                column: "ColaboradoresId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimentacoes_Colaboradores_ColaboradoresId1",
                table: "Movimentacoes",
                column: "ColaboradoresId1",
                principalTable: "Colaboradores",
                principalColumn: "ColaboradoresId");
        }
    }
}
