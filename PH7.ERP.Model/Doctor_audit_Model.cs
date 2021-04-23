using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生审核表
    /// </summary>
    [Table("Doctor_audit")]
    public class Doctor_audit_Model
    {
        public int id { get; set; }              //id
        public int DoctorLog_id { get; set; }    //所属医生id
        public string reason { get; set; }       //不通过原因
        public int _count { get; set; }         //审核次数

        public int seate { get; set; }           //是否通过
        public DateTime createtime { get; set; } //创建日期
                                                 
    }
}
