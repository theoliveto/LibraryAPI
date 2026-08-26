using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026082404)]
public class Migration_004_AlterPessoas : Migration {
    public override void Up() {
        Create.UniqueConstraint("UK_TBL_PESSOAS_NR_CPF")
            .OnTable("TBL_PESSOAS")
            .Column("NR_CPF");

        Create.UniqueConstraint("UK_TBL_PESSOAS_DS_EMAIL")
            .OnTable("TBL_PESSOAS")
            .Column("DS_EMAIL");
    }

    public override void Down() {
        Delete.UniqueConstraint("UK_TBL_PESSOAS_NR_CPF")
            .FromTable("TBL_PESSOAS");

        Delete.UniqueConstraint("UK_TBL_PESSOAS_DS_EMAIL")
            .FromTable("TBL_PESSOAS");
    }
}