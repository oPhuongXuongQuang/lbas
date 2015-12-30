using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace LBAS.Web.Models
{
    public class CommandViewModel
    {
        public bool IsExtensionCommand { get; set; }

        public SelectList CommandList { get; set; }
    }
}
