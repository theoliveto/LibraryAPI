using FluentMigrator;

namespace Library.Infrastructure.Data.Migrations;

[Migration(2026082706)]
public class Migration_006_InsertUsuarios : Migration {
    public override void Up() {
        Insert.IntoTable("TBL_USUARIOS").Row(new {
            DS_USUARIO = "Administrador",
            DS_LOGIN = "admin",
            DS_EMAIL = "oliveto.vinicius@uceff.edu.br",
            DS_SENHA = "$2a$11$sGpr37b8RWzPGMEqg3boNOCAudbE9S4oIHT1Fob3G./waorKFQbya",
            DS_OBSERVACOES = "Inserção de usuário base (Administrador).",
            X_ATIVO = true,
            X_ADMINISTRADOR = true,
            DT_CADASTRO = DateTime.UtcNow,
            DT_ATUALIZACAO = DateTime.UtcNow
        });
    }

    public override void Down() {
        Delete.FromTable("Usuarios").
            Row(new { DS_EMAIL = "oliveto.vinicius@uceff.edu.br", });
    }
}