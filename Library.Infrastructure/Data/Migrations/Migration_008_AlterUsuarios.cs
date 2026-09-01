using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090108)]
    public class Migration_008_AlterUsuarios : Migration {
        public override void Up() {
            Alter.Table("TBL_USUARIOS")
                .AddColumn("NR_TENTATIVAS_LOGIN")
                    .AsInt32()
                    .NotNullable()
                    .WithDefaultValue(0)

                .AddColumn("DT_BLOQUEIO_LOGIN")
                    .AsDateTime()
                    .Nullable();
        }

        public override void Down() {
            Delete.Column("DT_BLOQUEIO_LOGIN")
                .FromTable("TBL_USUARIOS");

            Delete.Column("NR_TENTATIVAS_LOGIN")
                .FromTable("TBL_USUARIOS");
        }
    }
}