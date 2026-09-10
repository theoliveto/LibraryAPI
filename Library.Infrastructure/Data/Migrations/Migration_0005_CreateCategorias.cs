using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100005)]
    public class Migration_0005_CreateCategorias : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Categorias"))
                .ComCamposBase()
                .ComCamposAuditoria();
        }
        public override void Down() {
            Delete.Table("Categorias");
        }
    }
}