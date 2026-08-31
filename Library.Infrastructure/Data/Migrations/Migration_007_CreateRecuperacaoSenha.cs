using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026082807)]
public class Migration_007_CreateRecuperacaoSenha : Migration {
    public override void Up() {
        Create.Table("TBL_RECUPERACAO_SENHA")
            .WithColumn("CD_RECUPERACAO")
                .AsInt32()
                .PrimaryKey("PK_TBL_RECUPERACAO_SENHA_CD_RECUPERACAO")
                .Identity()

            .WithColumn("CD_USUARIO")
                .AsInt32()
                .NotNullable()

            .WithColumn("DS_TOKEN")
                .AsString(128)
                .NotNullable()

            .WithColumn("DT_EXPIRACAO")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DT_UTILIZACAO")
                .AsDateTime()
                .Nullable()

            .WithColumn("DT_CADASTRO")
                .AsDateTime()
                .NotNullable();

        Create.ForeignKey("FK_TBL_RECUPERACAO_SENHA_CD_USUARIO")
            .FromTable("TBL_RECUPERACAO_SENHA")
            .ForeignColumn("CD_USUARIO")
            .ToTable("TBL_USUARIOS")
            .PrimaryColumn("CD_USUARIO");

        Create.Index("IX_TBL_RECUPERACAO_SENHA_DS_TOKEN")
            .OnTable("TBL_RECUPERACAO_SENHA")
            .OnColumn("DS_TOKEN")
            .Ascending();

        Create.Index("IX_TBL_RECUPERACAO_SENHA_CD_USUARIO")
            .OnTable("TBL_RECUPERACAO_SENHA")
            .OnColumn("CD_USUARIO")
            .Ascending();
    }

    public override void Down() {
        Delete.Table("TBL_RECUPERACAO_SENHA");
    }
}