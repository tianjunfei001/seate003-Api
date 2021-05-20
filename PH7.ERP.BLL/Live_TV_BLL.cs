using PH7.ERP.DAL;
using PH7.ERP.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace PH7.ERP.BLL
{
    public class Live_TV_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        public Live_TV_BLL(SqlServerHelper _helper):base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 直播管理
        /// </summary>
        /// <param name="id">医生id</param>
        /// <returns></returns>
        public List<Live_TV_Model> GetLive_TVList(int id)
        {
            string sql = $"select * from Live_TV where DoctorLog_id={id}";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Live_TV_Model> list = helper.DatatableTolist<Live_TV_Model>(dataSet.Tables[0]);
            return list;
        }







    }
}
