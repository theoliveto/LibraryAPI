using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026081802)]
public class Migration_002_CreateUsuarios : Migration {
    public override void Up() {
        Create.Table("TBL_USUARIOS")
            .WithColumn("CD_USUARIO")
                .AsInt32()
                .PrimaryKey("PK_TBL_USUARIOS_CD_USUARIO")
                .Identity()

            .WithColumn("DS_USUARIO")
                .AsString(150)
                .NotNullable()

            .WithColumn("DS_LOGIN")
                .AsString(30)
                .NotNullable()

            .WithColumn("DS_EMAIL")
                .AsString(100)
                .NotNullable()

            .WithColumn("DS_SENHA")
                .AsString(50)
                .NotNullable()

            .WithColumn("DS_OBSERVACOES")
                .AsString(500)
                .Nullable()

            .WithColumn("X_ATIVO")
                .AsBoolean()
                .NotNullable()

            .WithColumn("X_ADMINISTRADOR")
                .AsBoolean()
                .NotNullable()

            .WithColumn("DS_OBSERVACOES")
                .AsString(500)
                .Nullable()

            .WithColumn("DT_CADASTRO")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DT_ATUALIZACAO")
                .AsDateTime()
                .Nullable();
    }

    public override void Down() {
        Delete.Table("TBL_USUARIOS");
    }
}