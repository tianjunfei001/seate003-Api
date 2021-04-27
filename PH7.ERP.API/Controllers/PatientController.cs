using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PH7.ERP.BLL;
using PH7.ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PH7.ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        //注入
        Hospital_BLL hospital_BLL;
        Department_BLL department_BLL;
        Grade_BLL grade_BLL;
        DoctorLog_BLL doctorLog_BLL;
        Disease_records_BLL disease_Records_BLL;
        Patient_BLL patient_BLL;


        //构造函数
        public PatientController(Hospital_BLL _BLL, Department_BLL _department_BLL, Grade_BLL _grade_BLL, DoctorLog_BLL _doctorLog_BLL, Disease_records_BLL _disease_Records_BLL, Patient_BLL _patient_BLL)
        {
            hospital_BLL = _BLL;
            department_BLL = _department_BLL;
            grade_BLL = _grade_BLL;
            doctorLog_BLL = _doctorLog_BLL;
            disease_Records_BLL = _disease_Records_BLL;
            patient_BLL = _patient_BLL;
        }



        /// <summary>
        /// 患者手机端注册方法 返回当前id
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost, Route("GetPatientRegister")]
        public IActionResult GetPatientRegister(Patient_Model m)
        {
            int id = patient_BLL.GetPatientRegister(m);

            return Ok(new { id = id });
        }
        //患者注册
        [HttpPost, Route("Patient_Add")]
        public IActionResult Patient_Add(Patient_Model m)
        {
            int id = patient_BLL.Patient_register(m);
            return Ok(new { state = id > 0 ? true : false, msg = id > 0 ? "注册成功" : "注册失败" });
        }
        //患者登录
        [HttpPost, Route("Patient_Login")]
        public IActionResult Patient_Login(Patient_Model m)
        {
            int id = patient_BLL.Patient_Login(m);
            return Ok(new { userid = id, state = id > 0 ? true : false, msg = id > 0 ? "登录成功" : "登录失败" });
        }
        //根据用户获取积分
        [HttpGet, Route("Patient_jifen")]
        public IActionResult Patient_jifen(int id)
        {
            List<Patient_Model> list = patient_BLL.Patient_integral(id);
            return Ok(new { data = list });
        }
        //根据用户id显示显示钱包
        [HttpGet, Route("Patient_wallet_Show")]
        public IActionResult Patient_wallet_Show(int id)
        {
            List<patient_money_Model> list = patient_BLL.Patient_wallet_Show(id);
            return Ok(new { data = list });
        }
        //根据钱包id显示银行卡
        [HttpGet, Route("patient_bankcard_Show")]
        public IActionResult patient_bankcard_Show(int id)
        {
            List<Patient_Model> list = patient_BLL.patient_bankcard_Show(id);
            return Ok(new { data = list });
        }
        //显示消费记录 账单
        [HttpGet, Route("Show_detailed")]
        public IActionResult patient_detailed_Show(int id)
        {
            List<patient_detailed_Model> list = patient_BLL.patient_detailed_Show(id);
            return Ok(new { data = list });
        }
        //修改密码
        [HttpPost, Route("Update_password")]
        public IActionResult Update_password(Patient_Model p)
        {
            int id = patient_BLL.Update_password(p);
            return Ok(new { state = id > 0 ? true : false, msg = id > 0 ? "修改密码成功" : "修改密码失败" });
        }
        //用户诊断记录
        [HttpGet, Route("Show_Disease_records")]
        public IActionResult Show_Disease_records(int patientid)
        {
            List<Disease_records_Model> list = patient_BLL.Disease_records_Show(patientid);
            return Ok(new { data = list });
        }
        //钱包￥->银行卡(提现)
        [HttpGet, Route("Patient_Money_bankcard")]
        public IActionResult Patient_Money_bankcard(int patinetid, int moneyid, int bankcard, int money)
        {
            int h = patient_BLL.Patient_Money_bankcard(patinetid, moneyid, bankcard, money);
            return Ok(new { state = h > 0 ? true : false, msg = h > 0 ? "提现成功" : "提现失败" });
        }
        //钱包￥<-银行卡(充值)
        [HttpGet, Route("Patient_bankcard_Money")]
        public IActionResult Patient_bankcard_Money(int patinetid, int moneyid, int bankcard, int money)
        {
            int h = patient_BLL.Patient_bankcard_Money(patinetid, moneyid, bankcard, money);
            return Ok(new { state = h > 0 ? true : false, msg = h > 0 ? "充值成功" : "充值失败" });
        }
    }
}
