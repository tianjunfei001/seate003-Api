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
        public int id { get; set; }                //医院id
        public string hospitalName { get; set; }   //医院名
        public string _address { get; set; }       //地址
        public int Praise { get; set; }            //好评
        public string _home { get; set; }          //医院电话


        //额外字段
        public int Cycount { get; set; }//医生人数
        public int frequency { get; set; } //问诊次数

    }
}
