using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Webdiyer.WebControls.Mvc;
using System.Data;
using ERP.Common.Data;

namespace ERP.Web.Models
{
    public class TableModel
    {
        public PagedList<DataRow> PagedList { get; set; }

        public TableDataInfo TableInfo { get; set; }

        public RequestInfo RequestInfo { get; set; }
    }

    public class RequestInfo
    {
        public int PageIndex { get; set; }

        public string OrderBy { get; set; }

        public string Sort { get; set; }
    }
}