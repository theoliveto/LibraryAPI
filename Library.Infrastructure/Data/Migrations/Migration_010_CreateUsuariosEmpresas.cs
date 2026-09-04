using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {

    [Migration(2026090410)]
    public class Migration_010_CreateUsuariosEmpresas : Migration {
        public override void Up() {
            Create.Table("TBL_USUARIOS_EMPRESAS")
                .WithColumn("CD_ID")
                    .AsInt32()
                    .PrimaryKey("PK_TBL_USUARIOS_EMPRESAS_CD_ID")
                    .Identity()

                .WithColumn("CD_USUARIO")
                    .AsInt32()
                    .NotNullable()

                .WithColumn("CD_EMPRESA")
                    .AsInt32()
                    .NotNullable()

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

            Create.ForeignKey("FK_TBL_USUARIOS_EMPRESAS_CD_USUARIO")
                .FromTable("TBL_USUARIOS_EMPRESAS")
                .ForeignColumn("CD_USUARIO")
                .ToTable("TBL_USUARIOS")
                .PrimaryColumn("CD_USUARIO");

            Create.ForeignKey("FK_TBL_USUARIOS_EMPRESAS_CD_EMPRESA")
                .FromTable("TBL_USUARIOS_EMPRESAS")
                .ForeignColumn("CD_EMPRESA")
                .ToTable("TBL_EMPRESAS")
                .PrimaryColumn("CD_EMPRESA");
        }

        public override void Down() {
            Delete.Table("TBL_USUARIOS_EMPRESAS");
        }
    }
}