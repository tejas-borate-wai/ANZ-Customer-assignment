using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyTraining1101Demo.Configuration;
using MyTraining1101Demo.Web;

namespace MyTraining1101Demo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class MyTraining1101DemoDbContextFactory : IDesignTimeDbContextFactory<MyTraining1101DemoDbContext>
    {
        public MyTraining1101DemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MyTraining1101DemoDbContext>();

            /*
             You can provide an environmentName parameter to the AppConfigurations.Get method. 
             In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
             Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
             https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
             */
            var configuration = AppConfigurations.Get(
                WebContentDirectoryFinder.CalculateContentRootFolder(),
                addUserSecrets: true
            );

            MyTraining1101DemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(MyTraining1101DemoConsts.ConnectionStringName));

            return new MyTraining1101DemoDbContext(builder.Options);
        }
    }
}
