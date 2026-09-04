using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090409)]
    public class Migration_009_CreateEmpresas : Migration {
        public override void Up() {
            Create.Table("TBL_EMPRESAS")
                .WithColumn("CD_EMPRESA")
                    .AsInt32()
                    .PrimaryKey("PK_TBL_EMPRESAS_CD_EMPRESA")
                    .Identity()

                .WithColumn("DS_RAZAO_SOCIAL")
                    .AsString(500)
                    .NotNullable()

                .WithColumn("DS_NOME_FANTASIA")
                    .AsString(500)
                    .NotNullable()

                .WithColumn("NR_CNPJ")
                    .AsString(30)
                    .NotNullable()

                .WithColumn("DS_SIGLA")
                    .AsString(20)
                    .Nullable()

                .WithColumn("X_ATIVO")
                    .AsBoolean()
                    .NotNullable()
                    .WithDefaultValue(true)

                .WithColumn("DT_CADASTRO")
                    .AsDateTime()
                    .NotNullable()

                .WithColumn("DT_ATUALIZACAO")
                    .AsDateTime()
                    .Nullable();

            Create.UniqueConstraint("UQ_TBL_EMPRESAS_DS_RAZAO_SOCIAL")
                .OnTable("TBL_EMPRESAS")
                .Column("DS_RAZAO_SOCIAL");

            Create.UniqueConstraint("UQ_TBL_EMPRESAS_NR_CNPJ")
                .OnTable("TBL_EMPRESAS")
                .Column("NR_CNPJ");
        }

        public override void Down() {
            Delete.Table("TBL_EMPRESAS");
        }
    }
}