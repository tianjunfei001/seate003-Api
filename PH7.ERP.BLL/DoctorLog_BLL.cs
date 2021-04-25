using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class DoctorLog_BLL:BaseBLL
    {
        //注入
        SqlServerHelper helper;

        public DoctorLog_BLL(SqlServerHelper _helper):base(_helper)
        {
            helper = _helper;
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public int GetDoctorLog(string userName, string password)
        {
            string sql = $"select * from DoctorLog where userName='{userName}' and _password='{password}'";
            int h = Convert.ToInt32(helper.ExecuteScalar(sql));
            return h >= 1 ? 1 : 0;
        }


        /// <summary>
        /// 注册方法
        /// </summary>
        /// <param name="m">数据</param>
        /// <returns></returns>
        public int GetDoctorAdd(DoctorLog_Model m)
        {
            string sql = GetHelper.GetAddSql(m,"id");
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }

        
        /// <summary>
        /// 主治医师列表
        /// </summary>
        /// <returns></returns>
        public List<DoctorLog_Model> GetDoct_relationList(int hospital_Id, int Department_Id,int Grade_Id)
        {
            string sql = $"select a.*,balance,Grade.name GradeName from Grade join (select  DoctorLog.*, frequency, Years from DoctorLog join Doctor_relation on DoctorLog.id = Doctor_relation.Doctor_ID where hospital_Id = {hospital_Id} and Department_Id = {Department_Id} and Grade_Id = {Grade_Id}) a on Grade.id = a.Grade_Id join Doctor_money on a.id = Doctor_money.Doctor_ID";
            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;

        }


        /// <summary>
        /// 审核管理医生页面方法 判断状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <returns></returns>
        public List<DoctorLog_Model> GetDoct_State_relationList()
        {
            string sql = $"select DoctorLog.*,Years,hospitalName,Department.name,Grade.name from hospital join Department on hospital.id = Department.hospital_Id join Grade on Department.id = Grade.Department_ID join DoctorLog on Grade.id = DoctorLog.Grade_Id join Doctor_relation on DoctorLog.id = Doctor_relation.Doctor_ID";
            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;

        }


        /// <summary>
        /// 通过医生资质方法
        /// </summary>
        /// <param name="DoctorLog_Id">医生id</param>
        /// <returns></returns>
        public int GetUpdateDoct_true(int DoctorLog_Id)
        {
            string sql = $"exec DoctorLogSeate {DoctorLog_Id}";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }

        /// <summary>
        /// 不通过医生资质方法
        /// </summary>
        /// <param name="DoctorLog_Id">医生id</param>
        /// <returns></returns>
        public int GetUpdateDoct_first(int DoctorLog_Id,string reason)
        {
            string sql = $"exec DoctorLogSeatefales {DoctorLog_Id},'{reason}'";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }


        /// <summary>
        /// 查看未通过审核方法 两表联查
        /// </summary>
        /// <param name="DoctorLog_id">医生id</param>
        /// <returns></returns>
        public List<DoctorLog_Model> GetDoctorYuan(int DoctorLog_id)
        {
            string sql = $"select DoctorLog.*,reason from Doctor_audit join DoctorLog on Doctor_audit.DoctorLog_id={DoctorLog_id}";
            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;
        }


        /// <summary>
        /// 手机号登录  查询
        /// </summary>
        /// <param name="_phone">手机号</param>
        /// <returns></returns>
        public int GetDoctLog_phone(string cellPhone)
        {
            string sql = $"select count(*) from DoctorLog where cellPhone='{cellPhone}'";
            int h = Convert.ToInt32(helper.ExecuteScalar(sql));
            return h;
        }

        /// <summary>
        /// 医生端注册页面 注册方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetDoctLog_Zhuci(DoctorLog_Model m)
        {
            string sql = $"insert into DoctorLog(userName,_password,cellPhone) values('{m.userName}','{m._password}','{m.cellPhone}')";

            int h = helper.ExceuteNonQuery(sql);
            return h;
        }

        /// <summary>
        /// 医生端接诊台页面 方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public List<Patient_Model> GetDoctor_Platform(string State, string Sname)
        {
            string sql = $"select * from patient join Disease_records on patient.id=Disease_records.patient_Id where 1=1 and Disease_records.seate='{State}'";
            if (!string.IsNullOrEmpty(Sname))
            {
                sql += $"and patient.name like '%{Sname}%' or patient._phone like '%{Sname}%'";
            }
            var dataSet = helper.GetDataSet(sql);
            List<Patient_Model> list = helper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
            return list;
        }
    }
 

}
