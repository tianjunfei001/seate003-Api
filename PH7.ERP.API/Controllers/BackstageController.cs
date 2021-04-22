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
    //后台系统管理
    public class BackstageController : ControllerBase
    {

        //注入
        Hospital_BLL bLL;
        Department_BLL department_BLL;
        Grade_BLL grade_BLL;
        DoctorLog_BLL doctorLog_BLL;
        Disease_records_BLL disease_Records_BLL;
        Patient_BLL patient_BLL;

        //构造函数
        public BackstageController(Hospital_BLL _BLL, Department_BLL _department_BLL, Grade_BLL _grade_BLL, DoctorLog_BLL _doctorLog_BLL, Disease_records_BLL _disease_Records_BLL, Patient_BLL _patient_BLL)
        {
            bLL = _BLL;
            department_BLL = _department_BLL;
            grade_BLL = _grade_BLL;
            doctorLog_BLL = _doctorLog_BLL;
            disease_Records_BLL = _disease_Records_BLL;
            patient_BLL = _patient_BLL;
        }

        //获取医院表表
        [HttpGet]
        [Route("GetHost")]
        public IActionResult GetHost()
        {
            List<Hospital_Model> models = bLL.GetHospital();

            return Ok(new { data = models });
        }

        //获取医院管理页面 （问诊次数 医生数量）
        [HttpPost]
        [Route("GetHospList")]
        public IActionResult GetHospList(int page = 1, int limit = 2,string name="")
        {
            List<Hospital_Model> models = bLL.GetHospList();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.hospitalName.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        /// <summary>
        /// 获取科室管理页面 （问诊次数 医生数量）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDepaList")]
        public IActionResult GetDepaList(int hospital_Id, int page = 1, int limit = 2, string name = "")
        {
            List<Department_Model> models = department_BLL.GetDepartemntList(hospital_Id);

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.name.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        /// <summary>
        /// 获取等级管理页面 （问诊次数 医生数量）
        /// </summary>
        /// <param name="hospital_Id"> 医院id</param>
        /// <param name="Department_Id">科室id</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetGradeList")]
        public IActionResult GetGradeList(int hospital_Id, int Department_Id, int page = 1, int limit = 2, string name = "")
        {
            List<Grade_Model> models = grade_BLL.GetGradeList(hospital_Id, Department_Id);

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.name.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }



        /// <summary>
        /// 主治医师列表
        /// </summary>
        /// <param name="hospital_Id"> 医院id</param>
        /// <param name="Department_Id">科室id</param>
        /// <param name="Grade_Id">医生等级id</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDoct_relationList")]
        public IActionResult GetDoct_relationList(int hospital_Id, int Department_Id, int Grade_Id, int page = 1, int limit = 2, string name = "")
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetDoct_relationList(hospital_Id, Department_Id, Grade_Id);

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.Name.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }



        /// <summary>
        /// 医生看过的患者
        /// </summary>
        /// <param name="Doctor_Id"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDiseaeList")]
        public IActionResult GetDiseaeList(int Doctor_Id,int page = 1, int limit = 2, string name = "")
        {
            List<Disease_records_Model> models = disease_Records_BLL.GetDiseaeList(Doctor_Id);

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p=>p.name.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        /// <summary>
        /// 患者列表（分页，查询）
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPatientList")]
        public IActionResult GetPatientList(int page = 1, int limit = 2, string name = "")
        {
            List<Patient_Model> models = patient_BLL.GetShowTable<Patient_Model>();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.name.Contains(name)).ToList();
            }

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }


        /// <summary>
        /// 患者记录表 联查
        /// </summary>
        /// <param name="patient_Id">患者id</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDisease_recordsList")]
        public IActionResult GetDisease_recordsList(int patient_Id, int page = 1, int limit = 2)
        {
            List<Disease_records_Model> models = disease_Records_BLL.GetDisease_recordsList(patient_Id);

           
            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        //添加医生方法
        public IActionResult GetAddDoctor(DoctorLog_Model m)
        {
            int h = doctorLog_BLL.GetAddTable(m, "id");
            return Ok(new { msg = h });
        }

        //删除医生方法
        public IActionResult GetDelDoctor(string id)
        {
            int h = doctorLog_BLL.GetDelTable<DoctorLog_Model>(id,"id");
            return Ok(new { msg = h });
        }
        //修改患者方法
        public IActionResult GetUpdDoctor(DoctorLog_Model m)
        {
            int h = doctorLog_BLL.GetUpdateTable(m,"id");
            return Ok(new { msg = h });
        }
        

        //添加患者方法
        public IActionResult GetAddPatient(Patient_Model m)
        {
            int h = doctorLog_BLL.GetAddTable(m, "id");
            return Ok(new { msg = h });
        }

        //删除患者方法
        public IActionResult GetDelPatient(string id)
        {
            int h = doctorLog_BLL.GetDelTable<Patient_Model>(id, "id");
            return Ok(new { msg = h });
        }
        //修改患者方法
        public IActionResult GetUpdPatient(Patient_Model m)
        {
            int h = doctorLog_BLL.GetUpdateTable(m, "id");
            return Ok(new { msg = h });
        }

    }
}
