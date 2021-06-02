using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.VM
{
    public class LoginViewModel
    {
        [Required]
        public string ValidationName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
