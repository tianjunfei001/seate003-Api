using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PH7.ERP.BLL;
using PH7.ERP.Model;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Web;

namespace PH7.ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorLogController : ControllerBase
    {

        //注入
        Hospital_BLL hospital_BLL;
        Department_BLL department_BLL;
        Grade_BLL grade_BLL;
        DoctorLog_BLL doctorLog_BLL;
        Disease_records_BLL disease_Records_BLL;
        Patient_BLL patient_BLL;
        Doctor_detailed_BLL doctor_Detailed_Bll;
        Doctor_money_BLL doctor_Money_BLL;


        //构造函数
        //构造函数
        public DoctorLogController(Hospital_BLL _BLL, Department_BLL _department_BLL, Grade_BLL _grade_BLL, DoctorLog_BLL _doctorLog_BLL, Disease_records_BLL _disease_Records_BLL, Patient_BLL _patient_BLL, Doctor_detailed_BLL _doctor_Detailed_Bll, Doctor_money_BLL _doctor_Money_BLL)
        {
            hospital_BLL = _BLL;
            department_BLL = _department_BLL;
            grade_BLL = _grade_BLL;
            doctorLog_BLL = _doctorLog_BLL;
            disease_Records_BLL = _disease_Records_BLL;
            patient_BLL = _patient_BLL;
            doctor_Detailed_Bll = _doctor_Detailed_Bll;
            doctor_Money_BLL = _doctor_Money_BLL;
        }
        /////////医生端接诊台
        /// 默认未接诊
        [HttpGet]
        [Route("GetDis")]
        public IActionResult GetDis(int page = 1, int limit = 2)
        {
            List<Disease_records_Model> models = doctorLog_BLL.Get_Records();

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        //已接诊
        [HttpGet]
        [Route("GetDyes")]
        public IActionResult GetDyes(int page = 1, int limit = 2)
        {
            List<Disease_records_Model> models = doctorLog_BLL.Get_yes();

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }
        //诊断管理页
        [HttpGet]
        [Route("Getzd")]
        public IActionResult Getzd(int page = 1, int limit = 2)
        {
            List<Disease_records_Model> models = doctorLog_BLL.Get_zdgl();

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }

        //健康档案反填
        [HttpGet]
        [Route("GetFt")]
        public IActionResult GetFt(string id)
        {
            var models = doctorLog_BLL.Get_By(id);
            return Ok(new { data = models });
        }
        //诊断列表
        [HttpGet]
        [Route("GetAlist")]
        public IActionResult GetAlist(string sickdate, int page = 1, int limit = 2)
        {
            List<Disease_records_Model> models = doctorLog_BLL.Get_Administrationlist(sickdate);

            var _models = models.Skip((page - 1) * limit).Take(limit);

            return Ok(new
            {
                data = models,
                code = 0,
                msg = "",
                count = models.Count
            });
        }
        //好评反
        [HttpGet]
        [Route("GetAcclim")]
        public IActionResult GetAcclim(string id)
        {
            var models = doctorLog_BLL.Get_Acclaim(id);
            return Ok(new { data = models, code = 0 });
        }
        //接诊改
        [HttpGet]
        [Route("GetUpdrn")]
        public IActionResult GetUpdrn(string id)
        {
            var models = doctorLog_BLL.Get_Reception(id);
            return Ok(new { data = models, code = 0 });
        }
        /// <summary>
        /// 账号密码 登录方法
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDoctLog")]
        public IActionResult GetDoctLog(string userName, string password)
        {
            var list = doctorLog_BLL.GetDoctorLog(userName, password);

            if (list != null)
            {
                return Ok(new { _list = true, msg = $"尊贵的V10{list.userName}先生欢迎来到了第10区", data = list });
            }
            else
            {
                return Ok(new { _list = false, msg = $"用户名或密码错误！！！", data = "" });
            }
        }


        /// <summary>
        /// 手机号登录 判断是否成功
        /// </summary>
        /// <param name="cellPhone">手机号码</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDoctLogHome")]
        public IActionResult GetDoctLogHome(string cellPhone)
        {
            int h = doctorLog_BLL.GetDoctLog_phone(cellPhone);

            return Ok(new { seate = h });
        }


        /// <summary>
        /// 医生端注册页面 注册方法
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDoctLog_Zhuci")]
        public IActionResult GetDoctLog_Zhuci(DoctorLog_Model m)
        {
            int h = doctorLog_BLL.GetDoctLog_Zhuci(m);

            return Ok(new { seate = h });
        }


        /// <summary>
        /// 接诊台显示查询 ——平台
        /// </summary>
        /// <param name="Sname">查询患者姓名手机号</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPlatform")]
        public IActionResult GetPlatform(int pageindex, int pagesize, string State, string Sname)
        {
            var list = doctorLog_BLL.GetDoctor_Platform(State, Sname);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, count = list.Count });
        }

        /// <summary>
        /// 流水明细
        /// </summary>
        /// <param name="Doctor_ID"></param>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetDoctor_detailed")]
        public IActionResult GetDoctor_detailed(int Doctor_ID, int limit = 10, int page = 1)
        {
            List<Doctor_detailed_Model> list = doctor_Detailed_Bll.GetDoctor_detailed(Doctor_ID);
            //求和

            var _list = list.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new { code = 0, data = _list, count = list.Count, });
        }
        /// <summary>
        /// 获取用户余额
        /// </summary>
        /// <param name="Doctor_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Getbalance")]
        public IActionResult Getbalance(int Doctor_ID)
        {
            List<Doctor_money_Model> list = doctor_Money_BLL.GetDoctor_money(Doctor_ID);
            return Ok(new { data = (list[0].balance).ToString() });
        }
        /// <summary>
        /// 获取医生绑定的银行卡
        /// </summary>
        /// <param name = "id" ></ param >
        /// < param name="Doctor_ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBindCardlist")]
        public IActionResult GetBindCardlist(int id, int Doctor_ID)
        {
            //int id, int Doctor_ID
            List<Doctor_bankcard_Model> list = doctor_Money_BLL.GetBindBank( id,  Doctor_ID);
            return Ok(new { code = 0, data = list });
        }



        /// <summary>
        /// 医生端个人信息显示
        /// </summary>
        /// <param name="Personal">个人</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPersonal")]
        public IActionResult GetPersonal()
        {
            var list = doctorLog_BLL.GetDoctor_Personal();
            return Ok(new { data = list });
        }
        /// <summary>
        /// 医生端密码信息修改
        /// </summary>
        /// <param name="Personal">个人</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPassword")]
        public IActionResult GetPassword(string Mima, string number)
        {
            var list = doctorLog_BLL.ChangePassword(Mima, number);
            return Ok(new { data = list > 0 ? "修改密码成功" : "修改密码失败", msg = list > 0 ? true : false });
        }
        /// <summary>
        /// 医生端修改用户密码 
        /// </summary>
        /// <param name="ChangeUser">改用户</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetChangeUser")]

        public IActionResult GetChangeUser(string GetUser, string Mima, string number)
        {
            var list = doctorLog_BLL.GetModifyUser(GetUser, Mima, number);
            return Ok(new { data = list > 0 ? "修改成功" : "修改失败", msg = list > 0 ? true : false });
        }
        /// <summary>
        /// 医生端诊断管理
        /// </summary>
        /// <param name="Management">管理</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetManagement")]

        public IActionResult GetManagement(int pageindex, int pagesize, string GetName)
        {
            var list = doctorLog_BLL.GetDiagnosis(GetName);
            var _list = list.Skip((pageindex - 1) * pagesize).Take(pagesize);
            return Ok(new { data = _list, count = list.Count });
        }





        //手机号登录
        /// ========================================程序配置参数区开始

        //接口生产地址(应用上线后正式环境必须使用该地址)
        //private  static String url = "http://www.etuocloud.com/gateway.action";

        //接口测试地址（未上线前测试环境使用）
        private static String url = "http://www.etuocloud.com/gatetest.action";

        //应用 app_key
        private static String APP_KEY = "QqDVNr4nLTYHZMW9fp05pHgO8DOsydp2";
        //应用 app_secret
        private static String APP_SECRET = "s3J9XuaHVbJPSLeP4uZaZnyqrJNpWplLN6oUvyOgzj57vvmcK72KMctKqmuPU6lP";

        //接口响应格式 json或xml
        private static String FORMAT = "json";

        /// ========================================程序配置参数区结束

        //验证码方法
        [HttpGet]
        [Route("GetYan")]
        public IActionResult GetYan(string tel)
        {
            Random random = new Random();
            var sui = random.Next(1000, 9999).ToString() + "F";

            //发送短信验证码
            string yan = sendSmsCode(tel, 1, sui);

            return Ok(new { msg = yan, yan = sui });
        }

        /// <summary>
        /// 发生短信验证码
        /// </summary>
        /// <param name="to">手机号</param>
        /// <param name="template">短信模板ID</param>
        /// <param name="smscode">验证码</param>
        /// <returns></returns>
        public static string sendSmsCode(string to, int template, string smscode)
        {

            NameValueCollection parameters = new NameValueCollection();
            parameters.Add("app_key", APP_KEY);
            parameters.Add("view", FORMAT);
            parameters.Add("method", "cn.etuo.cloud.api.sms.simple");
            parameters.Add("out_trade_no", "");//商户订单号，可空
            parameters.Add("to", to);
            parameters.Add("template", template.ToString());
            parameters.Add("smscode", smscode);
            parameters.Add("sign", getsign(parameters));
            return HttpClient.HttpPost(url, parameters);

        }

        /// <summary>
        /// 获取param签名
        /// </summary>
        /// <param name="sParams"></param>
        /// <returns></returns>
        private static string getsign(NameValueCollection parameters)
        {
            SortedDictionary<string, string> sParams = new SortedDictionary<string, string>();
            foreach (string key in parameters.Keys)
            {
                sParams.Add(key, parameters[key]);
            }

            string sign = string.Empty;
            StringBuilder codedString = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sParams)
            {
                if (temp.Value == "" || temp.Value == null || temp.Key.ToLower() == "sign")
                {
                    continue;
                }

                if (codedString.Length > 0)
                {
                    codedString.Append("&");
                }
                codedString.Append(temp.Key.Trim());
                codedString.Append("=");
                codedString.Append(temp.Value.Trim());
            }

            // 应用key
            codedString.Append(APP_SECRET);
            string signkey = codedString.ToString();
            sign = GetMD5(signkey, "utf-8");

            return sign;
        }

        //md5
        private static string GetMD5(string encypStr, string charset)
        {
            string retStr;
            MD5CryptoServiceProvider m5 = new MD5CryptoServiceProvider();

            //创建md5对象
            byte[] inputBye;
            byte[] outputBye;

            //使用XXX编码方式把字符串转化为字节数组．
            try
            {
                inputBye = Encoding.GetEncoding(charset).GetBytes(encypStr);
            }
            catch (Exception)
            {
                inputBye = System.Text.Encoding.UTF8.GetBytes(encypStr);
            }
            outputBye = m5.ComputeHash(inputBye);

            retStr = System.BitConverter.ToString(outputBye);
            retStr = retStr.Replace("-", "").ToUpper();

            //  return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(ConvertString, "MD5").ToLower(); ;

            return retStr;
        }

    }

    /// <summary>
    /// 短信帮助类
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// POST请求与获取结果  
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string HttpPost(string Url, NameValueCollection parameters)
        {
            return HttpPost(Url, toParaData(parameters));
        }



        //调用http接口,接口编码为utf-8
        private static string toParaData(NameValueCollection parameters)
        {

            //设置参数，并进行URL编码
            StringBuilder codedString = new StringBuilder();
            foreach (string key in parameters.Keys)
            {
                // codedString.Append(HttpUtility.UrlEncode(key));
                codedString.Append(key);
                codedString.Append("=");
                codedString.Append(HttpUtility.UrlEncode(parameters[key], System.Text.Encoding.UTF8));
                codedString.Append("&");
            }
            string paraUrlCoded = codedString.Length == 0 ? string.Empty : codedString.ToString().Substring(0, codedString.Length - 1);


            return paraUrlCoded;
        }


        /// <summary>  
        /// POST请求与获取结果  
        /// </summary>  
        public static string HttpPost(string Url, string postDataStr)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            //request.ContentLength = postDataStr.Length;
            //StreamWriter writer = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.UTF8);
            // writer.Write(postDataStr);
            // writer.Flush();


            //将URL编码后的字符串转化为字节
            byte[] payload = System.Text.Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = payload.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            //获得响应流
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));

            string retString = reader.ReadToEnd();
            return retString;
        }



    }
}
