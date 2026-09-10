using FluentMigrator;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100011)]
    public class Migration_0011_CreateMultas : Migration {
        public override void Up() {
            Create.Table("Multas")
                .WithColumn("id").AsInt32().PrimaryKey("PK_Multas_Id").Identity()
                .WithColumn("pessoaId").AsInt32().NotNullable()
                .WithColumn("emprestimoId").AsInt32().NotNullable()
                .WithColumn("motivo").AsString(50).NotNullable()
                .WithColumn("valor").AsDecimal(18, 2).NotNullable()
                .WithColumn("data").AsDateTime().NotNullable()
                .WithColumn("pagamento").AsDateTime().Nullable()
                .WithColumn("status").AsString(50).NotNullable()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Multas_PessoaId")
                .FromTable("Multas")
                .ForeignColumn("pessoaId")
                .ToTable("Pessoas")
                .PrimaryColumn("id");

            Create.ForeignKey("FK_Multas_EmprestimoId")
                .FromTable("Multas")
                .ForeignColumn("emprestimoId")
                .ToTable("Emprestimos")
                .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("Multas");
        }
    }
}