using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PH7.ERP.BLL;
using PH7.ERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;


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
        IWebHostEnvironment webHost;
        

        //构造函数
        public BackstageController(Hospital_BLL _BLL, Department_BLL _department_BLL, Grade_BLL _grade_BLL, DoctorLog_BLL _doctorLog_BLL, Disease_records_BLL _disease_Records_BLL, Patient_BLL _patient_BLL,IWebHostEnvironment web)
        {
            hospital_BLL = _BLL;
            department_BLL = _department_BLL;
            grade_BLL = _grade_BLL;
            doctorLog_BLL = _doctorLog_BLL;
            disease_Records_BLL = _disease_Records_BLL;
            patient_BLL = _patient_BLL;
            webHost = web;
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
            if (hospital_Id>-1)
            {
                models = models.Where(p => p.hospital_Id.Equals(hospital_Id)).ToList();
            }
            

            return Ok(new { data = models });
        }

        //获取科室表
        [HttpGet]
        [Route("GetGrade")]
        public IActionResult GetGrade(int Department_Id=-1)
        {
            List<Grade_Model> models = grade_BLL.GetShowTable<Grade_Model>();
            if (Department_Id>-1)
            {
                models = models.Where(p => p.Department_ID.Equals(Department_Id)).ToList();
            }
            

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
        [HttpGet]
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
        [HttpGet]
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
        [HttpGet]
        [Route("GetUpdateDoct_true")]
        public IActionResult GetUpdateDoct_true(int DoctorLog_Id)
        {
            int h = doctorLog_BLL.GetUpdateDoct_true(DoctorLog_Id);
            return Ok(new { msg=h });
        }

        //不通过医生资质方法
        [HttpGet]
        [Route("GetUpdateDoct_first")]
        public IActionResult GetUpdateDoct_first(int DoctorLog_Id, string reason)
        {
            int h = doctorLog_BLL.GetUpdateDoct_first(DoctorLog_Id, reason);
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

       
        //修改患者方法
        [HttpPost]
        [Route("GetUpdPatient")]
        public IActionResult GetUpdPatient(Patient_Model m)
        {
            int h = doctorLog_BLL.GetUpdateTable(m, "id");
            return Ok(new { msg = h });
        }

        //查看医生账号管理
        [HttpGet]
        [Route("GetShowDoctorList")]
        public IActionResult GetShowDoctorList(string name = "", string phone = "", int page = 1, int limit = 5)
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetShowDoctorList();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.userName.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(phone))
            {
                models = models.Where(p => p.cellPhone.Contains(phone)).ToList();
            }
            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                code = 0,
                msg = "",
                data = _models,
                count = models.Count
            });
        }


        //添加医生方法
        [HttpPost]
        [Route("GetAddDoctor")]
        public IActionResult GetAddDoctor(DoctorLog_Model m)
        {
            if (m.id == 0)
            {
                int h = doctorLog_BLL.GetAddDoctors(m);
                return Ok(new { msg = h > 0 ? true : false, mrg = h > 0 ? "添加成功！" : "添加失败！" });
            }
            else
            {
                int h = doctorLog_BLL.GetupdDoctors(m);
                return Ok(new { msg = h > 0 ? true : false, mrg = h > 0 ? "修改成功！" : "修改失败！" });
            }
        }
        //医生账号管理反填
        [HttpGet]
        [Route("GetFanDoctor")]
        public IActionResult GetFanDoctor(int id)
        {
            var list = doctorLog_BLL.GetFanDoctor(id);
            return Ok(list);
        }
        //修改患者方法
        [HttpPost]
        [Route("GetupdPatient")]
        public IActionResult GetupdPatient(Patient_Model m)
        {
            int h = patient_BLL.GetupdPatient(m);
            return Ok(new { msg = h > 0 ? true : false, mrg = h > 0 ? "修改成功！" : "修改失败！" });
        }
        //医生账号患者反填
        [HttpGet]
        [Route("GetFanPatient")]
        public IActionResult GetFanPatient(int id)
        {
            var list = patient_BLL.GetFanPatient(id);
            return Ok(list);
        }

        //查看患者账号管理
        [HttpGet]
        [Route("GetShowPatientList")]
        public IActionResult GetShowPatientList(string name = "", string phone = "", int page = 1, int limit = 5)
        {
            List<Patient_Model> models = patient_BLL.GetShowPatientList();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.userName.Contains(name)).ToList();
            }
            if (!string.IsNullOrEmpty(phone))
            {
                models = models.Where(p => p._phone.Contains(phone)).ToList();
            }
            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                code = 0,
                msg = "",
                data = _models,
                count = models.Count
            });
        }
        //删除医生方法
        [HttpGet]
        [Route("GetDelDoctor")]
        public IActionResult GetDelDoctor(string id)
        {
            int h = doctorLog_BLL.GetDelTable<DoctorLog_Model>(id, "id");
            return Ok(new { msg = h });
        }


        //-------------账号管理 田俊飞
        /// <summary>
        /// 获取医生管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDoctorLogList")]
        public IActionResult GetDoctorLogList(int page = 1, int limit = 5,string name = "")
        {


            List<DoctorLog_Model> models = doctorLog_BLL.GetDoctorLogList();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.Name.Contains(name)).ToList();
            }
            for (int i = 0; i < models.Count; i++)
            {
                models[i].XuHao = i +1;
            }
            var _models = models.Skip((page - 1) * limit).Take(limit);
            
            return Ok(new
            {
                code = 0,
                msg = "",
                data = _models,
                count = models.Count
            });

        }


        /// <summary>
        /// 添加修改医生管理方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAddDoctorLog")]
        public IActionResult GetAddDoctorLog()
        {


            var www = webHost.ContentRootPath; //获取更目录
            var phat = $"{www}/wwwroot/imgs/";   //保存文件夹所在的位置

            if (!System.IO.Directory.Exists(phat))  //判断文件夹是否存在
            {
                System.IO.Directory.CreateDirectory(phat);  //创建一个文件夹
            }

            //创建一个MOdel
            DoctorLog_Model m = new DoctorLog_Model();
            m.id = Convert.ToInt32(Request.Form["id"]);
            m.userName = Request.Form["userName"];
            m.Name= Request.Form["Name"];
            m.cellPhone= Request.Form["cellPhone"];
            m.Grade_Id= Convert.ToInt32(Request.Form["Grade_Id"]);
            m.Department_Id= Convert.ToInt32(Request.Form["Department_Id"]);
            m.hospital_Id=Convert.ToInt32(Request.Form["hospital_Id"]);
            m._password= Request.Form["_password"];
            m._certificate = Request.Form["_certificate2"];
            m.identity_img = Request.Form["identity_img2"];
            m.Practice_img = Request.Form["Practice_img2"];
            //文件
            var files = Request.Form.Files;       
            

            if (m.id!=0)
            {
                //删除文件夹中图片
               
                
                

                //修改方法
                //遍历
                foreach (var item in files)
                {
                    //名字
                    var fileName = item.FileName;
                    if (item.Name.Equals("_certificate"))
                    {
                        if (m._certificate != null && !System.IO.Directory.Exists($"{www}/wwwroot/" + m._certificate))
                        {
                            var pat = $"{www}/wwwroot/" + m._certificate;
                            System.IO.File.Delete(pat);
                        }
                        m._certificate = "imgs/zhen" + item.FileName;
                        fileName = "zhen" + fileName;
                    }
                    if (item.Name.Equals("identity_img"))
                    {
                        if (m.identity_img != null && !System.IO.Directory.Exists($"{www}/wwwroot/" + m.identity_img))
                        {
                            var pat = $"{www}/wwwroot/" + m.identity_img;
                            System.IO.File.Delete(pat);
                        }
                        m.identity_img = "imgs/sheng" + item.FileName;
                        fileName = "sheng" + fileName;
                    }
                    if (item.Name.Equals("Practice_img"))
                    {
                        if (m.Practice_img != null && !System.IO.Directory.Exists($"{www}/wwwroot/" + m.Practice_img))
                        {
                            var pat = $"{www}/wwwroot/" + m.Practice_img;
                            System.IO.File.Delete(pat);
                        }
                        m.Practice_img = "imgs/zhi" + item.FileName;
                        fileName = "zhi" + fileName;
                    }
                    //数据流上传
                    using (System.IO.Stream sr = System.IO.File.Create($"{phat}{fileName}"))
                    {
                        item.CopyTo(sr);
                    }
                    


                }
                int h = doctorLog_BLL.GetUpdDoctorLog(m);
                return Ok(new { state = h >= 1 ? true : false, msg = h >= 1 ? "修改成功！" : "修改失败！" });
            }
            else
            {
                //添加方法
                //遍历
                foreach (var item in files)
                {
                    //名字
                    var fileName = item.FileName;
                    if (item.Name.Equals("_certificate"))
                    {
                        m._certificate = "imgs/zhen" + item.FileName;
                        fileName = "zhen" + fileName;
                    }
                    if (item.Name.Equals("identity_img"))
                    {
                        m.identity_img = "imgs/sheng" + item.FileName;
                        fileName = "sheng" + fileName;
                    }
                    if (item.Name.Equals("Practice_img"))
                    {
                        m.Practice_img = "imgs/zhi" + item.FileName;
                        fileName = "zhi" + fileName;
                    }
                    //数据流上传
                    using (System.IO.Stream sr = System.IO.File.Create($"{phat}{fileName}"))
                    {
                        item.CopyTo(sr);
                    }


                }
                int h = doctorLog_BLL.GetAddDoctorLog(m);
                return Ok(new { state = h >= 1 ? true : false,msg=h>=1?"添加成功！":"添加失败！" }) ;
            }

           

            


        }

        //反填医生方法
        [HttpGet]
        [Route("GetFanDoctorLog")]
        public IActionResult GetFanDoctorLog(int id)
        {
            List<DoctorLog_Model> models = doctorLog_BLL.GetDoctorLogList();

            DoctorLog_Model m = models.Where(p => p.id.Equals(id)).FirstOrDefault();
            return Ok(new
            {
                code = 0,
                msg = "",
                data = m,
                count = models.Count
            });
        }

        //删除医生方法
        [HttpGet]
        [Route("GetDelDoctorLog")]
        public IActionResult GetDelDoctorLog(string id)
        {
            int h = doctorLog_BLL.GetDelTable<DoctorLog_Model>(id, "id");
            return Ok(new { msg = h });
        }


        /// <summary>
        /// 获取患者管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPatient_List")]
        public IActionResult GetPatient_List(int page = 1, int limit = 5, string name = "")
        {


            List<Patient_Model> models = patient_BLL.GetShowTable<Patient_Model>();

            if (!string.IsNullOrEmpty(name))
            {
                models = models.Where(p => p.userName.Contains(name)).ToList();
            }
            for (int i = 0; i < models.Count; i++)
            {
                models[i].XuHao = i + 1;
            }
            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                code = 0,
                msg = "",
                data = _models,
                count = models.Count
            });

        }

        /// <summary>
        /// 反填患者
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFanPatient_List")]
        public IActionResult GetFanPatient_List(int id)
        {
            List<Patient_Model> patients = patient_BLL.GetShowTable<Patient_Model>();
            Patient_Model patient = patients.Where(p => p.id.Equals(id)).FirstOrDefault();
            return Ok(new
            {
                code = 0,
                msg = "",
                data = patient,
                count = patients.Count
            });
        }

        /// <summary>
        /// 修改患者方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUpdPatient_List")]
        public IActionResult GetUpdPatient_List(Patient_Model model)
        {
            int h = patient_BLL.GetupdPatient(model);
            return Ok(new { state = h >= 1 ? true : false, msg = h >= 1 ? "修改成功！" : "修改失败！" });
        }

        //删除患者方法
        [HttpGet]
        [Route("GetDelPatient")]
        public IActionResult GetDelPatient(string id)
        {
            int h = patient_BLL.GetDelTable<Patient_Model>(id, "id");
            return Ok(new { msg = h });
        }

    }
}
