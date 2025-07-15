using MyTraining1101Demo.EntityFrameworkCore;

namespace MyTraining1101Demo.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly MyTraining1101DemoDbContext _context;

        public InitialHostDbBuilder(MyTraining1101DemoDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
