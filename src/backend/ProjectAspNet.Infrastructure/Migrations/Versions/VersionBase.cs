using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator.Builders.Create.Table;

namespace ProjectAspNet.Infrastructure.Migrations.Versions
{
    public abstract class VersionBase : ForwardOnlyMigration
    {
        protected ICreateTableColumnOptionOrWithColumnSyntax CreateTable(string table)
        {
            return Create.Table(table)
                .WithColumn("Id").AsInt64().ForeignKey().Identity()
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefaultValue(DateTime.UtcNow)
                .WithColumn("Active").AsBoolean().NotNullable();
        }
    }
}
