using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Model;
using PH7.ERP.DAL;
using Microsoft.Extensions.Configuration;

namespace PH7.ERP.BLL
{
    public class Hospital_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        //构造函数
        public Hospital_BLL(SqlServerHelper _helper):base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 获取医院表
        /// </summary>
        /// <returns></returns>
        public List<Hospital_Model> GetHospital()
        {
            string sql = "select * from hospital";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Hospital_Model> list=helper.DatatableTolist<Hospital_Model>(dataSet.Tables[0]);
            return list;
        }

    }
}
