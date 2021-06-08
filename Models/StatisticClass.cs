using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    public class StatisticClass
    {
        public List<string> lsMonth { get; set; }
        public List<double> lsIncome { get; set; }
        BookshopEntity db = new BookshopEntity();
        public StatisticClass()
        {
            lsMonth = new List<string>();
            lsIncome = new List<double>();
            DateTime timeNow = DateTime.Today;
            for(int i = 0; i < 10; i++)
            {
                string year = timeNow.Year.ToString();
                string month = timeNow.Month.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                lsMonth.Add(year + "-" + month);
                var lsBill = db.HOADONs.Where(hd => hd.DONHANG.THOIGIAN_DAT.Substring(0, 4) == year &&
                hd.DONHANG.THOIGIAN_DAT.Substring(5, 2) == month).ToList();
                double total = 0;
                if(lsBill==null || lsBill.Count == 0)
                {
                    lsIncome.Add(total);
                }
                else
                {
                    foreach (var item in lsBill)
                    {
                        total += double.Parse(item.TONGTIEN.ToString());
                    }
                    lsIncome.Add(total);
                }
                DateTime newDT = new DateTime();
                if (timeNow.Month -1 == 0)
                {
                    newDT = new DateTime(timeNow.Year - 1, 12, timeNow.Day);
                }
                else
                {
                    newDT = new DateTime(timeNow.Year, timeNow.Month - 1, timeNow.Day);
                }
                timeNow = newDT;
            }
        }
    }
}