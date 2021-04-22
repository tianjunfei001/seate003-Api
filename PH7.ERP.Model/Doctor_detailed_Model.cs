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
        public int id { get; set; }          //id
        public int Doctor_ID { get; set; }          //所属医生id
        public int seate { get; set; }          //状态，收入支出
        public int _money { get; set; }          //金额
        public string _order { get; set; }     //订单明细
        public DateTime createtime { get; set; }     //创建日期
    }
}
