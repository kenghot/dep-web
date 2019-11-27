using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nep.Project.Common
{
    public class Logging
    {
        private static readonly string _logSource = "Nep.Project";

        public static void LogInfo(string category, string logMessage)
        {
            if (category == null)
                AddLogLine(logMessage, false);
            else
                AddLogLine(string.Format("[ServiceInfo ({0})] : {1}", category, logMessage), false);
        }

        public static void LogError(ErrorType type, string category, string logMessage)
        {
            if (category == null)
                AddLogLine(logMessage, true);
            else
                AddLogLine(string.Format("[{0} ({1})] : {2}", type.ToString(), category, logMessage), true);
        }

        public static void LogError(ErrorType type, string category, Exception ex)
        {
            if (ex is System.Threading.ThreadAbortException)
            {
                return;
            }

            if (category == null)
            {
                AddLogLine(ex.ToString(), true);
            }
            else
            {
                AddLogLine(string.Format("[{0} ({1})] : {2}", type.ToString(), category, ex.ToString()), true);
            }
        }

        private static void AddLogLine(string logMessage, bool isError)
        {
            EventLog log = new EventLog();
            log.Source = _logSource;

            try
            {
                log.WriteEntry(logMessage, (isError ? EventLogEntryType.Error : EventLogEntryType.Information));

            }
            catch (System.Security.SecurityException ex)
            {
                throw new ApplicationException("Nep Project Error", ex);
            }
            catch
            {
                log.Clear();
                log.WriteEntry(logMessage, (isError ? EventLogEntryType.Error : EventLogEntryType.Information));
            }
            log.Close();
        }

        public enum ErrorType
        {
            WebError,
            ServiceError
        }
    }
}
