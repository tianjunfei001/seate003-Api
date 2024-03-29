﻿using System;
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
        public DoctorLog_Model GetDoctorLog(string userName, string password)
        {
            string sql = $"select * from DoctorLog where userName='{userName}' and _password='{password}'";
            DataSet dataSet = helper.GetDataSet(sql);
            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
                    return list[0];
                }
            }
            return null;
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
            string sql = $"select DoctorLog.*,Years,hospitalName,Department.name,Grade.name GradeName from hospital join Department on hospital.id = Department.hospital_Id join Grade on Department.id = Grade.Department_ID join DoctorLog on Grade.id = DoctorLog.Grade_Id join Doctor_relation on DoctorLog.id = Doctor_relation.Doctor_ID";
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
        ///Bll医生诊断   HCM添加
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
        //默认未接诊
        public List<Disease_records_Model> Get_Records()
        {
            string sql = $"select *from Disease_records join patient on Disease_records.patient_Id=patient.id where seate=0";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
        }
        //已接诊
        public List<Disease_records_Model> Get_yes()
        {
            string sql = $"select *from Disease_records join patient on Disease_records.patient_Id=patient.id where seate=1";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
        }
        //健康档案
        public Patient_Model Get_By(string id)
        {
            string sql = $"select * from patient join Disease_records on Disease_records.patient_Id=patient.id where patient.id='{id}'";
            DataSet dataSet = helper.GetDataSet(sql);
            var list = GetHelper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
            return list[0];
        }
        //诊断管理
        public List<Disease_records_Model> Get_zdgl()
        {
            string sql = $"select createtime,sum(_money) Total,count(id) Patients from Disease_records group by createtime";          
            DataSet dataSet = helper.GetDataSet(sql);

            return GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
        }
        //诊断列表
        public List<Disease_records_Model> Get_Administrationlist(string sickdate) 
        {
            string sql = $"select* from Disease_records join patient on Disease_records.patient_Id = patient.id where Disease_records.createtime = '{sickdate}' ";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
        }

        //好评评价
        public Disease_records_Model Get_Acclaim(string id)
        {
            string sql = $"select * from Disease_records join patient on Disease_records.patient_Id=patient.id where Disease_records.id='{id}'";
            DataSet dataSet = helper.GetDataSet(sql);
            var list = GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
            return list[0];
        }
        //接诊改
        public int Get_Reception(string id)
        {
            string sql = $"update Disease_records set seate=1 where id='{id}'";
            int result = helper.ExceuteNonQuery(sql);
            return result;
        }


        ////////////////////////////////////////////

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
        /// 医生端接诊台 方法
        /// </summary>
        /// <param name="Platform">站台</param>
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


        /// <summary>
        /// 医生端个人信息 方法
        /// </summary>
        /// <param name="Personal">个人</param>
        /// <returns></returns>
        public List<DoctorLog_Model> GetDoctor_Personal()
        {
            string sql = $"select DoctorLog.*,Grade.name Grade_name,Doctor_relation.Years,hospital.hospitalName from DoctorLog join Doctor_relation on DoctorLog.id=Doctor_relation.Doctor_ID join hospital on DoctorLog.hospital_Id = hospital.id join Grade on DoctorLog.Grade_Id = Grade.id";
            var dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;
        }
        /// <summary>
        ///  医生端修改密码信息  方法
        /// </summary>
        /// <param name="ChangePassword">修改密码</param>
        /// <returns></returns>
        public int ChangePassword(string Mima, string number)
        {
            string sql = $"update DoctorLog set _password='{Mima}' where id={number}";
            return helper.ExceuteNonQuery(sql);
        }
        /// <summary>
        /// 医生端修改个人信息  方法
        /// </summary>
        /// <param name="ModifyUser">修改用户</param>
        /// <returns></returns>
        public int GetModifyUser(string GetUser, string Mima, string number)
        {
            string sql = $"update DoctorLog set  userName='{GetUser}',_password='{Mima}' where id={number}";
            return helper.ExceuteNonQuery(sql);
        }
        /// <summary>
        /// 医生端诊断管理  方法
        /// </summary>
        /// <param name="Diagnosis">诊断</param>
        /// <returns></returns>
        public List<Patient_Model> GetDiagnosis(string GetName)
        {
            string sql = $"select * from patient join Disease_records on Disease_records.patient_Id=patient.id where 1=1";
            if (!string.IsNullOrEmpty(GetName))
            {
                sql += $" and patient.name like '%{GetName}%' or patient._phone like '%{GetName}%'";
            }
            var dataSet = helper.GetDataSet(sql);
            List<Patient_Model> list = helper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
            return list;
        }



        /// <summary>
        /// 账号管理医生显示
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public List<DoctorLog_Model> GetShowDoctorList()
        {
            string sql = $"select hospitalName,Department.name Department_name,Grade.name Grade_name,DoctorLog.* from hospital  join Department on hospital.id = Department.hospital_Id join Grade on Department.id = Grade.Department_ID join DoctorLog on Grade.id = DoctorLog.Grade_Id";

            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;
        }

        /// <summary>
        /// 账号管理医生添加
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetAddDoctors(DoctorLog_Model d)
        {
            string sql = $"insert into DoctorLog(userName,hospital_Id,Grade_Id,_password,cellPhone) values('{d.userName}',{d.hospital_Id},{d.Grade_Id},'{d._password}','{d.cellPhone}')";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }
        /// <summary>
        /// 账号管理医生修改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetupdDoctors(DoctorLog_Model d)
        {
            string sql = $"update DoctorLog set userName='{d.userName}',hospital_Id={d.hospital_Id},Grade_Id={d.Grade_Id},_password='{d._password}',cellPhone='{d.cellPhone}' where id={d.id}";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }
        /// <summary>
        /// 账号管理医生反填
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public DoctorLog_Model GetFanDoctor(int id)
        {
            string sql = $"select hospitalName,Department.name Department_name,Grade.name Grade_name,DoctorLog.* from hospital  join Department on hospital.id = Department.hospital_Id join Grade on Department.id = Grade.Department_ID join DoctorLog on Grade.id = DoctorLog.Grade_Id where DoctorLog.id={id}";

            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list[0];
        }


        /// <summary>
        /// 医生端诊断管理  方法
        /// </summary>
        /// <param name="Diagnosis">诊断</param>
        /// <returns></returns>
        public List<Disease_records_Model> GetDiagnosis()
        {
            string sql = $"select createtime,sum(_money) Total,count(id) Patients from Disease_records  group by createtime";
            var dataSet = helper.GetDataSet(sql);
            List<Disease_records_Model> list = helper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
            return list;
        }
        /// <summary>
        /// 医生端诊断列表  方法
        /// </summary>
        /// <param name="Patient">病人</param>
        /// <returns></returns>
        public List<Patient_Model> GetPatient(string query)
        {
            string sql = $"select * from patient join Disease_records on Disease_records.patient_Id=patient.id where 1=1";
            if (!string.IsNullOrEmpty(query))
            {
                sql += $" and patient.name like '%{query}%' or patient._phone like '%{query}%'";
            }
            var dataSet = helper.GetDataSet(sql);
            List<Patient_Model> list = helper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
            return list;
        }



        //--------------------------------田俊飞  账号管理
        /// <summary>
        /// 获取医生管理页面
        /// </summary>
        /// <returns></returns>
        public List<DoctorLog_Model> GetDoctorLogList()
        {
            string sql = $"select hospitalName,Department.name as Department_name,Grade.name as Grade_name,DoctorLog.* from hospital join Department on hospital.id = Department.hospital_Id join Grade on Department.id = Grade.Department_ID join DoctorLog on Grade.id = DoctorLog.Grade_Id";
            DataSet dataSet = helper.GetDataSet(sql);
            List<DoctorLog_Model> list = helper.DatatableTolist<DoctorLog_Model>(dataSet.Tables[0]);
            return list;

        }

        /// <summary>
        /// 添加医生账户
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetAddDoctorLog(DoctorLog_Model m)
        {

            string sql = $"insert into DoctorLog(Name,_password,cellPhone,hospital_Id,Department_Id,Grade_Id,_certificate,identity_img,Practice_img,userName) values('{m.Name}', '{m._password}', '{m.cellPhone}', {m.hospital_Id}, {m.Department_Id}, {m.Grade_Id}, '{m._certificate}', '{m.identity_img}', '{m.Practice_img}','{m.userName}')";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }

        /// <summary>
        /// 修改医生账户方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public int GetUpdDoctorLog(DoctorLog_Model m)
        {
            string sql = $"update DoctorLog set Name='{m.Name}',_password='{m._password}',cellPhone='{m.cellPhone}',hospital_Id={m.hospital_Id},Department_Id={m.Department_Id},Grade_Id={m.Grade_Id},_certificate='{m._certificate}',identity_img='{m.identity_img}',Practice_img='{m.Practice_img}' where id={m.id}";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }


    }

}
