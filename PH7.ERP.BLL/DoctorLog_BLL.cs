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


    }

}
