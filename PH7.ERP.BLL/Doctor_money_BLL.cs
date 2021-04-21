using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class Doctor_money_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        public Doctor_money_BLL(SqlServerHelper _helper) : base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 获取所属医生金额表
        /// </summary>
        /// <param name="Doctor_ID">医生id</param>
        /// <returns></returns>
        public List<Doctor_money_Model> GetDoctPatiList(int Doctor_ID)
        {
            string sql = $"select * from Doctor_money where Doctor_ID={Doctor_ID}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Doctor_money_Model>(dataSet.Tables[0]);
        }

    }
}
