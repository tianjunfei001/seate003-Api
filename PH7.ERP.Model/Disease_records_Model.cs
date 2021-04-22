using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH7.ERP.Model
{
    /// <summary>
    ///    患者看病记录+诊断报告表
    /// </summary>
    
    [Table("Disease_records")]
    public class Disease_records_Model
    {
        public int id { get; set; }                  //id
        public int DoctorId { get; set; }            //所属医生id
        public string describe { get; set; }         //病情描述
        public string diagnosis { get; set; }        //诊断
        public int evaluate { get; set; }            //评价
        public int _money { get; set; }              //金额
        public int seate { get; set; }               //是否接诊
        public DateTime createtime { get; set; }     //创建日期
    }
}
