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
        public int Praise { get; set; }
        public string _address { get; set; }
        public string _home { get; set; }


        //额外字段
        public int Cycount { get; set; }//医生人数
        public int frequency { get; set; } //问诊次数

    }
}
