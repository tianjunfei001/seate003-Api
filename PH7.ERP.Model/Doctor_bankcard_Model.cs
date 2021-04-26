using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 银行卡信息
    /// </summary>
    /// 
    [Table("Doctor_bankcard")]
    public class Doctor_bankcard_Model
    {
        public int id { get; set; }                  //id
        public string Cardholder { get; set; }       //持卡人
        public string ID_number { get; set; }        //身份证号码
        public DateTime outTime { get; set; }        //身份证有效期     
        public string BankNumber { get; set; }       //银行卡号
        public string phone { get; set; }            //手机号
        public int Doctor_moneyId { get; set; }      //医生金额表Id
        public string _password { get; set; }        //银行卡密码
        public DateTime createtime { get; set; }     //创建日期 

        public string strcreatetime { get { return createtime.ToString("yyyy-MM-dd"); } set { } }     //创建日期 

    }
}
