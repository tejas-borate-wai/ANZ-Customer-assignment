using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyTraining1101Demo.EntityFrameworkCore
{
    public static class MyTraining1101DemoDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyTraining1101DemoDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyTraining1101DemoDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}