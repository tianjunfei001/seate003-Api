using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生具体关联表
    /// </summary>
    [Table("Doctor_relation")]
    public class Doctor_relation_Model
    {
        public int id { get; set; }          //id
        public int Doctor_ID { get; set; }          //所属医生id
        public int Years { get; set; }          //年限医龄
        public int frequency { get; set; }          //问诊次数
        public int seate { get; set; }          //状态 是否开始接诊
        public int _money { get; set; }          //金额
        public DateTime createtime { get; set; }     //创建日期
    }


}
