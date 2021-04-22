using PH7.ERP.DAL;
using PH7.ERP.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PH7.ERP.BLL
{
    public class Grade_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        public Grade_BLL(SqlServerHelper _helper) : base(_helper)
        {
            helper = _helper;
        }
        /// <summary>
        /// 获取等级表
        /// </summary>
        /// <returns></returns>
        public List<Grade_Model> GetGradeList(int hospital_Id,int Department_Id)
        {
            string sql = $"select * from Grade join(select Grade_Id, count(Department_Id) Cycount, sum(frequency) frequency from DoctorLog join Doctor_relation on DoctorLog.id = Doctor_relation.Doctor_ID where hospital_Id = {hospital_Id} and Department_Id = {Department_Id} group by Grade_Id) a on Grade.id = Grade_Id";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Grade_Model> list = helper.DatatableTolist<Grade_Model>(dataSet.Tables[0]);
            return list;

        }

    }
}
