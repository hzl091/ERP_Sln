using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using ERP.Domain;
using System.Data.SqlClient;

namespace ERP.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Add()
        {
            TableInfo tab = new TableInfo();
            tab.Name = "pre_customers_demo";
            tab.Type = 0;
            tab.ParentId = 0;
            tab.Desc = "客户信息表";
            tab.AddColumnInfo("Id", "int", "自动编号", true, 1);
            tab.AddColumnInfo("Name", "nvarchar(100)", "客户名称", true, 2);
            tab.AddColumnInfo("Tel", "varchar(20)", "客户电话", true, 3);
            tab.AddColumnInfo("Company", "nvarchar(100)", "所属公司", true, 4);
            tab.AddColumnInfo("Email", "varchar(50)", "客户邮箱", false, 5);

            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            repository.AddTableInfo(tab);
        }


        [TestMethod]
        public void TestMethod1()
        {
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            var tableInfo = repository.GetTableInfos("pre_customers");

            //ICollection<string> rs = repository.GetCols("pre_customers");
            string s = tableInfo.GetColumnSql();
            var sortColumnInfos = tableInfo.SortColumnInfos;

            Console.WriteLine(s);
            //repository.AddPreCustomers();

            var data = repository.GetDataTable(s);
            foreach (DataRow row in data.Rows)
            {
                sortColumnInfos.ToList().ForEach(colInfo =>
                {
                    Console.Write(row[colInfo.Name].ToString() + ",");
                });
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void AddPreCustomers_Test()
        {
            ERP.DAL.ERPRepository repository = new DAL.ERPRepository();
            repository.AddPreCustomers();
        }

        [TestMethod]
        public void GetData_Test()
        {
            var d = (new ERP.DAL.TableInfoRepository()).GetDataTest();
        }

        [TestMethod]
        public void GetPager()
        { 
           var info = ERP.DAL.SqlHelper.GetPagerData("[pre_customers]", "*", "1=1", "ID DESC", 5, 30);
        }
    }
}
