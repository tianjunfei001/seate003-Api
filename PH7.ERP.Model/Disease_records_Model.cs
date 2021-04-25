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
        public int Doctor_Id { get; set; }            //所属医生id
        public int patient_Id { get; set; }            //所属患者id
        
        public string describe { get; set; }         //病情描述
        public string diagnosis { get; set; }        //诊断
        public int evaluate { get; set; }            //评价
        public int _money { get; set; }              //金额
        public int seate { get; set; }               //是否接诊
        public DateTime createtime { get; set; }     //创建日期

        //额外字段


        public string name { get; set; }               //患者真实姓名
        public string Grade_Name { get; set; }         //医生等级名称
        public int hospital_Id { get; set; }        //所属医院id
        public string hospitalName { get; set; }       //医院名称

        public string userName { get; set; }        //医生账户名
        public string Doctor_Name { get; set; }        //医生真实名

        //Hcm添加额外字段
        public int age { get; set; }  //年龄
        public int Total { get; set; }   //总金额
        public int Patients { get; set; } //总患者数量
        public string Tim { get { return createtime.ToString("yyyy-MM-dd"); } set { } } //总患者数量

    }
}
