using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 所属科室表
    /// </summary>
    [Table("Department")]
    public class Department_Model
    {
        public int id{ get; set; }
        public int hospital_Id{ get; set; }
        public string name{ get; set; }
        public string _home { get; set; }
    }
}
