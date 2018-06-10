using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ERP.Common.Data
{
    public class PagerInfo
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public DataTable PagerData { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
    }
}
