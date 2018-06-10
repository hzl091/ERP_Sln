using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Domain;
using System.Data;

namespace ERP.DAL
{
    public class ERPRepository
    {
        private ERPDBContext _dbContext = null;

        public ERPRepository()
        {
            _dbContext = new ERPDBContext();
            //_dbContext.Database.Log = Console.WriteLine;
        }

        public void AddTableInfo(TableInfo tableInfo)
        {
            _dbContext.TableInfos.Add(tableInfo);
           _dbContext.SaveChanges();
        }

        public IEnumerable<TableInfo> GetTables(string tabName = null)
        { 
            if(!string.IsNullOrEmpty(tabName))
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

        public ICollection<string> GetCols(string tabName)
        {
            var rs = _dbContext.Database.SqlQuery<string>(string.Format("select  t.c_name from tab_col_info t where t.t_id  = (select t_id from tab_info where t_name = '{0}')", tabName));
            return rs.ToArray();
        }

        public ICollection<string> GetCols(int tabId)
        {
            var rs = _dbContext.Database.SqlQuery<string>(string.Format("select  t.c_name from tab_col_info t where t.t_id  = {0}", tabId));
            return rs.ToArray();
        }

        public ICollection<PreCustomer> GetData(string cols)
        {
            var rs = _dbContext.Database.SqlQuery<PreCustomer>(string.Format("select {0} from pre_customers", cols));
            return _dbContext.PreCustomers.ToList();
        }

        public DataTable GetDataTable(string cols)
        {
            return SqlHelper.GetTableData(string.Format("select {0} from pre_customers", cols));
        }

        public void AddPreCustomers()
        {
            for (int i = 0; i < 500; i++)
            {
                _dbContext.PreCustomers.Add(new Domain.PreCustomer
                {
                    Name = "ZHANGSAN"+i,
                    Tel = new Random().Next(10000,10000000).ToString(),
                    Email = "sss@126.com",
                    Company = "gggdddd"
                });
            } 
            _dbContext.SaveChanges();
        }

        public TableInfo GetTableInfos(int tabId)
        {
            return _dbContext.TableInfos.Where(t => t.Id.Equals(tabId)).Single();
        }

        public TableInfo GetTableInfos(string tabName)
        {
            return _dbContext.TableInfos.Where(t => t.Name.Equals(tabName)).Single();
        }

        public void AddColToTableInfo(string tabName, ColumnInfo colInfo)
        {
            var tabInfo = _dbContext.TableInfos.Where(t =>t.Name.Equals(tabName)).FirstOrDefault();
            if(tabInfo != null)
            {
                tabInfo.AddColumnInfo(colInfo.Name, colInfo.Type, colInfo.Desc, colInfo.IsSystem, colInfo.Sort);
                _dbContext.SaveChanges();
            }  
        }

        public int AddColToTabel(string tabName, ColumnInfo colInfo)
        { 
            string sql = string.Format("alter table [{0}] add [{1}] {2}", tabName, colInfo.Name, colInfo.Type);
            return SqlHelper.ExecuteNonQuery(sql);
        }
    }
}
