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
        public static string ConvertDataTableToHTML(TableDataInfo tableDataInfo, string orderBy, string sort)
        {
            DataTable dt = tableDataInfo.TableData;
            TableInfo t = tableDataInfo.TableInfo;

            string html = "<table border=\"1\">";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
            { 
                string colName = dt.Columns[i].ColumnName;
                if (orderBy.ToLower().Equals(colName.ToLower()))
                {
                    if (sort.ToLower().Equals("asc"))
                    {
                        html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", t.GetColumnDesc(colName), colName, "DESC");
                    }
                    else
                    {
                        html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", t.GetColumnDesc(colName), colName, "ASC");
                    }
                }
                else
                {
                    html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", t.GetColumnDesc(colName), colName, "DESC");
                }
            }
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
