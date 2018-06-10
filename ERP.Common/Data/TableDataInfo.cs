using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ERP.Domain;

namespace ERP.Common.Data
{
    public class TableDataInfo
    {
        public TableInfo TableInfo { get; set; }

        public DataTable TableData { get; set; }
    }

    public class DataRowDataInfo
    {
        public TableInfo TableInfo { get; set; }

        public IEnumerable<DataRow> Rows { get; set; }
    } 
}
