using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    [Migration(2, "Create product table")]
    public class Version_02 : VersionBase
    {
        public override void Up()
        {
            CreateTable("products")
                .WithColumn("ProductName").AsString(200).NotNullable()
                .WithColumn("Description").AsString(2000).NotNullable()
                .WithColumn("Price").AsDouble().NotNullable()
                .WithColumn("Brand").AsString().NotNullable()
                .WithColumn("Quantity").AsInt64().NotNullable();
        }
    }
}
