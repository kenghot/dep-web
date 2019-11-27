using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels.Security
{
    [Serializable]
    public class NepProjectPrincipal : IPrincipal
    {
        private ServiceModels.Security.NepProjectIdentity _identity;
        ServiceModels.Security.SecurityInfo _identityInfo;

        #region IPrincipal Members
        public IIdentity Identity
        {
            get { return _identity; }
        }

        public ServiceModels.Security.SecurityInfo IdentityInfo
        {
            get { return _identityInfo; }
            set { _identityInfo = value; }
        }

        public NepProjectIdentity InternalIdentity
        {
            get { return _identity; }
            set { _identity = value; }
        }

        public bool IsInRole(string role)
        {
            return _identityInfo.Roles.Contains(role);
        }
        #endregion IPrincipal Members
    }
}
