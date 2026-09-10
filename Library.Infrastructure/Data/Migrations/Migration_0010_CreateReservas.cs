using FluentMigrator;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100010)]
    public class Migration_0010_CreateReservas : Migration {
        public override void Up() {
            Create.Table("Reservas")
                .WithColumn("id").AsInt32().PrimaryKey("PK_Reservas_Id").Identity()
                .WithColumn("pessoaId").AsInt32().NotNullable()
                .WithColumn("exemplarId").AsInt32().NotNullable()
                .WithColumn("reserva").AsDateTime().NotNullable()
                .WithColumn("prazo").AsString(50).NotNullable()
                .WithColumn("validade").AsDateTime().NotNullable()
                .WithColumn("status").AsString(50).NotNullable()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Reservas_PessoaId")
                .FromTable("Reservas")
                .ForeignColumn("pessoaId")
                .ToTable("Pessoas")
                .PrimaryColumn("id");

            Create.ForeignKey("FK_Reservas_ExemplarId")
                .FromTable("Reservas")
                .ForeignColumn("exemplarId")
                .ToTable("Exemplares")
                .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("Reservas");
        }
    }
}