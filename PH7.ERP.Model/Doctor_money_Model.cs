using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生金额表
    /// </summary>
    [Table("Doctor_money")]
    public class Doctor_money_Model
    {
        public int id{ get; set; }
        public int Doctor_ID{ get; set; }
        public int balance{ get; set; }
        public DateTime createtime{ get; set; }
    }
}
