using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Security
{
    [Serializable]
    public class NepProjectIdentity : IIdentity
    {
        public readonly static NepProjectIdentity Anonymous = new NepProjectIdentity() { IsAuthenticated = false, Name = "" };
        
        #region IIdentity Members
        Boolean _isAuthenticated = false;
        String _name = "";
        public string AuthenticationType
        {
            get { return "NEP_PROJECT"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set { _isAuthenticated = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion IIdentity Members


        
    }
}
