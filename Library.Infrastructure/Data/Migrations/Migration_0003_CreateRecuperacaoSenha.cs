using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100003)]
    public class Migration_0003_CreateRecuperacaoSenha : Migration {
        public override void Up() {
            Create.Table("RecuperacaoSenha")
                .WithColumn("id").AsInt32().PrimaryKey("PK_RecuperacaoSenha_Id").Identity()
                .WithColumn("usuarioId").AsInt32().NotNullable()
                .WithColumn("token").AsString(128).NotNullable()
                .WithColumn("expiracao").AsDateTime().NotNullable()
                .WithColumn("utilizacao").AsDateTime().Nullable()
                .WithColumn("cadastro").AsDateTime().NotNullable();

            Create.ForeignKey("FK_RecuperacaoSenha_UsuarioId")
                .FromTable("RecuperacaoSenha")
                .ForeignColumn("usuarioId")
                .ToTable("Usuarios")
                .PrimaryColumn("id");

            Create.Index("IX_RecuperacaoSenha_Token")
                .OnTable("RecuperacaoSenha")
                .OnColumn("token")
                .Ascending();

            Create.Index("IX_RecuperacaoSenha_UsuarioId")
                .OnTable("RecuperacaoSenha")
                .OnColumn("usuarioId")
                .Ascending();
        }

        public override void Down() {
            Delete.Table("RecuperacaoSenha");
        }
    }
}