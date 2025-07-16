using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1101Demo.Customers
{
    [Table("Customers")]
    public class Customer : FullAuditedEntity<int>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string Address { get; set; }

        public ICollection<CustomerUser> CustomerUsers { get; set; }
    }

}
