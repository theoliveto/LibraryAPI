using FluentMigrator;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100009)]
    public class Migration_0009_CreateEmprestimos : Migration {
        public override void Up() {
            Create.Table("Emprestimos")
                .WithColumn("id").AsInt32().PrimaryKey("PK_Emprestimos_Id").Identity()
                .WithColumn("pessoaId").AsInt32().NotNullable()
                .WithColumn("exemplarId").AsInt32().NotNullable()
                .WithColumn("emprestimo").AsDateTime().NotNullable()
                .WithColumn("previsaoDevolucao").AsDateTime().NotNullable()
                .WithColumn("devolucao").AsDateTime().Nullable()
                .WithColumn("status").AsString(50).NotNullable()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Emprestimos_PessoaId")
                .FromTable("Emprestimos")
                .ForeignColumn("pessoaId")
                .ToTable("Pessoas")
                .PrimaryColumn("id");

            Create.ForeignKey("FK_Emprestimos_ExemplarId")
                .FromTable("Emprestimos")
                .ForeignColumn("exemplarId")
                .ToTable("Exemplares")
                .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("Emprestimos");
        }
    }
}