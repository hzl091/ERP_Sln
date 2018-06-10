using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ERP.Common.Data;
using ERP.Domain;
using Webdiyer.WebControls.Mvc;

namespace ERP.Common
{
    public class HtmlHelper
    {
        public static string ConvertDataTableToHTML(TableDataInfo tableDataInfo)
        {
            DataTable dt = tableDataInfo.TableData;
            TableInfo t = tableDataInfo.TableInfo;

            string html = "<table border=\"1\">";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += string.Format("<td><a href=\"?sortby={1}\">{0}</a></td>", t.GetColumnDesc(dt.Columns[i].ColumnName), dt.Columns[i].ColumnName);
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

  

    }
}
