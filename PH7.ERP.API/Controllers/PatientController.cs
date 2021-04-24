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
        [HttpPost,Route("GetPatientRegister")]
        public IActionResult GetPatientRegister(Patient_Model m)
        {
            int id = patient_BLL.GetPatientRegister(m);

            return Ok(new { id = id });
        }



    }
}
