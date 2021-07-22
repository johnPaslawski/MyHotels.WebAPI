using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Rating { get; set; }
        public string Country { get; set; }
        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
    }
}
