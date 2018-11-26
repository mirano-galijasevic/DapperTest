using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer
{
    public class Addresses
    {
        [Required]
        public Int32 Id { get; set; }

        [Required]
        public Int32 ContactId { get; set; }

        [Required]
        [MaxLength( 10 )]
        public String AddressType { get; set; }

        [Required]
        [MaxLength( 50 )]
        public String StreetAddress { get; set; }

        [Required]
        [MaxLength( 50 )]
        public String City { get; set; }

        [Required]
        public Int32 StateId { get; set; }

        public States State { get; set; }

        [Required]
        [MaxLength( 20 )]
        public String PostalCode { get; set; }

    }
}
