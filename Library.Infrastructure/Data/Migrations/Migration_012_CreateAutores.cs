using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090912)]
    public class Migration_012_CreateAutores : Migration {
        public override void Up() {
            Create.Table("TBL_AUTORES")
            .WithColumn("CD_AUTOR")
                .AsInt32()
                .PrimaryKey("PK_TBL_AUTORES_CD_AUTOR")
                .Identity()

            .WithColumn("DS_AUTOR")
                .AsString(150)
                .NotNullable()
                .Unique("UK_TBL_AUTORES_DS_AUTOR")

            .WithColumn("DT_NASCIMENTO")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DT_FALECIMENTO")
                .AsDateTime()
                .Nullable()

            .WithColumn("DS_PAIS")
                .AsString(50)
                .NotNullable()

            .WithColumn("DS_GENERO")
                .AsString(20)
                .NotNullable()

            .WithColumn("DS_SITE_OFICIAL")
                .AsString(100)
                .NotNullable()

            .WithColumn("IM_FOTO")
                .AsBinary(int.MaxValue)
                .Nullable()

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
            Delete.Table("TBL_AUTORES");
        }
    }
}