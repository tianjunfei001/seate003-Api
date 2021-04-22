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
        public int id { get; set; }                 //id
        public int Doctor_ID { get; set; }          //所属医生id
        public int balance { get; set; }            //余额
        public int seate { get; set; }             //是否绑定银行卡
        public DateTime createtime { get; set; }  //创建日期
    }
}
