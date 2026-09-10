using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100006)]
    public class Migration_0006_CreateEditoras : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Editoras"))
                .ComCamposBase()
                .WithColumn("razaoSocial").AsString(200).NotNullable()
                .ComCamposDadosBasicos()
                .WithColumn("nacionalidade").AsString(50).NotNullable()
                .WithColumn("siteOficial").AsString(100).NotNullable()
                .WithColumn("fundacao").AsDateTime().NotNullable()
                .ComCamposEndereco()
                .ComCamposAuditoria();
        }
        public override void Down() {
            Delete.Table("Editoras");
        }
    }
}