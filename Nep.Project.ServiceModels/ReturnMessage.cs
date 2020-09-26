using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.ServiceModels
{
    public class ReturnMessage
    {
        private List<String> _message = new List<String>();
        public List<String> Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (value == null)
                    _message = new List<String>();
                else
                    _message = value;
            }
        }
        public bool IsCompleted { get; set; }
        public void SetExceptionMessage(Exception ex)
        {
            _message.Add(ex.Message);
            if (ex.InnerException != null)
            {
                _message.Add(ex.InnerException.Message);
            }
        }
    }    

    public class ReturnObject<T> : ReturnMessage
    {
        public T Data { get; set; }
    }

    public class ReturnQueryData<T> : ReturnObject<List<T>>
    {
        public Int32 TotalRow { get; set; }
    }

}
