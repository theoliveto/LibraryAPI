using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100001)]
    public class Migration_0001_CreateUsuarios : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Usuarios"))
                .ComCamposBase("Usuarios")
                .WithColumn("login").AsString(30).NotNullable().Unique("UK_Usuarios_Login")
                .WithColumn("email").AsString(100).NotNullable().Unique("UK_Usuarios_Email")
                .WithColumn("senha").AsString(200).NotNullable()
                .WithColumn("perfil").AsString(200).NotNullable()
                .WithColumn("avatar").AsBinary(int.MaxValue).Nullable()
                .WithColumn("administrador").AsBoolean().NotNullable()
                .WithColumn("tentativasLogin").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("bloqueioLogin").AsDateTime().Nullable()
                .ComCamposAuditoria();

            Insert.IntoTable("Usuarios").Row(new {
                descricao = "Administrador",
                login = "admin",
                email = "oliveto.vinicius@uceff.edu.br",
                senha = "$2a$11$ICT0EM0gm2wWCxmlkFL9F.H2zHABntuRHRgDVUFK.z5QbNnPdSaYe",
                perfil = "Master",
                administrador = true,
                tentativasLogin = 0,
                bloqueioLogin = (DateTime?)null,
                observacoes = "Inserção de usuário base (Administrador).",
                ativo = true,
                cadastro = DateTime.UtcNow,
                atualizacao = DateTime.UtcNow
            });
        }

        public override void Down() {
            Delete.Table("Usuarios");
        }
    }
}