using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly2.Models;

namespace Vidly2.Dtos
{
    public class CustomerDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }

        [DataType(DataType.Date)] 
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

        
        // foriegn key        
        public byte MembershipTypeId { get; set; }
    }
}