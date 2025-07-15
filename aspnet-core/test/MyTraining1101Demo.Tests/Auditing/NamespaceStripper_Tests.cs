using MyTraining1101Demo.Auditing;
using MyTraining1101Demo.Test.Base;
using Shouldly;
using Xunit;

namespace MyTraining1101Demo.Tests.Auditing
{
    // ReSharper disable once InconsistentNaming
    public class NamespaceStripper_Tests: AppTestBase
    {
        private readonly INamespaceStripper _namespaceStripper;

        public NamespaceStripper_Tests()
        {
            _namespaceStripper = Resolve<INamespaceStripper>();
        }

        [Fact]
        public void Should_Stripe_Namespace()
        {
            var controllerName = _namespaceStripper.StripNameSpace("MyTraining1101Demo.Web.Controllers.HomeController");
            controllerName.ShouldBe("HomeController");
        }

        [Theory]
        [InlineData("MyTraining1101Demo.Auditing.GenericEntityService`1[[MyTraining1101Demo.Storage.BinaryObject, MyTraining1101Demo.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null]]", "GenericEntityService<BinaryObject>")]
        [InlineData("CompanyName.ProductName.Services.Base.EntityService`6[[CompanyName.ProductName.Entity.Book, CompanyName.ProductName.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[CompanyName.ProductName.Services.Dto.Book.CreateInput, N...", "EntityService<Book, CreateInput>")]
        [InlineData("MyTraining1101Demo.Auditing.XEntityService`1[MyTraining1101Demo.Auditing.AService`5[[MyTraining1101Demo.Storage.BinaryObject, MyTraining1101Demo.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],[MyTraining1101Demo.Storage.TestObject, MyTraining1101Demo.Core, Version=1.10.1.0, Culture=neutral, PublicKeyToken=null],]]", "XEntityService<AService<BinaryObject, TestObject>>")]
        public void Should_Stripe_Generic_Namespace(string serviceName, string result)
        {
            var genericServiceName = _namespaceStripper.StripNameSpace(serviceName);
            genericServiceName.ShouldBe(result);
        }
    }
}
