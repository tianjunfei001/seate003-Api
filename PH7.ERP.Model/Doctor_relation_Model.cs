using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生具体关联表
    /// </summary>
    [Table("Doctor_relation")]
    public class Doctor_relation_Model
    {
        public int id{ get; set; }
        public int Doctor_ID{ get; set; }
        public int Years{ get; set; }
        public int frequency{ get; set; }
        public int seate{ get; set; }
        public DateTime createtime{ get; set; }
    }


}
