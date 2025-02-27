using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContainRs.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class LocalizacaoPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solicitacoes_Endereco_EnderecoId",
                table: "Solicitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Solicitacoes_EnderecoId",
                table: "Solicitacoes");

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Solicitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Solicitacoes",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Solicitacoes",
                type: "decimal(18,6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Referencias",
                table: "Solicitacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Conteineres",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "LocacaoId",
                table: "Conteineres",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "Referencias",
                table: "Solicitacoes");

            migrationBuilder.DropColumn(
                name: "LocacaoId",
                table: "Conteineres");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Conteineres",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Solicitacoes_EnderecoId",
                table: "Solicitacoes",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Solicitacoes_Endereco_EnderecoId",
                table: "Solicitacoes",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
