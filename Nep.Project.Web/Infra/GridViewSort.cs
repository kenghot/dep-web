using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nep.Project.Web.Infra
{
    public class GridViewSort
    {
        public static System.Web.UI.WebControls.SortDirection GetSortDirection(string GridViewClientID, string ColumnName, System.Web.UI.WebControls.SortDirection? SortDirection )
        {
             
             Dictionary<string,System.Web.UI.WebControls.SortDirection  >  dic;

            string sessName = "GridViewSort_" + GridViewClientID;
            dic = ( Dictionary <string,System.Web.UI.WebControls.SortDirection>)HttpContext.Current.Session[sessName];
            if (dic == null)
            {
                dic = new Dictionary<string, System.Web.UI.WebControls.SortDirection>();
                if (!SortDirection.HasValue)
                    SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;

                dic.Add(ColumnName,SortDirection.Value);
            }else
            {
                var col = dic.ContainsKey(ColumnName);
               
                if (!col)
                {
                    if (!SortDirection.HasValue)
                        SortDirection = System.Web.UI.WebControls.SortDirection.Ascending;                       
                    dic.Add(ColumnName, SortDirection.Value);
                }else
                
                {
                     
                    if (! SortDirection.HasValue)
                        SortDirection = (dic[ColumnName] == System.Web.UI.WebControls.SortDirection.Ascending ? System.Web.UI.WebControls.SortDirection.Descending : System.Web.UI.WebControls.SortDirection.Ascending);

                    dic[ColumnName] = SortDirection.Value;


                }
                
            }

          
           
            HttpContext.Current.Session[sessName] = dic;
            return SortDirection.Value;
        }
    }
}