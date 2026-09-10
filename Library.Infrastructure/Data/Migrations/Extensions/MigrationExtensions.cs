using FluentMigrator.Builders.Create.Table;

namespace Library.Infrastructure.Data.Migrations.Extensions {
    public static class MigrationExtensions {
        public static ICreateTableColumnOptionOrWithColumnSyntax ComCamposAuditoria(this ICreateTableColumnOptionOrWithColumnSyntax table) {
            return table
                .WithColumn("observacoes").AsString(500).Nullable()
                .WithColumn("ativo").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("cadastro").AsDateTime().NotNullable()
                .WithColumn("atualizacao").AsDateTime().Nullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax ComCamposEndereco(this ICreateTableColumnOptionOrWithColumnSyntax table) {
            return table
                .WithColumn("cep").AsString(30).NotNullable()
                .WithColumn("endereco").AsString(200).NotNullable()
                .WithColumn("cidade").AsString(200).NotNullable()
                .WithColumn("estado").AsString(100).NotNullable()
                .WithColumn("bairro").AsString(100).NotNullable()
                .WithColumn("numero").AsString(10).Nullable()
                .WithColumn("complemento").AsString(100).Nullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax ComCamposBase(this ICreateTableColumnOptionOrWithColumnSyntax table) {
            return table
                .WithColumn("id").AsInt32().PrimaryKey($"PK_{table}_Id").Identity()
                .WithColumn("descricao").AsString(200).NotNullable().Unique($"UK_{table}_Descricao");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax ComCamposDadosBasicos(this ICreateTableColumnOptionOrWithColumnSyntax table) {
            return table
                .WithColumn("cpfCnpj").AsString(50).NotNullable().Unique($"UK_{table}_CpfCnpj")
                .WithColumn("email").AsString(100).NotNullable().Unique($"UK_{table}_Email")
                .WithColumn("celular").AsString(30).NotNullable().Unique($"UK_{table}_Celular")
                .WithColumn("telefone").AsString(30).Nullable().Unique($"UK_{table}_Telefone");
        }
    }
}