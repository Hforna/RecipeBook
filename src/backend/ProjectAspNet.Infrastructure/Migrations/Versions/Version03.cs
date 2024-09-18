using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    [Migration(3, "create recipes table")]
    public class Version03 : VersionBase
    {
        public override void Up()
        {
            CreateTable("recipes")
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("TimeRecipe").AsInt32().Nullable()
                .WithColumn("Difficulty").AsInt32().Nullable()
                .WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_USER_RECIPE_ID", "users", "Id");

            CreateTable("ingredients")
                .WithColumn("Item").AsString().NotNullable()
                .WithColumn("RecipeId").AsInt64().ForeignKey("FK_RECIPE_INGREDIENT_ID", "recipes", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("instructions")
                .WithColumn("Step").AsInt16().NotNullable()
                .WithColumn("Text").AsString(2000).NotNullable()
                .WithColumn("RecipeId").AsInt64().ForeignKey("FK_RECIPE_INSTRUCTION_ID", "recipes", "Id")
                .OnDelete(System.Data.Rule.Cascade);

            CreateTable("dishtype")
                .WithColumn("Type").AsInt16().NotNullable()
                .WithColumn("RecipeId").AsInt64().ForeignKey("FK_RECIPE_DISHTYPE_ID", "recipes", "Id")
                .OnDelete(System.Data.Rule.Cascade);

        }

        
    }
}
