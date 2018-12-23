using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoCompleteText
{
	public class AutoCompleteText
	{
		[DllImport("SHLWAPI", CharSet=CharSet.Auto, SetLastError=true)]
		static extern int SHAutoComplete(IntPtr hwndEdit, SHAutoCompleteFlags dwFlags);

		[Flags]
		enum SHAutoCompleteFlags: uint
		{
			/// <summary>
			/// Currently (FileSystem | UrlAll)
			/// </summary>
			Default=0x00000000,
            
			/// <summary>
			/// This includes the File System as well as the rest 
			/// of the shell (Desktop\My Computer\Control Panel\)
			/// </summary>
			FileSystem=0x00000001,
            
			/// <summary>
			/// URLs in the User's History and URLs in the User's 
			/// Recently Used list.
			/// </summary>
			UrlAll=(UrlHistory | UrlMRU),
            
			/// <summary>
			/// URLs in the User's History
			/// </summary>
			UrlHistory=0x00000002,
            
			/// <summary>
			/// URLs in the User's Recently Used list.
			/// </summary>
			UrlMRU=0x00000004,
            
			/// <summary>
			///  Use the tab to move thru the autocomplete 
			///  possibilities instead of to the next dialog/window
			///  control.
			/// </summary>
			UseTab=0x00000008,
            
			/// <summary>
			/// Don't AutoComplete non-File System items.
			/// </summary>
			FileSysOnly=0x00000010,

			/// <summary>
			/// Ignore the registry default and force the feature on.
			/// </summary>
			AutoSuggestForceOn=0x10000000,
            
			/// <summary>
			/// Ignore the registry default and force the feature off.
			/// </summary>
			AutoSuggestForceOff=0x20000000,

			/// <summary>
			/// Ignore the registry default and force the feature on. 
			/// (Also known as AutoComplete)
			/// </summary>
			AutoAppendForceOn=0x40000000,

			/// <summary>
			/// Ignore the registry default and force the feature off. 
			/// (Also known as AutoComplete)
			/// </summary>
			AutoAppendForceOff=0x80000000
		}

		public static void EnableAutoComplete(IntPtr handle, bool autoCompleteUrls, bool autoCompleteFiles) 
		{
			if (autoCompleteUrls || autoCompleteFiles)
			{
				Application.OleRequired();

				SHAutoCompleteFlags flags = SHAutoCompleteFlags.AutoSuggestForceOn | 
					SHAutoCompleteFlags.AutoAppendForceOn;

				if (autoCompleteUrls ) flags |= SHAutoCompleteFlags.UrlAll;
				if (autoCompleteFiles) flags |= SHAutoCompleteFlags.FileSystem;

				SHAutoComplete(handle, flags);
			}
		}
	}
}
