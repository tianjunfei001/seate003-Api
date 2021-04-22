using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class BaseBLL
    {
        //SqlServerHelper helper = new SqlServerHelper();
        //注入
        SqlServerHelper helper;
        public BaseBLL(SqlServerHelper _helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 添加表方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetAddTable<T>(T dt,string key)
        {
            string sql = GetHelper.GetAddSql<T>(dt,key);
            return helper.ExceuteNonQuery(sql);
        }

        /// <summary>
        /// 显示表信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetShowTable<T>()
        {
            string sql = GetHelper.GetShowSql<T>();
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<T>(dataSet.Tables[0]);
        }


        /// <summary>
        /// 删除表数据方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetDelTable<T>(string id,string key)
        {
            string sql = GetHelper.GetDelSql<T>(id, key);
            return helper.ExceuteNonQuery(sql);
        }
        /// <summary>
        /// 修改 表数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetUpdateTable<T>(T dt,string key)
        {
            string sql = GetHelper.GetUpdSql<T>(dt, key);
            return helper.ExceuteNonQuery(sql);
        }
        //反填
        public T GetFanTable<T>(int id,string key)
        {
            string sql = GetHelper.GetFanSql<T>(id, key);
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<T>(dataSet.Tables[0])[0];
        }


    }
}
