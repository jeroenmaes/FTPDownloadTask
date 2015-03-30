using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ScheduledTaskAdapter.TaskComponents;
using System.Net;
using System.Threading;
using System.Diagnostics;

namespace JM.CustomTaskComponents
{
    /// <summary>
	/// FtpDownload: implements the IScheduledTaskStreamProvider interface.
	/// downloads the data at the specified Url and passes to the ScheduledTask Adapter as a stream
	/// </summary>
    public class FTPDownload : IScheduledTaskStreamProvider
    {
        private const string TaskComponentName = "ScheduledTask FTPDownload";
        private const string EventLogSource = "BizTalk Server";
        
        public Stream GetStream(object parameter)
        {
            int retryCounter = 1;
            bool isDownloaded = false;
            Stream responseStream = null;   
            
            FTPDownloadArguments args = (FTPDownloadArguments)parameter;
            if (args.Url == string.Empty)
                throw (new ArgumentException(TaskComponentName, "url"));
           
            EventLog.WriteEntry(EventLogSource, System.String.Format("Starting the {0}.", TaskComponentName), EventLogEntryType.Information);

            while (!isDownloaded && retryCounter <= args.RetryCount)
            {
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@args.Url);
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    request.Credentials = new NetworkCredential(args.User, args.Password);

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    responseStream = response.GetResponseStream();                    
                    isDownloaded = true;
                }
                catch (WebException e)
                {
                    if ((retryCounter + 1) <= args.RetryCount)
                    {
                        EventLog.WriteEntry(EventLogSource, System.String.Format("The {0} caused an exception, the operation will be retried in {1} minutes: {2}", TaskComponentName, args.RetryInterval, e.Message.ToString()), EventLogEntryType.Warning);
                        Thread.Sleep(args.RetryInterval * 60000);
                    }
                    else
                    {
                        EventLog.WriteEntry(EventLogSource, System.String.Format("The {0} caused an exception, the operation will not be retried, max number of retries exceeded: {1}", TaskComponentName, e.Message.ToString()), EventLogEntryType.Error);
                    }
                    
                    retryCounter++;
                }
                catch (Exception e)
                {
                    throw e; //unhandled exception, this will disable the adapter
                }
            }

            if (isDownloaded)
            {
                EventLog.WriteEntry(EventLogSource, System.String.Format("The {0} finished successfully.", TaskComponentName), EventLogEntryType.Information);
                return responseStream;
            }
            else
            {
                EventLog.WriteEntry(EventLogSource, String.Format("The {0} failed to complete successfully after {1} retries. The adapter will start normally the next schedueled moment.", TaskComponentName, args.RetryCount), EventLogEntryType.Error);
                
                return null; //prevent the adapter from being disabled
            }
        }

		public System.Type GetParameterType()
		{
            return typeof(FTPDownloadArguments);
		}
	}
 }

