using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026081401)]
public class Migration_001_CreatePessoas : Migration {
    public override void Up() {
        Create.Table("TBL_PESSOAS")
            .WithColumn("CD_PESSOA")
                .AsInt32()
                .PrimaryKey("PK_TBL_PESSOAS_CD_PESSOA")
                .Identity()

            .WithColumn("DS_PESSOA")
                .AsString(150)
                .NotNullable()

            .WithColumn("NR_CPF")
                .AsString(30)
                .NotNullable()

            .WithColumn("DS_EMAIL")
                .AsString(100)
                .NotNullable()

            .WithColumn("NR_TELEFONE")
                .AsString(30)
                .NotNullable()

            .WithColumn("DT_NASCIMENTO")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DS_GENERO")
                .AsString(20)
                .NotNullable()

            .WithColumn("DS_OBSERVACOES")
                .AsString(500)
                .Nullable()

            .WithColumn("NR_CEP")
                .AsString(30)
                .NotNullable()

            .WithColumn("DS_ENDERECO")
                .AsString(200)
                .NotNullable()

            .WithColumn("DS_CIDADE")
                .AsString(200)
                .NotNullable()

            .WithColumn("DS_ESTADO")
                .AsString(100)
                .NotNullable()

            .WithColumn("DS_BAIRRO")
                .AsString(100)
                .NotNullable()

            .WithColumn("NR_NUMERO")
                .AsString(10)
                .Nullable()

            .WithColumn("DS_COMPLEMENTO")
                .AsString(100)
                .Nullable()

            .WithColumn("DT_CADASTRO")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DT_ATUALIZACAO")
                .AsDateTime()
                .Nullable();
    }

    public override void Down() {
        Delete.Table("TBL_PESSOAS");
    }
}