using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PH7.ERP.BLL
{
    public class Doctor_detailed_BLL : BaseBLL
    {
        SqlServerHelper helper;

        public Doctor_detailed_BLL(SqlServerHelper _helper) : base(_helper)
        {
            helper = _helper;
        }
        /// <summary>
        /// 钱包流水明细
        /// </summary>
        /// <param name="DoctorLog_id">医生id</param>
        /// <returns></returns>
        public List<Doctor_detailed_Model> GetDoctor_detailed(int DoctorLog_id)
        {
            string sql = $"select * from Doctor_detailed where Doctor_ID ={DoctorLog_id}";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Doctor_detailed_Model> list = helper.DatatableTolist<Doctor_detailed_Model>(dataSet.Tables[0]);
            return list;
        }
    }
}
