using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090411)]
    public class Migration_011_InsertEmpresas : Migration {
        public override void Up() {
            Insert.IntoTable("TBL_EMPRESAS").Row(new {
                DS_RAZAO_SOCIAL = "Centro Educacional Monte das Oliveiras",
                DS_NOME_FANTASIA = "Colégio Metanoia",
                NR_CNPJ = "12.345.678/0001-90",
                DS_SIGLA = "CM",
                X_ATIVO = true,
                DT_CADASTRO = DateTime.Now
            });

            Insert.IntoTable("TBL_EMPRESAS").Row(new {
                DS_RAZAO_SOCIAL = "Biblioteca Regional Monte das Oliveiras",
                DS_NOME_FANTASIA = "Biblioteca Yeshivá",
                NR_CNPJ = "12.345.678/0002-71",
                DS_SIGLA = "BY",
                X_ATIVO = true,
                DT_CADASTRO = DateTime.Now
            });
        }

        public override void Down() {
            Delete.FromTable("TBL_EMPRESAS").
                Row(new { NR_CNPJ = "12.345.678/0001-90", });

            Delete.FromTable("TBL_EMPRESAS").
                Row(new { NR_CNPJ = "12.345.678/0002-71", });
        }
    }
}