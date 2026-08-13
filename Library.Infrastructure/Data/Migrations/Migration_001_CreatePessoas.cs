using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026081201)]
public class Migration_001_CreatePessoas : Migration
{
    public override void Up()
    {
        Create.Table("Pessoas")
            .WithColumn("id")
                .AsInt32()
                .PrimaryKey()
                .Identity()

            .WithColumn("nome")
                .AsString(150)
                .NotNullable()

            .WithColumn("cpfCnpj")
                .AsString(30)
                .Nullable()

            .WithColumn("nascimento")
                .AsDateTime()
                .Nullable()

            .WithColumn("email")
                .AsString(100)
                .Nullable()

            .WithColumn("genero")
                .AsString(20)
                .Nullable()

            .WithColumn("cadastro")
                .AsDateTime()
                .NotNullable()

            .WithColumn("atualizacao")
                .AsDateTime()
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("Pessoas");
    }
}