using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class Department_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        public Department_BLL(SqlServerHelper _helper) : base(_helper)
        {
            helper = _helper;
        }
        /// <summary>
        /// 获取科室表
        /// </summary>
        /// <returns></returns>
        public List<Department_Model> GetDepartemntList(int hospital_Id)
        {
            string sql = $"select * from Department join(select Department_Id, count(Department_Id) Cycount, sum(frequency) frequency from DoctorLog join Doctor_relation on DoctorLog.id = Doctor_relation.Doctor_ID where hospital_Id = {hospital_Id} group by Department_Id) a on Department.id = Department_Id";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Department_Model> list = helper.DatatableTolist<Department_Model>(dataSet.Tables[0]);
            return list;

        }


    }
}
