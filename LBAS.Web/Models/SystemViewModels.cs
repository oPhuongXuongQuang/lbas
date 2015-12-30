using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBAS.Web.Models
{
    public class SystemViewModel
    {
       [Key]
        [Required]
        [Display(Name = "PIN Code (*)")]
        public string PinCode { get; set; }

        [Display(Name = "LCC's Serial No.")]
        public string SerialNo { get; set; }

        [Display(Name = "System Name")]
        public string SystemName { get; set; }

        [Display(Name ="System Address")]
        public string SystemAddress { get; set; }

        [Display(Name ="City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}
