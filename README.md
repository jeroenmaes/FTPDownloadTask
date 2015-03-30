# FTPDownloadTask [![Build status](https://ci.appveyor.com/api/projects/status/8jtkigqgp9rvyf74?svg=true)](https://ci.appveyor.com/project/joenmaes/ftpdownloadtask)
FTPDownloadTask is an FTP Download task for the [BizTalk Scheduled Task Adapter](https://biztalkscheduledtask.codeplex.com).

## Why do do I need this?
A while back I had the necessity to retrieve a single XML file every night from an FTP site and do further processing with it in BizTalk. Since I dind't want to use an orchestration in combination with the XML "Trigger" functionality of the Scheduled Task Adapter, I made this little thing.

## Screenshots

![Find task](http://i.imgur.com/XtgVqfy.png)

![Task properties](http://i.imgur.com/v3EBr56.png)

## What can it do?

  * Download file from FTP
  * Configurable Retry/Timeout

## How does it work?
Based on the 'HTTPDownload' task that comes with the adapter. Implemented using the 'FtpWebRequest' class.

## LICENSE
FTPDownload is licensed under the [MIT License](https://github.com/joenmaes/BTFGui/blob/master/LICENSE) ([OSI](http://www.opensource.org/licenses/mit-license.php)). Basically you're free to do whatever you want with it. Attribution not necessary but appreciated.

## Dependencies
FTPDownload depends on the [BizTalk Scheduled Task Adapter](https://biztalkscheduledtask.codeplex.com). It works with version 5.0.4 and implements the 'IScheduledTaskStreamProvider' interface.
