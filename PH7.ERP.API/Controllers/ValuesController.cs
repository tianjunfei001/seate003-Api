using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PH7.ERP.Model;

namespace PH7.ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetCe")]
        public IActionResult GetCe()
        {

            string a = Ce_Model.Show();
            return Ok(new { data = a });
        }
    }
}
