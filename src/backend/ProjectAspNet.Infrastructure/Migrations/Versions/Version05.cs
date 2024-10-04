using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    [Migration(6, "Create refresh token table")]
    public class Version05 : VersionBase
    {
        public override void Up()
        {
            CreateTable("refreshToken")
                .WithColumn("Value").AsString().NotNullable()
                .WithColumn("UserId").AsInt64().ForeignKey("FK_REFRESHTOKEN_USER", "users", "Id").OnDelete(System.Data.Rule.Cascade);
        }
    }
}
