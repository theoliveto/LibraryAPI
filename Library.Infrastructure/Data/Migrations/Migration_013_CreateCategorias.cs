using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090913)]
    public class Migration_013_CreateCategorias : Migration {
        public override void Up() {
            Create.Table("TBL_CATEGORIAS")
                .WithColumn("CD_CATEGORIA")
                    .AsInt32()
                    .PrimaryKey("PK_TBL_CATEGORIAS_CD_CATEGORIA")
                    .Identity()

                .WithColumn("DS_CATEGORIA")
                    .AsString(150)
                    .NotNullable()
                    .Unique("UK_TBL_CATEGORIAS_DS_CATEGORIA")

                .WithColumn("DS_OBSERVACOES")
                    .AsString(500)
                    .Nullable()

                .WithColumn("X_ATIVO")
                    .AsBoolean()
                    .NotNullable()

                .WithColumn("DT_CADASTRO")
                    .AsDateTime()
                    .NotNullable()

                .WithColumn("DT_ATUALIZACAO")
                    .AsDateTime()
                    .Nullable();
        }

        public override void Down() {
            Delete.Table("TBL_CATEGORIAS");
        }
    }
}