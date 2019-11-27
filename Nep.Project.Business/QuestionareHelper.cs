using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Business
{
    public static class QuestionareHelper
    {
        public static string QuestionareJS(decimal? projID ,string QNGroup ,string elementPanel, string elementHDFDisable, string appendJS,string afterBinding)
        {
            string js = "";
            js = @" //console.log('getting data');
                var QuestionareBindingControls = [];
                var QuestionareModel = {};
                QuestionareBindingControls = [];
                 var xxx ;
                //$('[data-bind]').attr('data-bind',function(a,b) {QuestionareBindingControls.push(b.split(':')[1].trim());})
                //$('[data-bind]').attr('data-bind',function(a,b) {QuestionareBindingControls.push(b.trim());})
                $('[data-bind]').each(function(a,b) { QuestionareBindingControls.push($(this).attr('data-bind') + '|' + $(this).attr('type') );})
                $.ajax({ 
                url: '/questionarehandler/getquestionare',
                datatype: 'JSON',
                type: 'post'," +
                string.Format("data: JSON.stringify({{ Controls: QuestionareBindingControls, QNGroup: '{0}', ProjID: '{1}' }})", QNGroup, projID.Value.ToString()) +
                @"}).done(function(data)
                   {    //console.log(data); 
                        var obj =  JSON.parse(data);
                       // console.log(obj); 
                       //xxx = obj;
                        obj.forEach(function myFunction(item, index) {
                       // console.log(item)
                        item = item.replace(new RegExp('\n', 'g'),'\\n');
                        item = item.replace(new RegExp('\r', 'g'),'\\r');
                       // item = item.replace(new RegExp('""', 'g'),'\""');
                        eval(item);
                        })
                        //QuestionareModel = data;
                        ko.cleanNode(" + elementPanel + @");
                        ko.applyBindings(QuestionareModel," + elementPanel + @");
                        khProjBG.KnockoutNumberFormat();
              " + afterBinding + @"
                    });                
             function DisableQNPanel()
            {
                $('#" + elementPanel + @"')
                .find('input,textarea,select').not(':input[type=button], :input[type=submit], :input[type=reset]')
                .prop('disabled', JSON.parse(" + elementHDFDisable + @".value));
            }
              DisableQNPanel();
            " + appendJS;
            return js;

        }
        public static  string CommonScript(string elementPanel, string elementHiddenModel, string appendJS)
        {
            string ret = "";
            ret = @"

            function GetQNModelToServer()
            {  console.log('GetQNModelToServer');
             var isValid = true
             var objTmp = {}
             Object.keys(QuestionareModel).forEach(function(key) {
            eval (""objTmp['"" + key + ""'] = "" + JSON.stringify(QuestionareModel[key]()) );   });
                " + elementHiddenModel + @".value = JSON.stringify(objTmp);
            //console.log(" + elementHiddenModel + @");
               return isValid;
            }" + appendJS;
            return ret;
        }
   

        public static string ConvertQNToJson(List<ServiceModels.ProjectInfo.Questionare> qn, List<string> controls)
        {
            string ret = "[]";
            List<string> tmp = new List<string>();
            foreach(string c in controls)
            {
                var arrtype = c.Split('|');

                var arr = arrtype[0].Split(':');
                string v = "";
                var fname = "";
                if (arr.Count() > 2)
                {
                    fname = arr[2].Trim().Split(',')[0].Trim();
                }
                else
                {
                    fname = arr[1].Trim();
                }
                var q = qn.Where(w => w.QField == fname).Select(s => s.QValue).FirstOrDefault();
                if (q != null)
                {
                    v = q;
                }
                string value = "";
                if (arrtype[1].ToLower() == "checkbox".ToLower())
                {
                    if (v == "")
                    {
                        value = "false";
                    }
                    else
                    {
                        value = v.ToLower();
                    }

                }else
                {

                     if (arr[0].ToLower() == "kendoNumericTextBox".ToLower())
                     {
                        if (v == "")
                        {
                            value = "0";
                        }else
                        {
                                value =  v;
     
                        }
                    } else if (arr[0].ToLower() == "json".ToLower()) {
                        value = v.Replace("\\\"", "\\\\\"");
                        //value = value.Replace("\\'", "\\\\'");
                        value = value.Replace("\"","\\\"")   ;
                        //value = value.Replace("'", "\\'");
                        value = value.Replace("\n", "");
                        value = value.Replace("\r", "");
                       // value = "[]";
                    }
                    else 
                    {
                        //if (fname == "B1_14_9_text")
                        //{
                        //    value = value;
                        //}
                        value =  v.Replace("\"","")  ;
                        value = "'" + value.Replace("'", "") + "'";
                    }
                }


                //tmp.Add(string.Format("\"{0}\" : ko.observable(\"{1}\")", arr[1].Trim(), System.Web.HttpUtility.JavaScriptStringEncode(v)));
                value = value.Replace("\n", "\\n");
                value = value.Replace("\r", "\\r");
                tmp.Add(string.Format("\"QuestionareModel['{0}'] = ko.observable({1})\"",fname, value)); 
                // Nep.Project.Common.Web.WebUtility.ToJSON(value)));
                // System.Web.HttpUtility.JavaScriptStringEncode(value)));
            }
            if (tmp.Count > 0)
            {
                ret = "[" + string.Join(",", tmp.ToArray() ) + "]";
            }
            
            return ret;
        }  
        public static decimal SumQNValue (List<DBModels.Model.PROJECTQUESTION> qn , string[] field)
        {
            decimal result = 0;
            decimal dTmp = 0;
            foreach (string f in field)
            { dTmp = 0;
                var found = qn.Where(w => w.QFIELD == f).FirstOrDefault();
                if (found != null)
                {
                    if (decimal.TryParse(found.QVALUE, out dTmp))
                    {
                        result += dTmp;
                    }
                }
                
            }
            return result;
        }  
    }
}
