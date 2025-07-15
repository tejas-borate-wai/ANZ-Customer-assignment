using MyTraining1101Demo.EntityFrameworkCore;

namespace MyTraining1101Demo.Test.Base.TestData
{
    public class TestDataBuilder
    {
        private readonly MyTraining1101DemoDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(MyTraining1101DemoDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
            new TestEditionsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
