using Abp.Domain.Entities;
using MyTraining1101Demo.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Customers
{
    [Table("CustomerUsers")]
    public class CustomerUser: Entity<int>
    {
        public int CustomerId { get; set; }
        public long UserId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }

}
