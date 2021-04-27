using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 患者钱包表
    /// </summary>
    [Table("patient_money")]
    public class patient_money_Model
    {
        public int id { get; set; }
        public int patient_ID { get; set; }
        public int balance { get; set; }
        public DateTime createtime { get; set; }
    }
}
