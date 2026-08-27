using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026082705)]
public class Migration_005_AlterUsuarios : Migration {
    public override void Up() {
        Alter.Table("TBL_USUARIOS")
            .AlterColumn("DS_SENHA")
            .AsString(200)
            .NotNullable();

        Create.UniqueConstraint("UK_TBL_USUARIOS_DS_LOGIN")
            .OnTable("TBL_USUARIOS")
            .Column("DS_LOGIN");

        Create.UniqueConstraint("UK_TBL_USUARIOS_DS_EMAIL")
            .OnTable("TBL_USUARIOS")
            .Column("DS_EMAIL");
    }

    public override void Down() {
        Delete.UniqueConstraint("UK_TBL_USUARIOS_DS_EMAIL")
            .FromTable("TBL_USUARIOS");

        Delete.UniqueConstraint("UK_TBL_USUARIOS_DS_LOGIN")
            .FromTable("TBL_USUARIOS");

        Alter.Table("TBL_USUARIOS")
            .AlterColumn("DS_SENHA")
            .AsString(50) 
            .NotNullable();
    }
}