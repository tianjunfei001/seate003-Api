using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 患者银行卡表
    /// </summary>
    [Table("patient_bankcard")]
    public class patient_bankcard_Model
    {
        public int id { get; set; }
        public string BankNumber { get; set; }
        public int patient_moneyId { get; set; }
        public string _password { get; set; }
        public string BankImg { get; set; }
        public DateTime createtime { get; set; }
    }
}
