using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PH7.ERP.Model
{
    /// <summary>
    /// 聊天记录表
    /// </summary>
    /// 
    [Table("Disease_Chat")]
    public class Disease_Chat_Model
    {
        public int id{ get; set; }                   //id
        public int Records_Id{ get; set; }           //关联看病记录表
        public string content{ get; set; }           //聊天内容
        public string imgs { get; set; }             //聊天图片
        public int category{ get; set; }             //类别  判断是医生  还是患者说的话
        public DateTime createtime{ get; set; }      //创建日期
    }
}
