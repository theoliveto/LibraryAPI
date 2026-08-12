using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026081201)]
public class Migration_001_CreatePessoas : Migration
{
    public override void Up()
    {
        Create.Table("PESSOAS")
            .WithColumn("ID")
                .AsInt32()
                .PrimaryKey()
                .Identity()

            .WithColumn("NOME")
                .AsString(150)
                .NotNullable()

            .WithColumn("CPF")
                .AsString(11)
                .Nullable()

            .WithColumn("DATA_NASCIMENTO")
                .AsDateTime()
                .Nullable()

            .WithColumn("EMAIL")
                .AsString(150)
                .Nullable()

            .WithColumn("TELEFONE")
                .AsString(20)
                .Nullable()

            .WithColumn("DATA_CADASTRO")
                .AsDateTime()
                .NotNullable();
    }

    public override void Down()
    {
        Delete.Table("PESSOAS");
    }
}