using MyTraining1101Demo.Editions.Dto;

namespace MyTraining1101Demo.MultiTenancy.Payments.Dto
{
    public class PaymentInfoDto
    {
        public EditionSelectDto Edition { get; set; }

        public decimal AdditionalPrice { get; set; }

        public bool IsLessThanMinimumUpgradePaymentAmount()
        {
            return AdditionalPrice < MyTraining1101DemoConsts.MinimumUpgradePaymentAmount;
        }
    }
}
