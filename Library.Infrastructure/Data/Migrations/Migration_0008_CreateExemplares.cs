using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100008)]
    public class Migration_0008_CreateExemplares : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Exemplares"))
                .ComCamposBase("Exemplares")
                .WithColumn("livroId").AsInt32().NotNullable()
                .WithColumn("patrimonio").AsString(50).NotNullable()
                .WithColumn("localizacao").AsString(100).Nullable()
                .WithColumn("ean").AsString(50).NotNullable()
                .WithColumn("aquisicao").AsDateTime().NotNullable()
                .WithColumn("valorAquisicao").AsDecimal(18, 2).NotNullable()
                .WithColumn("status").AsString(50).NotNullable()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Exemplares_LivroId")
                .FromTable("Exemplares")
                .ForeignColumn("livroId")
                .ToTable("Livros")
                .PrimaryColumn("id");
        }
        public override void Down() {
            Delete.Table("Exemplares");
        }
    }
}