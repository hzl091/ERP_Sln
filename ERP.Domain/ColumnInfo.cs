using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain
{
    public class ColumnInfo
    {
        public int Id { get; set; }

        public int TableInfoId { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Desc { get; set; }

        public bool IsSystem { get; set; }

        public int Sort { get; set; }

        public TableInfo TableInfo { get; set; }
    }
}
