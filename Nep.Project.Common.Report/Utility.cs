using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Common.Report
{
    public static class Utility
    {
        /// <summary>
        /// สำหรับแสดงวันที่ไทย เช่น 01 มกราคม 2558
        /// </summary>
        public static String ToBuddhaDateFormat(DateTime? date, String format, String nullValue = "")
        {
            String text = nullValue;
            if (date.HasValue)
            {
                DateTime buddhaDate = (DateTime)date;
                text = buddhaDate.ToString(format, new System.Globalization.CultureInfo("th-TH"));
            }
            return text;
        }

        /// <summary>
        /// สำหรับแสดงวันที่ไทย เช่น ๐๑ มกราคม ๒๕๕๘
        /// </summary>       
        public static String ToThaiDateFormat(DateTime? date, String format, String nullValue = "")
        {
            StringBuilder thaiFormat = new StringBuilder();
            String tempFormat;

            DateTime thaiDate;
            String[] nums = ThaiNumbers;
            Char[] temp;
            Int32 tempNum;
            String tempText;
            if (date.HasValue)
            {
                thaiDate = (DateTime)date;
                tempFormat = thaiDate.ToString(format, new System.Globalization.CultureInfo("th-TH"));
                temp = tempFormat.ToCharArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    tempText = temp[i].ToString();
                    if (Int32.TryParse(tempText, out tempNum))
                    {
                        thaiFormat.Append(nums[tempNum]);
                    }
                    else
                    {
                        thaiFormat.Append(tempText);
                    }
                }
            }
            else
            {
                thaiFormat.Append(nullValue);
            }
            return thaiFormat.ToString();
        }        

        /// <summary>
        /// สำหรับแสดงตัวเลขไทย เช่น 2558 เป็น ๒๕๕๘ หรือ 8000.50 เป็น ๘,๐๐๐.๕๐
        /// </summary> 
        public static string ToThaiNumber(object num, String format, String nullValue = "")
        {
            StringBuilder text = new StringBuilder();
            char temp;
            Int32 intTemp;
            String[] nums = ThaiNumbers;
            if (num != null)
            {
                decimal amount = 0;
                if((num is Decimal || num is Decimal? || num is Double || num is Double?)){
                    amount = (decimal)num;
                }
                else if ((num is Int32 || num is Int32? || num is Int64 || num is Int64?))
                {
                    amount = (Int32)num;
                }else if(Decimal.TryParse(num.ToString(), out amount)){

                }

                String numFormat = amount.ToString(format);
                Char[] chNumFormat = numFormat.ToCharArray();
                for (int i = 0; i < chNumFormat.Length; i++)
                {
                    temp = chNumFormat[i];
                    intTemp = 0;
                    if (Int32.TryParse(temp.ToString(), out intTemp))
                    {
                        text.Append(nums[intTemp]);
                    }
                    else
                    {
                        text.Append(temp);
                    }
                }

                if (String.IsNullOrEmpty(text.ToString()))
                {
                    text.Append(nullValue);
                }
            }    
            return text.ToString();
        }

        /// <summary>
        /// สำหรับแสดงคำอ่านจำนวนเงิน เช่น "แปดพันบาทห้าสิบสตางค์"
        /// </summary> 
        public static string ToThaiBath(decimal? num)
        {
            decimal amount = (num.HasValue) ? (decimal)num : 0;
            string bahtTxt, n, bahtTH = "";
            bahtTxt = amount.ToString("####.00");
            string[] nums = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] ranks = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += nums[Convert.ToInt32(n)];
                        bahtTH += ranks[(intVal.Length - i) - 1];
                    }
                }

                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += nums[Convert.ToInt32(n)];
                            bahtTH += ranks[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        private static String GetThaiNumber(int num)
        {
            StringBuilder thaiNum = new StringBuilder();
            Char[] ch = num.ToString().ToCharArray();
            int temp;
            for (int i = 0; i < ch.Length; i++)
            {
                temp = Convert.ToInt32(ch[i].ToString());
                thaiNum.Append(ThaiNumbers[temp]);
            }
            return thaiNum.ToString();
        }

        private static String[] ThaiNumbers = new String[]
        {   
            "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙"
        };
        public static String ParseToThaiNumber(String text)
        {

            if (!String.IsNullOrEmpty(text))
            {
                String[] nums = ThaiNumbers;
                StringBuilder newText = new StringBuilder();
                Char[] ch = text.ToString().ToCharArray();
                Char temp;
                int intTemp;
                for (int i = 0; i < ch.Length; i++)
                {
                    temp = ch[i];
                    intTemp = 0;
                    if (Int32.TryParse(temp.ToString(), out intTemp))
                    {
                        newText.Append(nums[intTemp]);
                    }
                    else
                    {
                        newText.Append(temp);
                    }
                }
                text = newText.ToString();
            }

            return text;

        }
    }
}
