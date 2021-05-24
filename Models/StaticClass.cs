using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homepage.Models
{
    public static class StaticClass
    {
        public static string ConvertNumberToPrice(string xc)
        {
            string result = "";
            int count = 1;
            if (xc.Contains('.'))
            {
                count = 0;
                for (int i = xc.Length - 1; i >= 0; i--)
                {
                    char item = xc[i];
                    if (item == '.')
                    {
                        result += item;
                        count = 1;
                    }
                    else
                    {
                        if(count == 0)
                        {
                            result += item;
                        }
                        else
                        {
                            if (count == 4)
                            {
                                result = item + "," + result;
                                count = 1;
                            }
                            else
                            {
                                result = item + result;
                                count++;
                            }
                        }
                    }
                }
            }
            else
            {
                count = 1;
                for (int i = xc.Length - 1; i >= 0; i--)
                {
                    char item = xc[i];
                    if (count == 4)
                    {
                        result = item + "," + result;
                        count = 1;
                    }
                    else
                    {
                        result = item+ result;
                        count++;
                    }
                }
            }
            
            return result;

        }
    }
}