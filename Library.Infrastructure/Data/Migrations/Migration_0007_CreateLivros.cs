using FluentMigrator;
using FluentMigrator.Builders.Create.Table;
using Library.Infrastructure.Data.Migrations.Extensions;

namespace Library.Infrastructure.Data.Migrations {
    [Migration(202609100007)]
    public class Migration_0007_CreateLivros : Migration {
        public override void Up() {
            ((ICreateTableColumnOptionOrWithColumnSyntax)Create.Table("Livros"))
                .ComCamposBase()
                .WithColumn("subtitulo").AsString(200).Nullable()
                .WithColumn("autorId").AsInt32().NotNullable()
                .WithColumn("editoraId").AsInt32().NotNullable()
                .WithColumn("categoriaId").AsInt32().NotNullable()
                .WithColumn("isbn").AsString(50).NotNullable()
                .WithColumn("idioma").AsString(50).NotNullable()
                .WithColumn("edicao").AsString(50).Nullable()
                .WithColumn("paginas").AsInt32().NotNullable()
                .WithColumn("publicacao").AsDateTime().NotNullable()
                .WithColumn("sinopse").AsString(300).Nullable()
                .WithColumn("imagemCapa").AsByte().Nullable()
                .ComCamposAuditoria();

            Create.ForeignKey("FK_Livros_LivroId")
                .FromTable("Livros")
                .ForeignColumn("autorId")
                .ToTable("Autores")
                .PrimaryColumn("id");

            Create.ForeignKey("FK_Livros_LivroId")
                .FromTable("Livros")
                .ForeignColumn("editoraId")
                .ToTable("Editoras")
                .PrimaryColumn("id");

            Create.ForeignKey("FK_Livros_LivroId")
                .FromTable("Livros")
                .ForeignColumn("categoriaId")
                .ToTable("Categorias")
                .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.Table("Livros");
        }
    }
}