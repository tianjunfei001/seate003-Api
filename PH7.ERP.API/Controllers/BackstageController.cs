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
        Hospital_BLL hospital_BLL;
        Department_BLL department_BLL;
        Grade_BLL grade_BLL;
        DoctorLog_BLL doctorLog_BLL;
        Disease_records_BLL disease_Records_BLL;
        Patient_BLL patient_BLL;
        

        //构造函数
        public BackstageController(Hospital_BLL _BLL, Department_BLL _department_BLL, Grade_BLL _grade_BLL, DoctorLog_BLL _doctorLog_BLL, Disease_records_BLL _disease_Records_BLL, Patient_BLL _patient_BLL)
        {
            hospital_BLL = _BLL;
            department_BLL = _department_BLL;
            grade_BLL = _grade_BLL;
            doctorLog_BLL = _doctorLog_BLL;
            disease_Records_BLL = _disease_Records_BLL;
            patient_BLL = _patient_BLL;
        }

        //获取医院表表
        [HttpGet]
        [Route("GetHost")]
        public IActionResult GetDospital()
        {
            List<Hospital_Model> models = hospital_BLL.GetHospital();

            return Ok(new { data = models });
        }

        //获取科室表
        [HttpGet]
        [Route("GetDepartment")]
        public IActionResult GetDepartment(int hospital_Id=-1)
        {
            List<Department_Model> models = department_BLL.GetShowTable<Department_Model>();
            models = models.Where(p => p.hospital_Id.Equals(hospital_Id)).ToList();

            return Ok(new { data = models });
        }

        //获取科室表
        [HttpGet]
        [Route("GetGrade")]
        public IActionResult GetGrade(int Department_Id=-1)
        {
            List<Grade_Model> models = grade_BLL.GetShowTable<Grade_Model>();
            models = models.Where(p => p.Department_ID.Equals(Department_Id)).ToList();

            return Ok(new { data = models });
        }

        //获取医院管理页面 （问诊次数 医生数量）
        [HttpGet]
        [Route("GetHospList")]
        public IActionResult GetHospList(int page = 1, int limit = 2,string name="")
        {
            List<Hospital_Model> models = hospital_BLL.GetHospList();

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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
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
        /// 诊断报告表
        /// </summary>
        /// <param name="Disease_records_id">诊断记录表ID</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPati_DiagnosisList")]
        public IActionResult GetPati_DiagnosisList(int Disease_records_id)
        {
            List<Patient_Model> models = patient_BLL.GetPati_DiagnosisList(Disease_records_id);

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
        [HttpGet]
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
        [HttpGet]
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







        /// <summary>
        /// 审核管理医生页面方法 判断状态
        /// </summary>
        /// <param name="state">状态 0待审核 1审核通过 2审核失败</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="Years">年限查询</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDoct_State_relationList")]
        public IActionResult GetDoct_State_relationList(int state, int page = 1, int limit = 2, int hospital_Id=-1, int Department_Id=-1, int Grade_Id=-1, int Years = -1)
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetDoct_State_relationList();
            models = models.Where(p => p._state.Equals(state)).ToList();
            if (Years!=-1)
            {
                models = models.Where(p => p.Years.Equals(Years)).ToList();
            }
            if (hospital_Id != -1)
            {
                models = models.Where(p => p.hospital_Id.Equals(hospital_Id)).ToList();
            }
            if (Department_Id != -1)
            {
                models = models.Where(p => p.Department_Id.Equals(Department_Id)).ToList();
            }
            if (Grade_Id != -1)
            {
                models = models.Where(p => p.Grade_Id.Equals(Grade_Id)).ToList();
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

        //审核管理医生页面方法(待审核功能反填)
        [HttpPost]
        [Route("GetDoct_Fan_relationList")]
        public IActionResult GetDoct_Fan_relationList(int id)
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetDoct_State_relationList();
            models = models.Where(p => p.id.Equals(id)).ToList();


            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }


        /// <summary>
        /// 查看未通过审核方法显示原因  两表联查
        /// </summary>
        /// <param name="DoctorLog_id">医生id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDoctorYuan")]
        public IActionResult GetDoctorYuan(int DoctorLog_id)
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetDoctorYuan(DoctorLog_id);
           
            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }


        //通过医生资质方法
        [HttpPost]
        [Route("GetUpdateDoct_true")]
        public IActionResult GetUpdateDoct_true(int DoctorLog_Id)
        {
            int h = doctorLog_BLL.GetUpdateDoct_true(DoctorLog_Id);
            return Ok(new { msg=h });
        }

        //不通过医生资质方法
        [HttpPost]
        [Route("GetUpdateDoct_first")]
        public IActionResult GetUpdateDoct_first(int DoctorLog_Id, string reason)
        {
            int h = doctorLog_BLL.GetUpdateDoct_first(DoctorLog_Id, reason);
            return Ok(new { msg = h });
        }


        //添加医生方法
        [HttpPost]
        [Route("GetAddDoctor")]
        public IActionResult GetAddDoctor(DoctorLog_Model m)
        {
            int h = doctorLog_BLL.GetAddTable(m, "id");
            return Ok(new { msg = h });
        }

        //删除医生方法
        [HttpPost]
        [Route("GetDelDoctor")]
        public IActionResult GetDelDoctor(string id)
        {
            int h = doctorLog_BLL.GetDelTable<DoctorLog_Model>(id,"id");
            return Ok(new { msg = h });
        }
        //修改患者方法
        [HttpPost]
        [Route("GetUpdDoctor")]
        public IActionResult GetUpdDoctor(DoctorLog_Model m)
        {
            int h = doctorLog_BLL.GetUpdateTable(m,"id");
            return Ok(new { msg = h });
        }


        //添加患者方法
        [HttpPost]
        [Route("GetAddPatient")]
        public IActionResult GetAddPatient(Patient_Model m)
        {
            int h = doctorLog_BLL.GetAddTable(m, "id");
            return Ok(new { msg = h });
        }

        //删除患者方法
        [HttpPost]
        [Route("GetDelPatient")]
        public IActionResult GetDelPatient(string id)
        {
            int h = doctorLog_BLL.GetDelTable<Patient_Model>(id, "id");
            return Ok(new { msg = h });
        }
        //修改患者方法
        [HttpPost]
        [Route("GetUpdPatient")]
        public IActionResult GetUpdPatient(Patient_Model m)
        {
            int h = doctorLog_BLL.GetUpdateTable(m, "id");
            return Ok(new { msg = h });
        }



    }
}
