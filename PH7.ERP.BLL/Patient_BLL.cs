using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class Patient_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;
        //构造函数
        public Patient_BLL(SqlServerHelper _helper):base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 获取所属医生的患者资料  是否接诊 未0 已接诊1
        /// </summary>
        /// <param name="DoctorId">医生 id</param>
        /// <param name="seate">是否接诊 未0 已接诊1</param>
        /// <returns></returns>
        public List<Patient_Model> GetDoctPatiList(int DoctorId, int seate)
        {
            string sql = $"select * from patient where DoctorId={DoctorId} and seate={seate}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
        }

        /// <summary>
        /// 诊断报告表
        /// </summary>
        /// <param name="Disease_records_id">诊断记录表ID</param>
        /// <returns></returns>
        public List<Patient_Model> GetPati_DiagnosisList(int Disease_records_id)
        {
            string sql = $"select patient.*,describe,diagnosis from patient join Disease_records on patient.id=Disease_records.patient_Id where Disease_records.id={Disease_records_id}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
        }

        /// <summary>
        /// 患者手机端注册方法 返回当前id
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetPatientRegister(Patient_Model m)
        {
            string sql = $"insert into patient(userName,_password,_phone,_tou) values('{m.userName}','{m._password}','{m._phone}','{m._tou}');select @@IDENTITY";
            int h = Convert.ToInt32(helper.ExecuteScalar(sql));

            return h;
        }


    }
}
