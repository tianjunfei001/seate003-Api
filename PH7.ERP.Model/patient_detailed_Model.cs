using System;
using System.Collections.Generic;
using System.Text;

namespace PH7.ERP.Model
{
    public class patient_detailed_Model
    {
        public int id { get; set; }
        public int patient_money_ID { get; set; }
        public int seate { get; set; }
        public int _money { get; set; }
        public string _order { get; set; }
        public DateTime createtime { get; set; }
    }
}
