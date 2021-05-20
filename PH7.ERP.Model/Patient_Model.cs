using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 患者表
    /// </summary>
    [Table("patient")]
    public class Patient_Model
    {
        public int id { get; set; }                   //id
        public string userName { get; set; }          //用户名
        public string _password { get; set; }         //密码
        public string _phone { get; set; }            //手机号
        public string _tou { get; set; }            //--头像							
        public int integral { get; set; }             //积分
        public int Consultation { get; set; }         //总问诊次数
        public int moneySum { get; set; }             //总支付金额
        public string name { get; set; }              //姓名
        public int age { get; set; }                  //年龄
        public string sex { get; set; }               //性别
        public string height { get; set; }            //身高
        public string _weight { get; set; }           //体重
        public string kidney { get; set; }            //肾功能
        public string marriage { get; set; }          //婚姻
        public string birth { get; set; }             //生育
        public string _case { get; set; }             //病例史
        public string liver_function { get; set; }    //肝功能
        public DateTime createtime { get; set; }      //创建日期


        //额外字段
        public string describe { get; set; }        //病情描述
        public string diagnosis { get; set; }       //诊断报告
        public int _money { get; set; }          //金额

        public int seate { get; set; }               //是否接诊
        public int evaluate { get; set; }            //评价

        //
        public int XuHao { get; set; }          //序号
    }
}
