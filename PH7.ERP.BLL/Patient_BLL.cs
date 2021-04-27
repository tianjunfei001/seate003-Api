using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PH7.ERP.Common;
using PH7.ERP.DAL;
using PH7.ERP.Model;

namespace PH7.ERP.BLL
{
    public class Patient_BLL : BaseBLL
    {
        //注入
        SqlServerHelper helper;
        //构造函数
        public Patient_BLL(SqlServerHelper _helper) : base(_helper)
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
        //注册患者
        public int Patient_register(Patient_Model m)
        {
            string sql = $"insert into patient(userName,_phone,_password) values ('{m.userName}','{m._phone}','{m._password}')";
            int h = helper.ExceuteNonQuery(sql);
            return h;
        }
        //患者登录
        public int Patient_Login(Patient_Model m)
        {
            string sql = $"select * from patient where (userName='{m.userName}' or _phone='{m._phone}') and _password='{m._password}'";
            var h = Convert.ToInt32(helper.ExecuteScalar(sql));
            return h;
        }
        //根据用户获取积分
        public List<Patient_Model> Patient_integral(int id)
        {
            string sql = $"select integral from patient where id={id}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
        }
        //根据用户id显示显示钱包
        public List<patient_money_Model> Patient_wallet_Show(int id)
        {
            string sql = $"select * from patient_money where patient_ID={id}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<patient_money_Model>(dataSet.Tables[0]);
        }
        //根据钱包id显示银行卡
        public List<Patient_Model> patient_bankcard_Show(int id)
        {
            string sql = $"select * from patient_bankcard join patient_money on  patient_money.id=patient_bankcard.qianbaoid join patient on patient.id = patient_money.patient_ID where patient.id = {id}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Patient_Model>(dataSet.Tables[0]);
        }
        //显示消费记录 账单
        public List<patient_detailed_Model> patient_detailed_Show(int moneyid)
        {
            string sql = $"select * from patient_detailed where patient_money_ID ={moneyid}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<patient_detailed_Model>(dataSet.Tables[0]);
        }
        //患者修改密码 
        public int Update_password(Patient_Model p)
        {
            string sql = $"update  patient set Name='{p.userName}', password='{p._password}' where Id='{p.id}'";
            var h = helper.ExceuteNonQuery(sql);
            return h;
        }
        //用户诊断记录
        public List<Disease_records_Model> Disease_records_Show(int patientid)
        {
            string sql = $"select * from Disease_records where patient_Id={patientid}";
            DataSet dataSet = helper.GetDataSet(sql);
            return GetHelper.DatatableTolist<Disease_records_Model>(dataSet.Tables[0]);
        }
        //钱包￥->银行卡(提现)
        public int Patient_Money_bankcard(int patinetid, int moneyid, int bankcard, int money)
        {
            string sql = $"exec proc_Patient_Money_bankcard {patinetid} , {moneyid} ,{bankcard},{money}";
            var h = helper.ExceuteNonQuery(sql);
            return h;
        }
        //钱包￥<-银行卡(充值)
        public int Patient_bankcard_Money(int patinetid, int moneyid, int bankcard, int money)
        {
            string sql = $"exec proc_Patient_bankcard_Money {patinetid} , {moneyid} ,{bankcard},{money}";
            var h = helper.ExceuteNonQuery(sql);
            return h;
        }
    }
}
