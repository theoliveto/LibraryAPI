using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100002)]
    public class Migration_0002_CreatePessoas : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Pessoas"))
                .ComCamposBase("Pessoas")
                .WithColumn("razaoSocial").AsString(200).Nullable()
                .WithColumn("usuarioId").AsInt32().NotNullable()
                .ComCamposDadosBasicos("Pessoas")
                .WithColumn("nascimento").AsDateTime().NotNullable()
                .WithColumn("genero").AsString(30).NotNullable()
                .ComCamposEndereco()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Pessoas_UsuarioId")
                .FromTable("Pessoas")
                .ForeignColumn("usuarioId")
                .ToTable("Usuarios")
                .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("Pessoas");
        }
    }
}