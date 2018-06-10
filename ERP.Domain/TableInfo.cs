using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain
{
    public class TableInfo
    {
        private List<ColumnInfo> _columnInfos = null;

        public TableInfo()
        {
            _columnInfos = new List<ColumnInfo>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Desc { get; set; }

        public int Type { get; set; }

        public int ParentId { get; set; }

        public virtual ICollection<ColumnInfo> ColumnInfos 
        {
            get { return _columnInfos; }
            set { _columnInfos = value.ToList(); } 
        }

        public IEnumerable<ColumnInfo> SortColumnInfos 
        {
            get
            {
                return ColumnInfos.OrderBy(c => c.Sort).ToList();
            }    
        }
        public string GetColumnSql()
        {
            return string.Join(",", SortColumnInfos.Select(c => c.Name));
        }

        public string GetColumnDesc(string colName)
        {
            if (colName.ToLower() == "rownum")
            {
                return "编号";
            }
           return this.ColumnInfos.Single(c => c.Name.Equals(colName)).Desc;
        }

        public ColumnInfo AddColumnInfo(string name, string type, string desc, bool isSystem, int sort)
        {
            ColumnInfo info = new ColumnInfo();
            info.Name = name;
            info.Type = type;
            info.Desc = desc;
            info.IsSystem = isSystem;
            info.Sort = sort;

            _columnInfos.Add(info);
            return info;
        }
    }
}
