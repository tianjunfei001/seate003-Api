using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 流水明细表
    /// </summary>
    [Table("Doctor_detailed")]
   public class Doctor_detailed_Model
    {
        public int id{ get; set; }
        public int Doctor_ID{ get; set; }
        public int seate{ get; set; }
        public int _money{ get; set; }
        public string _order{ get; set; }
        public DateTime createtime{ get; set; }
    }
}
