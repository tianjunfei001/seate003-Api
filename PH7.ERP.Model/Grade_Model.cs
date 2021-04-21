using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 所属医生等级表
    /// </summary>
    [Table("Grade")]
    public class Grade_Model
    {
        public int id{ get; set; }
        public int Department_ID{ get; set; }
        public string name{ get; set; }
    }
}
