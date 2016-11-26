using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class MembershipType
    {
        [Required]
        public byte Id { get; set; }

        [StringLength(50)]
        public string MembershipName { get; set; }

        [Required]
        public short SignupFee { get; set; }

        [Required]
        public byte DurationInMonths { get; set; }

        [Required]
        public byte DiscountRate { get; set; }
    }
}