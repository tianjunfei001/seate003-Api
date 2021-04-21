using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using PH7.ERP.DAL;

namespace PH7.ERP.Common
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public static class GetHelper
    {
        /// <summary>
        /// 数据表转List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DatatableTolist<T>(DataTable dt)
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

        //导出Excel
       

        //高级版本导出
       

        /// <summary>
        /// 添加sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAddSql<T>(T dt, string key)
        {
            string tableName = GetTableName<T>();
            string _values = "";
            string _columnNames = "";
            //获取类型
            Type type = typeof(T);
            //获取属性
            PropertyInfo[] info = type.GetProperties();

            //遍历
            foreach (var item in info)
            {
                if (item.Name.Equals(key))
                {

                }
                else if (item.GetValue(dt) != null && item.GetValue(dt).GetType() == typeof(int))
                {
                    _columnNames += item.Name + ",";
                    _values += $"{item.GetValue(dt)},";
                }
                else
                {
                    if (item.GetValue(dt) != null && item.GetValue(dt).ToString() != "0001/1/1 0:00:00")
                    {
                        _columnNames += item.Name + ",";
                        _values += $"N'{Convert.ToString(item.GetValue(dt)).Replace("'", "''")}',";
                    }

                }



            }
            string sql = $"insert into {tableName}({_columnNames.Trim(',')}) values({_values.Trim(',')})";
            return sql;

        }


        /// <summary>
        /// 删除sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDelSql<T>(string id, string key)
        {
            return $"delete from {GetTableName<T>()} where {key} in({id})";
        }
        /// <summary>
        /// 显示sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetShowSql<T>()
        {
            string sql = $"select * from {GetTableName<T>()}";
            return sql;
        }
        /// <summary>
        /// 修改sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUpdSql<T>(T dt, string key)
        {
            //定义变量
            string where = "";
            string _columnName = "";

            //获取类型
            Type type = typeof(T);
            //获取属性
            PropertyInfo[] infos = type.GetProperties();

            foreach (var item in infos)
            {

                if (item.Name.Equals(key))
                {
                    where = $"{item.Name}={item.GetValue(dt)}";
                }
                else if (item.GetValue(dt) != null && item.GetValue(dt).GetType() == typeof(int))
                {

                    _columnName += $"{item.Name}={item.GetValue(dt)},";

                }
                else
                {
                    if (item.GetValue(dt) != null && item.GetValue(dt).ToString() != "0001/1/1 0:00:00")
                    {
                        _columnName += $"{item.Name}='{Convert.ToString(item.GetValue(dt)).Replace("'", "''")}',";
                    }
                }



            }

            string sql = $"update {GetTableName<T>()} set {_columnName.Trim(',')} where {where}";

            return sql;
        }

        /// <summary>
        /// 反填sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetFanSql<T>(int id, string key)
        {
            string sql = $"select * from {GetTableName<T>()} where {key}={id}";
            return sql;
        }



        //获取表名
        public static string GetTableName<T>()
        {
            //定义表名字
            string TableName = "";
            //判断类型
            MemberInfo info = typeof(T);

            //获取自定义特性
            var res = (TableAttribute)info.GetCustomAttribute(typeof(TableAttribute), false);
            if (res != null)
            {
                TableName = res.Name;
            }

            return TableName;
        }


    }
}
