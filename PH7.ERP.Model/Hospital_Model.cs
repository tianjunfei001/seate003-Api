using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医院表
    /// </summary>
    [Table("hospital")]
    public class Hospital_Model
    {
        public int id{ get; set; }
        public string hospitalName{ get; set; }
        public string _address { get; set; }
        public string _home { get; set; }
    }
}
