using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PH7.ERP.BLL;
using PH7.ERP.Model;

namespace PH7.ERP.API.Controllers
{
    //[EnableCors("cors")] //默认允许方法
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        //注入
        Hospital_BLL bLL;

        //构造函数
        public HospitalController(Hospital_BLL _BLL)
        {
            bLL = _BLL;
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
        [HttpGet]
        [Route("GetHospList")]
        public IActionResult GetHospList()
        {
            List<Hospital_Model> models = bLL.GetHospList();

            return Ok(new { data = models });
        }


    }

}
