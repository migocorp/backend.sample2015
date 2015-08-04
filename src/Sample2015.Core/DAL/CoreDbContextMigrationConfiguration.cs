namespace Sample2015.Core.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal sealed class CoreDbContextMigrationConfiguration : DbMigrationsConfiguration<CoreDbContext>
    {
        public CoreDbContextMigrationConfiguration()
        {
            //// Use command:  Update-Database -Script -StartUpProjectName "Sample2015.Core"

            //// if need to drop table:
            //// this.AutomaticMigrationDataLossAllowed = true;

            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "Sample2015.Core.DAL.CoreDbContext";
        }
    }
}
