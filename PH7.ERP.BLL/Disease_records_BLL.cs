using System;
using System.Collections.Generic;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;
using System.Data;

namespace PH7.ERP.BLL
{
    public class Disease_records_BLL:BaseBLL
    {

        //注入
        SqlServerHelper helper;

        public Disease_records_BLL(SqlServerHelper _helper) : base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 医生看过的患者
        /// </summary>
        /// <param name="Doctor_Id"></param>
        /// <returns></returns>
        public List<Disease_records_Model> GetDiseaeList(int Doctor_Id)
        {
            string sql = $"select a.*,hospital.hospitalName from hospital join(select Disease_records.*, patient.name, Grade.name as Grade_Name, DoctorLog.hospital_Id,DoctorLog.userName,DoctorLog.Name as Doctor_Name  from Grade join DoctorLog on Grade.id = DoctorLog.Grade_Id join Disease_records on DoctorLog.id = Disease_records.Doctor_Id join patient on Disease_records.patient_Id = patient.id) a on hospital.id = a.hospital_Id where a.Doctor_Id = {Doctor_Id}";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Disease_records_Model> list = helper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
            return list;

        }

        /// <summary>
        /// 患者记录表 联查
        /// </summary>
        /// <param name="patient_Id"></param>
        /// <returns></returns>
        public List<Disease_records_Model> GetDisease_recordsList(int patient_Id)
        {
            string sql = $"select a.*,hospital.hospitalName from hospital join (select Disease_records.*, patient.name, Grade.name as Grade_Name, DoctorLog.hospital_Id from Grade join DoctorLog on Grade.id = DoctorLog.Grade_Id join Disease_records on DoctorLog.id = Disease_records.Doctor_Id join patient on Disease_records.patient_Id = patient.id) a on hospital.id = a.hospital_Id where a.patient_Id = {patient_Id}";
            DataSet dataSet = helper.GetDataSet(sql);
            List<Disease_records_Model> list = helper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
            return list;

        }


    }
}
