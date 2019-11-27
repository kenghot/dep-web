using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public class OrganizationParameter
    {
        public String AttachFilePath { get; set; }        

        public String BangkokProvinceAbbr { get; set; }

        public String CenterProvinceAbbr { get; set; }

        public String PrefixDocNo0702 { get; set; }

        public String NepAddress { get; set; }

        public String FollowupContact { get; set; }

        public String NepProjectDirectorName { get; set; }
        public String NepProjectDirectorFirstName
        {
            get
            {
                string firstName = this.NepProjectDirectorName;
                string[] temp = firstName.Split(new char[]{' '});
                firstName = temp[0];
                return firstName;
            }
        }
        public String NepProjectDirectorLastName
        {
            get
            {
                string lastName = this.NepProjectDirectorName;
                string[] temp = lastName.Split(new char[] { ' ' });
                lastName = temp[temp.Length - 1];
                return lastName;
            }
        }

        public String NepProjectDirectorPosition { get; set; }
    }
}
