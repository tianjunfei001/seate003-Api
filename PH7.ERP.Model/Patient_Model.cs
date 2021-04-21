using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 患者表
    /// </summary>
    [Table("patient")]
    public class Patient_Model
    {
        public int id{ get; set; }
        public string name{ get; set; }
        public int age{ get; set; }
        public string sex { get; set; }
        public string height { get; set; }
        public string _weight { get; set; }
        public string kidney { get; set; }
        public string marriage { get; set; }
        public string birth { get; set; }
        public string _case { get; set; }
        public string liver_function { get; set; }
        public int DoctorId{ get; set; }
        public string describe { get; set; }
        public string diagnosis { get; set; }
        public string _phone { get; set; }
        public int evaluate{ get; set; }
        public int _money{ get; set; }
        public int seate{ get; set; }
        public DateTime createtime{ get; set; }
    }
}
