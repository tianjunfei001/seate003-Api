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
        public int id { get; set; }            //ID
        public int hospital_Id { get; set; }   //所属医院id
        public string name { get; set; }       //科室名称
        public int Praise { get; set; }        //好评
        public string _home { get; set; }      //科室电话
        //额外字段
        public int Cycount { get; set; }//医生人数
        public int frequency { get; set; } //问诊次数
    }
}
