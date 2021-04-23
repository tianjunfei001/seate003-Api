using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 医生端登录个人信息表
    /// </summary>
    [Table("DoctorLog")]
    public class DoctorLog_Model
    {
        public int id { get; set; }                  //医生id
        public string userName { get; set; }         //用户名
        public string Name { get; set; }             //真实姓名
        public string cellPhone { get; set; }        //手机号
        public string _password { get; set; }        //密码
        public string userImg { get; set; }          //头像
        public int _state { get; set; }              //状态 是否有医生资格证
        public string _certificate { get; set; }     //证书图片路径
        public int evaluate { get; set; }            //评价
        public string _address { get; set; }         //家庭住址
        public string university { get; set; }       //大学
        public string sex { get; set; }              //性别
        public string _identity { get; set; }        //身份证号
        public int hospital_Id { get; set; }         //医院id
        public int Department_Id { get; set; }       //医生所属科室
        public int Grade_Id { get; set; }            //医生等级id
        public DateTime createtime { get; set; }     //创建时间

        //额外字段
        public string GradeName { get; set; }       //医生等级名字
        public int Years { get; set; }           //年限
        public int frequency { get; set; }          //问诊次数
        public int balance { get; set; }          //总金额

        //
        public string reason { get; set; }       //资质审核原因



    }
}
