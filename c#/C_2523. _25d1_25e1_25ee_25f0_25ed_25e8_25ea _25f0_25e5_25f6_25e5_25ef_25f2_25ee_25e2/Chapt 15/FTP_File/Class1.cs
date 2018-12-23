using System;
using System.IO;
using System.Runtime.InteropServices;

namespace FTP_File
{
	internal class __FtpDownload {
		[DllImport("WININET", EntryPoint="InternetOpen",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern IntPtr __InternetOpen(
			string lpszAgent,
			int dwAccessType,
			string lpszProxyName,
			string lpszProxyBypass,
			int dwFlags);

		[DllImport("WININET", EntryPoint="InternetCloseHandle",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern bool __InternetCloseHandle(IntPtr hInternet);

		[DllImport("WININET", EntryPoint="InternetConnect",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern IntPtr __InternetConnect(
			IntPtr hInternet,
			string lpszServerName,
			int nServerPort,
			string lpszUsername,
			string lpszPassword,
			int dwService,
			int dwFlags,
			int dwContext);

		[DllImport("WININET", EntryPoint="FtpSetCurrentDirectory",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern bool __FtpSetCurrentDirectory(
			IntPtr hConnect,
			string lpszDirectory);

		[DllImport("WININET", EntryPoint="InternetAttemptConnect",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern int __InternetAttemptConnect(int dwReserved);

		[DllImport("WININET", EntryPoint="FtpGetFile",
			SetLastError=true, CharSet=CharSet.Auto)]
		static extern bool __FtpGetFile(
			IntPtr hConnect,
			string lpszRemoteFile,
			string lpszNewFile,
			bool fFailIfExists,
			FileAttributes dwFlagsAndAttributes,
			int dwFlags,
			int dwContext);

		const int ERROR_SUCCESS = 0;
		const int INTERNET_OPEN_TYPE_DIRECT = 1;
		const int INTERNET_SERVICE_FTP = 1;

		static void Main() 
		{
			IntPtr inetHandle = IntPtr.Zero;
			IntPtr ftpconnectHandle = IntPtr.Zero;

			try 
			{
				//check for inet connection
				if (__InternetAttemptConnect(0) != ERROR_SUCCESS) 
				{
					throw new InvalidOperationException(
						"no connection to internet available");
				}

				//connect to inet
				inetHandle = __InternetOpen(
					"billyboy FTP", INTERNET_OPEN_TYPE_DIRECT, null, null, 0);
				if (inetHandle == IntPtr.Zero) 
				{
					throw new NullReferenceException(
						"couldn't establish a connection to the internet");
				}

				//connect to ftp.microsoft.com
				ftpconnectHandle = __InternetConnect(
					inetHandle, "ftp.microsoft.com", 21, "anonymous",
					"myemail@yahoo.com", INTERNET_SERVICE_FTP,
					0, 0);
				if (ftpconnectHandle == IntPtr.Zero) 
				{
					throw new NullReferenceException(
						"couldn't connect to microsoft.com");
				}

				//set to desired directory on FTP server
				if (! __FtpSetCurrentDirectory(ftpconnectHandle, "/deskapps")) 
				{
					throw new InvalidOperationException(
						"couldn't set to desired directory");
				}

				//download file from server
				if (! __FtpGetFile(ftpconnectHandle, "readme.txt",
				                   "c:\\downloadedFile1.txt", false, 0, 0, 0)) 
				{
					throw new IOException("couldn't download file");
				}

				//success
				Console.WriteLine("SUCCESS: file downloaded successfully");
			} catch (Exception ex) {
				//print error message
				Console.WriteLine("ERROR: " + ex.Message);
			} finally {
				//close connection to ftp.microsoft.com
				if (ftpconnectHandle != IntPtr.Zero) {
					__InternetCloseHandle(ftpconnectHandle);
				}
				ftpconnectHandle = IntPtr.Zero;

				//close connection to inet
				if (inetHandle != IntPtr.Zero) {
					__InternetCloseHandle(inetHandle);
				}
				inetHandle = IntPtr.Zero;
			}

			Console.ReadLine();
		}
	}
}