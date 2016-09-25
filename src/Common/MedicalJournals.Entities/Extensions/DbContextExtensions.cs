using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicalJournals.Entities.Extensions
{
    public static class DbContextExtensions
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static void DeleteDefaultContraint(this MigrationBuilder migration, string tableName, string colName)
        {
            var sql = string.Format(@"DECLARE @SQL varchar(1000) SET @SQL='ALTER TABLE {0} DROP CONSTRAINT 
                                    ['+(SELECT name FROM sys.default_constraints WHERE parent_object_id = object_id('{0}') AND col_name(parent_object_id, parent_column_id) = '{1}')+']'; 
                                    PRINT @SQL; EXEC(@SQL);",
                    tableName, colName);

            migration.Sql(sql);
        }
    }
}
