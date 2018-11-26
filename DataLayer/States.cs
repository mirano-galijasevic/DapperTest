using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class States
    {
        [Required]
        public Int32 StateId { get; set; }

        [Required]
        [MaxLength( 50 )]
        public String StateName { get; set; }

    }
}
