using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100004)]
    public class Migration_0004_CreateAutores : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Autores"))
                .ComCamposBase("Autores")
                .WithColumn("nascimento").AsDateTime().NotNullable()
                .WithColumn("falecimento").AsDateTime().Nullable()
                .WithColumn("nacionalidade").AsString(50).NotNullable()
                .WithColumn("genero").AsString(20).NotNullable()
                .WithColumn("siteOficial").AsString(100).NotNullable()
                .ComCamposAuditoria();
        }

        public override void Down() {
            Delete.Table("Autores");
        }
    }
}