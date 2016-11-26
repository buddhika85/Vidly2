using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly2.Models
{
    public class CustomerFormViewModel
    {
        #region fromModel
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Display(Name = "Is subscribed to news letter?")]
        public bool IsSubscribedToNewsletter { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BirthDate { get; set; }

        // navigation property
        public MembershipType MembershipType { get; set; }

        // foriegn key
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
        #endregion fromModel

        // for the drop down lists
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}