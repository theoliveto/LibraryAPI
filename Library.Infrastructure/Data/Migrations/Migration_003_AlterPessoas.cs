using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026081903)]
public class Migration_003_AlterPessoas : Migration {
    public override void Up() {
        Alter.Table("TBL_PESSOAS")
            .AddColumn("X_ATIVO")
            .AsBoolean()
            .NotNullable();
    }

    public override void Down() {
        Delete.Column("X_ATIVO")
            .FromTable("TBL_PESSOAS");
    }
}