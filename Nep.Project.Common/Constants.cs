using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Common
{
    public static class Constants
    {
        static Constants()
        {
            CULTUREINFO = new CultureInfo("en-GB");

            UI_CULTUREINFO = new CultureInfo("th-TH");

            var appSettings = ConfigurationManager.AppSettings;
            WEBSITE_URL = appSettings["WEBSITE_URL"] ?? WEBSITE_URL;
            SMS_SERVICE_URL = appSettings["SMS_SERVICE_URL"] ?? SMS_SERVICE_URL;
            SMS_SENDER = appSettings["SMS_SENDER"] ?? SMS_SENDER;
            SMS_SERVICE_USERNAME = appSettings["SMS_SERVICE_USERNAME"] ?? SMS_SERVICE_USERNAME;
            SMS_SERVICE_PASSWORD = appSettings["SMS_SERVICE_PASSWORD"] ?? SMS_SERVICE_PASSWORD;
        }
        public const Int32 SYSTEM_ACCOUNT = 1;
        public const String CONNECTION_STRING_NAME = "NepProjectDBEntities";
        public const String CULTURE = "en-GB";
        public const String UI_CULTURE = "th-TH";
        public static readonly CultureInfo CULTUREINFO;

        public static readonly CultureInfo UI_CULTUREINFO;

        public const String UI_FORMAT_DATE = "dd/MM/yyyy";
        public const String UI_FORMAT_DATE_TIME = "dd/MM/yyyy HH:mm:ss";

        public const String UI_GRID_FORMAT_DATE = "{0:dd/MM/yyyy}";
        public const String UI_GRID_FORMAT_DATE_TIME = "{0:dd/MM/yyyy HH:mm:ss}";

        public const String DB_FORMAT_DATE = "yyyy-MM-dd";
        public const String DB_FORMAT_DATE_TIME = "yyyy-MM-dd HH:mm:ss";

        public const String FROMAT_TIME = "HH.mm";
        public const String FORMAT_FULL_TIME = "HH:mm:ss";

        public const String FORMAT_NUMBER = "0.00";
        public const String UI_FORMAT_INT = "#,##0";
        public const String UI_FORMAT_DECIMAL = "#,##0.00";

        public const String FILE_NAME_VALIDATE = "The filename cannot contain spaces or any of \nthe following characters:\n/, \\, :, *, ?, \", <, >, |, %";

        public const Int32 PAGE_SIZE = 10;

        public const String SYSTEM_USERNAME = "system";

        #region CssClass
        public const string CSS_ERROR_TEXT = "alert alert-danger";
        public const string CSS_RESULT_TEXT = "alert alert-info";
        #endregion

        public const String OTHER_PROJECT_TARGET_NAME = "อื่นๆ";

        public const String TICKET_COOKIE_NAME = "sso_tid";

        public const String UPLOAD_TEMP_PATH = "~/App_Data/UploadTemp/";

        public const String RUNNING_KEY_PROJECT_NO_C = "C";
        public const String RUNNING_KEY_PROJECT_NO_N = "N";
        public const String RUNNING_KEY_PROJECT_NO_NT = "NT";
        public const String RUNNING_KEY_PROJECT_NO_S = "S";

        public const String REGISTER_ATTACHMENT_FOLDER = "regis";

        public static readonly String WEBSITE_URL = "http://localhost:8976";
        public static readonly String SMS_SERVICE_URL = "http://www.cat4sms.com/api/api.php";

        public static readonly String SMS_SENDER = "CAT4SMS";
        public static readonly String SMS_SERVICE_USERNAME = "nepfund";
        public static readonly String SMS_SERVICE_PASSWORD = "mckm";

        public const String BOOLEAN_TRUE = "1";
        public const String BOOLEAN_FALSE = "0";

        public const String GENDER_MALE = "M";
        public const String GENDER_FEMALE = "F";

        public const string ACTIVITY_BUDGET_MANAGE_EXPENSE_CODE = "ค่าบริหารจัดการโครงการ";
        public const string SESSION_LOGIN_DATETIME = "SESSION_LOGIN_DATETIME";
    }
}
