using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain;
using ERP.Common.Data;
using System.Data;

namespace ERP.DAL
{
    public class TableInfoRepository
    {
        private ERPDBContext _dbContext = null;

        public TableInfoRepository()
        {
            _dbContext = new ERPDBContext();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public TableInfo GetTableInfo(int tabId)
        {
            return _dbContext.TableInfos.Where(t => t.Id.Equals(tabId)).Single();
        }

        public TableInfo GetTableInfo(string tabName)
        {
            return _dbContext.TableInfos.Where(t => t.Name.Equals(tabName)).Single();
        }

        public IEnumerable<TableInfo> GetTableInfos(string tabName = null)
        {
            if (!string.IsNullOrEmpty(tabName))
            {
                return (from p in _dbContext.TableInfos
                        where p.Name.Equals(tabName)
                        select p).ToList();
            }
            else
            {
                return _dbContext.TableInfos.ToList();
            }
        }

        public TableDataInfo GetTableData(int tabId)
        {
            DataTable dt = new DataTable();
            var tabInfo = this.GetTableInfo(tabId);
            if (tabInfo != null)
            {
                string tabName = tabInfo.Name;
                string cols = tabInfo.GetColumnSql();
                dt = SqlHelper.GetTableData(string.Format("select {0} from {1}", cols, tabName));
            }

            TableDataInfo dataInfo = new TableDataInfo();
            dataInfo.TableInfo = tabInfo;
            dataInfo.TableData = dt;
            return dataInfo;
        }


        public dynamic GetDataTest()
        {
            return _dbContext.Database.SqlQuery<dynamic>("select * from pre_customers")
                .Skip(10)
                .Take(15)
                .ToList();
        }
    }
}
