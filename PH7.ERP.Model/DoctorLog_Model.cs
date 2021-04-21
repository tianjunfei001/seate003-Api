using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生端登录个人信息表
    /// </summary>
    [Table("DoctorLog")]
    public class DoctorLog_Model
    {
        public int id{ get; set; }
        public string userName{ get; set; }
        public string Name { get; set; }
        public string cellPhone { get; set; }
        public string _password { get; set; }
        public string userImg { get; set; }
        public int _state{ get; set; }
        public string _certificate{ get; set; }
        public DateTime createtime{ get; set; }
        public string _address { get; set; }
        public string university { get; set; }
        public string sex { get; set; }
        public int _identity{ get; set; }
        public int hospital_Id{ get; set; }
        public int Department_Id{ get; set; }
        public int Grade_Id{ get; set; }
    }
}
