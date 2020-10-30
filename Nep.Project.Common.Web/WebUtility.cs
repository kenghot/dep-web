using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Nep.Project.Common.Web
{
    public static class WebUtility
    {
        public static string DisplayErrorMessageInHTML(string[] returnMessage)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (returnMessage != null)
            {
                if (returnMessage.Length > 0)
                {
                    sb.Append("<div class=\"alert alert-danger \" role=\"alert\">");
                    sb.Append("<button type=\"button\" class=\"close\" onclick=\"c2x.closeAlert(this)\"><span aria-hidden=\"true\">&times;</span></button>");
                    sb.Append("<h4>Error!</h4>");
                    sb.Append("<div class=\"alert-container-message\">");
                    sb.Append("<ul>");

                    for (int i = 0; i < returnMessage.Length; i++)
                    {
                        sb.AppendFormat("<li>{0}</li>", returnMessage[i]);
                    }
                    sb.Append("</ul>");
                    sb.Append("</div></div>");
                }
            }

            return sb.ToString();
        }

        public static string DisplayResultMessageInHTML(string[] returnMessage)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (returnMessage != null)
            {
                if (returnMessage.Length > 0)
                {       
                    sb.Append("<ul>");

                    for (int i = 0; i < returnMessage.Length; i++)
                    {
                        sb.AppendFormat("<li>{0}</li>", returnMessage[i]);
                    }
                    sb.Append("</ul>");
                }
            }

            return sb.ToString();
        } 

        public static String ReplaceSingleQuote(String value)
        {
            return value.Replace("'", "''");
        }

        public static String DisplayInHtml(object objData, String formatString, String nullValue)
        {
            String displayText = WebUtility.ConvertToNull(objData, formatString, nullValue);
            return HttpUtility.HtmlEncode(displayText).Replace("" + Convert.ToChar(10), "<br />");
        }

        public static String DisplayInHtmlNoBR(object objData, String formatString, String nullValue)
        {
            String displayText = WebUtility.ConvertToNull(objData, formatString, nullValue);
            return HttpUtility.HtmlEncode(displayText).Replace("" + Convert.ToChar(10), " ");
        }

        public static String DisplayInHtml(object objData)
        {
            if (objData != null)
            {
                if (objData is DateTime || objData is DateTime?)
                {
                    return WebUtility.DisplayInHtml(objData, Common.Constants.UI_FORMAT_DATE_TIME, "-");
                }
                else if (objData is Decimal ||
                                             objData is Decimal? ||
                                             objData is Double)
                {
                    return WebUtility.DisplayInHtml(objData, Common.Constants.UI_FORMAT_DECIMAL, "0.00");
                }
                else if (objData is Int32 ||
                                             objData is Int32? ||
                                             objData is Int64 ||
                                             objData is Int64?)
                {
                    return WebUtility.DisplayInHtml(objData, Common.Constants.UI_FORMAT_INT, "0");
                }
            }

            return WebUtility.DisplayInHtml(objData, "", "-");
        }


        public static String DisplayInForm(object objData, String formatString, String nullValue)
        {
            String displayText = WebUtility.ConvertToNull(objData, formatString, nullValue);
            return displayText;
        }

        public static String DisplayInForm(object objData)
        {
            if (objData != null)
            {
                if (objData is DateTime || objData is DateTime?)
                {
                    return WebUtility.DisplayInForm(objData, Common.Constants.UI_FORMAT_DATE, "");
                }
                else if (objData is Decimal ||
                                             objData is Decimal? ||
                                             objData is Double)
                {
                    return WebUtility.DisplayInForm(objData,  Common.Constants.FORMAT_NUMBER, "0.00");
                }
                //else if (objData is Int32 ||
                //                             objData is Int32? ||
                //                             objData is Int64 ||
                //                             objData is Int64?)
                //{
                //    return WebUtility.DisplayInForm(objData, Constraints.FormatValueInt, "");
                //}
            }
            return WebUtility.DisplayInForm(objData, "", "");
        }
        public static string ConvertToNull(object objData, String formatString, String nullValue)
        {
            if (objData == null)
            {
                return nullValue;
            }
            else
            {
                if (objData is DateTime)
                {
                    if (((DateTime)objData) == DateTime.MinValue)
                        return nullValue;
                }

                if ((objData).ToString().Trim() == "") 
                {
                    return nullValue;
                }
                else
                {
                    if (String.IsNullOrEmpty(formatString))
                    {
                        formatString = "{0}";
                    }
                    else
                    {
                        formatString = "{0:" + formatString + "}";
                    }
                    return String.Format(formatString, objData);
                }
            }

        }


        private static String DecodeInHtml(String objData, String nullValue)
        {
            objData = HttpUtility.HtmlDecode(objData.Replace("<br />", "" + Convert.ToChar(10)));
            objData = (objData == nullValue) ? null : objData;
            return objData;
        }

        public static String DecodeInHtml(String objData)
        {
            return DecodeInHtml(objData, "-");
        }

        public static String ReplaceNewLine(String str)
        {
            return str.Replace("\n", "<br />");
        }

        public static String JavaScriptStringEncode(object objData)
        {
            String text = "";
            if ((objData != null) && (!String.IsNullOrEmpty(objData.ToString())))
            {
                text = HttpUtility.JavaScriptStringEncode(objData.ToString());
            }
            return text;
        }

        public static String ReplaceDashStringToNull(String str)
        {
            return str.Replace("-", "");
        }

        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }

        public static System.IO.Stream LoadReportFile(String reportName)
        {
            System.Reflection.Assembly current = System.Reflection.Assembly.GetCallingAssembly();

            System.Reflection.Assembly assembly = System.Reflection.Assembly.Load("Nep.Project.Report");
            System.IO.Stream stream = assembly.GetManifestResourceStream("Nep.Project.Report." + reportName);
            return stream;
            
        }

        public static String DisplayIDCardNoInHtml(object objData, String nullValue)
        {
            string text = nullValue;
            if (objData != null)
            {
                if (objData is Decimal || objData is Decimal?)
                {
                    text = WebUtility.DisplayInHtmlNoBR(objData, "#-####-#####-##-#", nullValue);
                }else if(objData is String){
                    text = Regex.Replace(objData.ToString(), "(\\d)(\\d\\d\\d\\d)(\\d\\d\\d\\d\\d)(\\d\\d)(\\d)", "$1-$2-$3-$4-$5");
                }
            }

            return text;
        }

        public static String ToBuddhaDateFormat(DateTime? date, String format, String nullValue = "")
        {
            String text = nullValue;
            if(date.HasValue){
                DateTime buddhaDate = (DateTime)date;
                text = buddhaDate.ToString(format, new System.Globalization.CultureInfo("th-TH"));
            }
            return text;
        }
        public static string ToBuddhaYear(string y)
        {
            int iy;
            bool result = int.TryParse(y,out iy);
            if (result)
            {
                if (iy > 1900 && iy < 2099)
                {
                    return (iy + 543).ToString();
                }else
                {
                    return y;
                }
            }else
            {
                return y;
            }
        }
        public static String ToThaiDateDDMMMMYYYY(DateTime? date)
        {
            StringBuilder thaiFormat = new StringBuilder();
            String tempFormat;
           
            DateTime thaiDate;
            if(date.HasValue){
                thaiDate = (DateTime)date;
                tempFormat = thaiDate.ToString("d MMMM yyyy", new System.Globalization.CultureInfo("th-TH"));
                String[] temp = tempFormat.Split(new char[]{' '});

                String strDay = ToThaiNumber(Convert.ToDecimal(temp[0]), "#"); 

                String strYear = ToThaiNumber(Convert.ToDecimal(temp[2]), "####");

                thaiFormat.AppendFormat("{0} {1} {2}", strDay, temp[1], strYear);
            }
           
            

            return thaiFormat.ToString();
        }       

        public static string ToThaiAmountNumber(decimal? num, String format)
        {           
            String thaiFormat = ToThaiNumber(num, format);
            int dotIndex = thaiFormat.IndexOf(".");
            if(dotIndex >= 0){
                String decValue = thaiFormat.Substring(dotIndex+1, 2);
                if(decValue == "๐๐"){
                    thaiFormat = thaiFormat.Replace(".๐๐", ".-");
                }
            }
            return thaiFormat;
        }

        public static string ToThaiNumber(String text, String format)
        {
            String textFormat = "";
            decimal num = 0;
            if(Decimal.TryParse(text, out num)){
                textFormat = ToThaiNumber(num, format);
            }
            return text;
        }

        public static string ToThaiNumber(decimal? num, String format)
        {
            StringBuilder text = new StringBuilder();
            char temp;
            Int32 intTemp;
            String[] nums = ThaiNumbers;
            decimal amount = (num.HasValue) ? (decimal)num : 0;
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

            return text.ToString();
        }

       
      

        public static string ToThaiBath(decimal? num)
        {
            if (num < 10000000 || !num.HasValue)
            {
                return ToThaiBathOld(num);
            }else
            {
                decimal less10m = num.Value - 10000000;
               
                string s = string.Format("{0:##,#0.#0}", num);
                string[] ss = s.Split(',');
                decimal more10m = decimal.Parse(ss[0]);
                decimal rest = num.Value - (more10m * 1000000);
                string srest;
                srest = (rest == 0) ? "บาทถ้วน" :  ToThaiBathOld(rest);

                string smore10m = ToThaiBathOld(more10m);
                smore10m = smore10m.Replace("บาทถ้วน", "ล้าน");
                return smore10m + srest;
            }
            
        }
        public static string ToThaiBathOld(decimal? num)
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
            else if (Convert.ToDouble(bahtTxt) == 1)
                bahtTH = "หนึ่งบาทถ้วน";
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
        public static String ParseToThaiNumber(String text)
        {
            
            if(!String.IsNullOrEmpty(text)){
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


        private static String[] ThaiMonthNames = new String[]
        {   
            "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน",
            "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม"
        };

        private static String[] ThaiNumbers = new String[]
        {   
            "๐", "๑", "๒", "๓", "๔", "๕", "๖", "๗", "๘", "๙"
        };
        
    }
}
