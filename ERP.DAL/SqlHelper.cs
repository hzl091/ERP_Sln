using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using ERP.Common.Data;

namespace ERP.DAL
{
    public abstract class SqlHelper
    {
        public static string conString {
            get {
                return ConfigurationManager.ConnectionStrings["erpcon"].ConnectionString;
            }
        }

        public static DataTable GetTableData(string sql)
        { 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql,conString);
            da.Fill(dt);
            return dt;
        }

        public static int ExecuteNonQuery(string sql)
        {
            var con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            int count = cmd.ExecuteNonQuery();
            con.Close();
            return count;
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="tableName">DataSet结果中的表名</param>
        /// <param name="outputValues">输出参数集合</param>
        /// <returns>DataSet</returns>
        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                int a = Convert.ToInt32(sqlDA.SelectCommand.Parameters["@PageCount"].Value);
                int b = Convert.ToInt32(sqlDA.SelectCommand.Parameters["@RecordCount"].Value);

                connection.Close();
                return dataSet;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="parameters">存储过程参数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>DataTable</returns>
        public static DataTable RunPagerProcedure(IDataParameter[] parameters, out int pageCount, out int recordCount)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                DataTable dt = new DataTable();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, "Pager", parameters);
                sqlDA.Fill(dt);
                pageCount = Convert.ToInt32(sqlDA.SelectCommand.Parameters["@PageCount"].Value);
                recordCount = Convert.ToInt32(sqlDA.SelectCommand.Parameters["@RecordCount"].Value);
                connection.Close();
                return dt;
            }
        }

        /// <summary>
        /// 构建 SqlCommand 对象(用来返回一个结果集，而不是一个整数值)
        /// </summary>
        /// <param name="connection">数据库连接</param>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    // 检查未分配值的输出参数,将其分配以DBNull.Value.
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }

            return command;
        }


        public static PagerInfo GetPagerData(string tableName, string columns, string where, string orderBy, int pageIndex, int pageSize)
        {
            List<SqlParameter> ps = new List<SqlParameter>();
            ps.Add(new SqlParameter("@TableName", tableName) { DbType = DbType.String });
            if (string.IsNullOrEmpty(columns))
            {
                ps.Add(new SqlParameter("@Columns", "*") { DbType = DbType.String });
            }
            else 
            {
                ps.Add(new SqlParameter("@Columns", columns) { DbType = DbType.String });
            }

            if (string.IsNullOrEmpty(where))
            {
                ps.Add(new SqlParameter("@Condition", "1=1") { DbType = DbType.String });
            }
            else
            {
                ps.Add(new SqlParameter("@Condition", where) { DbType = DbType.String });
            }

            ps.Add(new SqlParameter("@OrderBy", orderBy) { DbType = DbType.String });
            ps.Add(new SqlParameter("@PageNum", pageIndex) { DbType = DbType.Int32 });
            ps.Add(new SqlParameter("@PageSize", pageSize) { DbType = DbType.Int32 });
            ps.Add(new SqlParameter("@PageCount", SqlDbType.Int) { Direction = ParameterDirection.Output });
            ps.Add(new SqlParameter("@RecordCount", SqlDbType.BigInt) { Direction = ParameterDirection.Output });

            int pageCount = 0;
            int recordCount = 0;
            var dt = ERP.DAL.SqlHelper.RunPagerProcedure(ps.ToArray(), out pageCount, out recordCount);
            PagerInfo pagerInfo = new PagerInfo();
            
            pagerInfo.PagerData = dt;
            pagerInfo.PageCount = pageCount;
            pagerInfo.RecordCount = recordCount;

            return pagerInfo;
        }
    }
}