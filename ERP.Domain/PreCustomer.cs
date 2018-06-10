using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.Domain
{
    public class PreCustomer
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }
    }
}
