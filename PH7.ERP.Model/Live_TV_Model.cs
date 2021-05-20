using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PH7.ERP.Model
{
    //直播表
    [Table("Live_TV")]
    public class Live_TV_Model
    {
        public int id{ get; set; }                     //--id //直播间号
        public int DoctorLog_id{ get; set; }           //--所属医生id
        public string Keimg{ get; set; }                  //--课程封面
        public string title { get; set; }                  //--课程标题
        public int watch_number{ get; set; }           //--观看人数
        public int Collection_number{ get; set; }      //--收藏人数	
        public int give_number{ get; set; }            //--点赞人数
        public DateTime StartTime{ get; set; }              //--开始时间
        public DateTime EndTime { get; set; }                //--结束时间


        //
        public int XuHao { get; set; }              //序号

    }
}
