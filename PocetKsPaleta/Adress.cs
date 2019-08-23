using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocetKsPaleta
{
    class Adress
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string State { get; set; }
        public string ContaktPerson { get; set; }
        public string TelNumber { get; set; }
    }
}
