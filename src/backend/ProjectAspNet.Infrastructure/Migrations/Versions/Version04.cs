using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    [Migration(4, "add imagem column in recipes")]
    public class Version04 : VersionBase
    {
        public override void Up()
        {
            Alter.Table("recipes")
                .AddColumn("ImageIdentifier").AsString().Nullable();
        }        
    }
}
