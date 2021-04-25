using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace PH7.ERP.DAL
{
    public class SqlServerHelper
    {
        IConfiguration configuration;

        //构造函数
        public SqlServerHelper(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public string strConnection { get { return configuration.GetConnectionString("SqlServerConnection"); } set { } }

        /// <summary>
        /// 增删改
        /// </summary>
        public int ExceuteNonQuery(string sql)
        {
            using (SqlConnection conn=new SqlConnection(strConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                conn.Open();
                //创建适配器
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                //创建数据
                DataSet dataSet = new DataSet();
                sda.Fill(dataSet);
                return dataSet;
            }
        }
       
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql)
        {
            using (SqlConnection conn=new SqlConnection(strConnection)) 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                object h= cmd.ExecuteScalar();
                return h;
            }
        }

        /// <summary>
        /// 获取sql分页存储过程方法
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="parames"></param>
        /// <param name="outPaoName"></param>
        /// <param name="pagecount"></param>
        /// <returns></returns>
        public DataSet GetProcSql(string procName,Dictionary<string,object> parames,string outPaoName,out int pagecount)
        {
            using (SqlConnection conn=new SqlConnection(strConnection))
            {
                conn.Open();
                //创建命令对象
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = procName;
                cmd.Connection = conn;
                //指定类型
                cmd.CommandType = CommandType.StoredProcedure;

                //遍历接受的参数
                foreach (var item in parames)
                {

                    SqlParameter parameter = new SqlParameter();

                    if (item.Key.ToLower().Equals(outPaoName.ToLower()))
                    {
                        parameter.Direction = ParameterDirection.Output;
                        parameter.Size = 50;
                    }
                    parameter.ParameterName = item.Key;
                    parameter.Value = item.Value;
                    cmd.Parameters.Add(parameter);
                }
                //创建适配器
                SqlDataAdapter sad = new SqlDataAdapter(cmd);
                //创建dataset表
                DataSet dataSet = new DataSet();
                sad.Fill(dataSet);

                pagecount =Convert.ToInt32( cmd.Parameters[outPaoName].Value);

                return dataSet;

            }


        }


        /// <summary>
        /// 事务执行 批量执行sql语句
        /// </summary>
        /// <param name="sqls"></param>
        /// <returns></returns>
        public int ExecTran(List<string> sqls)
        {
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.Transaction = transaction;

                try
                {
                    foreach (var item in sqls)
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

                return 1;
            }
        }


        /// <summary>
        /// 数据表转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> DatatableTolist<T>(DataTable dt)
        {

            Type t = typeof(T);//获取类型
            //获取所有属性
            PropertyInfo[] p = t.GetProperties();
            //定义集合
            List<T> list = new List<T>();
            //遍历数据表
            foreach (DataRow dr in dt.Rows)
            {
                //创建对象
                T obj = (T)Activator.CreateInstance(t);
                //数据流列数
                string[] sdrFileName = new string[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sdrFileName[i] = dt.Columns[i].ColumnName.ToLower();
                }
                foreach (PropertyInfo item in p)
                {
                    //判断Model中的属性是否在流的列名中
                    if (sdrFileName.ToList().IndexOf(item.Name.ToLower()) > -1)
                    {
                        if (dr[item.Name] != System.DBNull.Value)
                        {
                            item.SetValue(obj, dr[item.Name]);//对象属性赋值
                        }
                        else
                        {
                            item.SetValue(obj, null);//对象属性赋值
                        }
                    }
                    else
                    {
                        item.SetValue(obj, null);//对象属性赋值
                    }

                }
                list.Add(obj);
            }
            return list;
        }



    }
}
