using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nep.Project.Common;

namespace Nep.Project.Business
{
    public class RunningNumberService : IServices.IRunningNumberService
    {
        private readonly DBModels.Model.NepProjectDBEntities _db;
        private char[] ARR_THAI_NUMERIC;
        private Dictionary<string, string> PROVINCE_SECTION_KEYS = new Dictionary<string, string>();
               
        public RunningNumberService(DBModels.Model.NepProjectDBEntities db)
        {
            _db = db;

            ARR_THAI_NUMERIC = new char[] { '๐', '๑', '๒', '๓', '๔', '๕', '๖', '๗', '๘', '๙' };

            PROVINCE_SECTION_KEYS.Add(Common.LOVCode.Section.ส่วนกลาง, Common.Constants.RUNNING_KEY_PROJECT_NO_C);
            PROVINCE_SECTION_KEYS.Add(Common.LOVCode.Section.ภาคกลางและตะวันออก, Common.Constants.RUNNING_KEY_PROJECT_NO_C);
            PROVINCE_SECTION_KEYS.Add(Common.LOVCode.Section.ภาคเหนือ, Common.Constants.RUNNING_KEY_PROJECT_NO_N);
            PROVINCE_SECTION_KEYS.Add(Common.LOVCode.Section.ภาคตะวันออกเฉียงเหนือ, Common.Constants.RUNNING_KEY_PROJECT_NO_NT);
            PROVINCE_SECTION_KEYS.Add(Common.LOVCode.Section.ภาคใต้, Common.Constants.RUNNING_KEY_PROJECT_NO_S);
        }
        public ServiceModels.ReturnObject<string> GetProjectContractNo(decimal projectID, int contractYear)
        {
            ServiceModels.ReturnObject<string> result = new ServiceModels.ReturnObject<string>();
            try
            {
                ServiceModels.Province province = (from pro in _db.ProjectGeneralInfoes
                                join prov in _db.MT_Province on pro.ProvinceID equals prov.ProvinceID
                                where pro.ProjectID == projectID
                                                   select new ServiceModels.Province
                                                    {
                                                        ProvinceID = pro.ProvinceID,
                                                        ProvinceAbbr = prov.ProvinceAbbr                                                                  
                                                    }).SingleOrDefault();
               

                if(province != null){
                    String keyName = province.ProvinceAbbr;
                    String keyNumber = contractYear.ToString();
                    decimal runningNumber = 1;
                    var runningNumResult = _db.MT_RunningNumbers.Where(x => (x.KeyName == keyName) && (x.KeyNumber == keyNumber)).SingleOrDefault();
                    if (runningNumResult != null)
                    {
                        runningNumber = runningNumResult.RunningNumber;

                        runningNumResult.RunningNumber = (runningNumber < 999) ? (runningNumber + 1) : runningNumber;
                    }
                    else
                    {
                        DBModels.Model.MT_RunningNumber newRunning = new DBModels.Model.MT_RunningNumber();
                        newRunning.KeyName = keyName;
                        newRunning.KeyNumber = keyNumber;
                        newRunning.RunningNumber = runningNumber + 1;
                        _db.MT_RunningNumbers.Add(newRunning);
                    }
                    _db.SaveChanges();

                    //Contract No : ชื่อย่อจังหวัด(๒ ตัวอักษร) + running number ตามปี 3 หลัก + ‘/’ + ปี พ .ศ. (๔ หลัก) เช่น ชร00๑/๒๕๕๘
                    string keyRunning = runningNumber.ToString("000");
                    //keyRunning = MapToThaiNumber(keyRunning);

                    //keyNumber = MapToThaiNumber(keyNumber);

                    result.Data = String.Format("{0}{1}/{2}", keyName, keyRunning, keyNumber);
                    result.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Running Number", ex);
            }
            return result;
        }

        public ServiceModels.ReturnObject<string> GetProjectProjectNo(decimal projectID, DateTime approvalDate, int committeeNo, int committeeYear)
        {
            ServiceModels.ReturnObject<string> result = new ServiceModels.ReturnObject<string>();
            try
            {
                ServiceModels.Province province = (from pro in _db.ProjectGeneralInfoes
                                                   join prov in _db.MT_Province on pro.ProvinceID equals prov.ProvinceID
                                                   where pro.ProjectID == projectID
                                                   select new ServiceModels.Province
                                                   {
                                                       ProvinceID = pro.ProvinceID,
                                                       SectionCode = prov.Section.LOVCode
                                                   }).SingleOrDefault();
                if(province != null){
                    String keyName = PROVINCE_SECTION_KEYS[province.SectionCode];

                    String approvalYY = (approvalDate.Year + 543).ToString().Substring(2, 2);
                    String comitteeYY = (committeeYear + 543).ToString().Substring(2,2);
                    String keyNumber = String.Format("{0}{1}{2}", approvalYY, committeeNo.ToString("00"), comitteeYY);

                    decimal runningNumber = 1;
                    var runningNumResult = _db.MT_RunningNumbers.Where(x => (x.KeyName == keyName) && (x.KeyNumber == keyNumber)).SingleOrDefault();
                    if (runningNumResult != null)
                    {
                        runningNumber = runningNumResult.RunningNumber;

                        runningNumResult.RunningNumber = (runningNumber < 999) ? (runningNumber + 1) : runningNumber;
                    }
                    else
                    {
                        DBModels.Model.MT_RunningNumber newRunning = new DBModels.Model.MT_RunningNumber();
                        newRunning.KeyName = keyName;
                        newRunning.KeyNumber = keyNumber;
                        newRunning.RunningNumber = runningNumber + 1;
                        _db.MT_RunningNumbers.Add(newRunning);
                    }
                    _db.SaveChanges();

                    //ชื่อย่อภาค(ภาคกลาง C, ภาคเหนือ N, ภาคตะวันออกเฉียงเหนือ NT, ภาคใต้ S) + ปี พ .ศ. (๒ หลัก) มติที่ประชุม ๒ หลัก + ปีมติที่ประชุม (๒ หลัก) + running number (๓ หลัก)                    //เช่น C570856001
                    String keyRunning = runningNumber.ToString("000");
                    result.Data = String.Format("{0}{1}{2}", keyName, keyNumber, keyRunning);
                    result.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                result.IsCompleted = false;
                result.Message.Add(ex.Message);
                Common.Logging.LogError(Logging.ErrorType.ServiceError, "Running Number", ex);
            }
            return result;
        }

        private string MapToThaiNumber(string arabicNumber)
        {            
            string text = "";
            char[] number = arabicNumber.ToArray();
            int index;
            for (int i = 0; i < number.Length; i++ )
            {
                index = (int)Char.GetNumericValue(number[i]);
                text += ARR_THAI_NUMERIC[index];
            }
            return text;
        }

    }
}
