using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    [Migration(3, "add more columns in product table")]
    public class Version03 : VersionBase
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
