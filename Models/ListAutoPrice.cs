using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    public class ListAutoPrice
    {
        public string ngayApDung { get; set; }
        public double giaTri { get; set; }
        public string tangGiam { get; set; }
        public string donVi { get; set; }
        public List<SACH> lsDepend { get; set; }

        public string minDate { get; set; }
        public ListAutoPrice()
        {
            lsDepend = new List<SACH>();
        }
    }
}