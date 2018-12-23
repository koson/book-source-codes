using System;
using System.Runtime.InteropServices;

namespace IEHistory
{
	class Class1
	{
		struct STATURL
		{
			public static uint SIZEOF_STATURL = (uint)Marshal.SizeOf(typeof(STATURL));
			public uint cbSize;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwcsUrl;
			[MarshalAs(UnmanagedType.LPWStr)]
			public string pwcsTitle;
			public FILETIME ftLastVisited, ftLastUpdated, ftExpires;
			public uint dwFlags;
		}

		[ComImport, Guid("3C374A42-BAE4-11CF-BF7D-00AA006946EE"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			interface IEnumSTATURL
		{
			[PreserveSig]
			uint Next(uint celt, out STATURL rgelt, out uint pceltFetched);
			void Skip(uint celt);
			void Reset();
			void Clone(out IEnumSTATURL ppenum);
			void SetFilter(
				[MarshalAs(UnmanagedType.LPWStr)] string poszFilter,
				uint dwFlags);
		}

		[ComImport, Guid("AFA0DC11-C313-11d0-831A-00C04FD5AE38"),
			InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			interface IUrlHistoryStg2
		{
			void AddUrl(
				[MarshalAs(UnmanagedType.LPWStr)] string pocsUrl,
				[MarshalAs(UnmanagedType.LPWStr)] string pocsTitle,
				uint dwFlags);

			void DeleteUrl(
				[MarshalAs(UnmanagedType.LPWStr)] string pocsUrl,
				uint dwFlags);

			void QueryUrl(
				[MarshalAs(UnmanagedType.LPWStr)] string pocsUrl,
				uint dwFlags,
				ref STATURL lpSTATURL);

			void BindToObject(
				[MarshalAs(UnmanagedType.LPWStr)] string pocsUrl,
				ref Guid riid,
				[MarshalAs(UnmanagedType.IUnknown)] out object ppvOut);

			IEnumSTATURL EnumUrls();

			void AddUrlAndNotify(
				[MarshalAs(UnmanagedType.LPWStr)] string pocsUrl,
				[MarshalAs(UnmanagedType.LPWStr)] string pocsTitle,
				uint dwFlags,
				[MarshalAs(UnmanagedType.Bool)] bool fWriteHistory,
				[MarshalAs(UnmanagedType.IUnknown)] object /*IOleCommandTarget*/
				poctNotify,
				[MarshalAs(UnmanagedType.IUnknown)] object punkISFolder);

			void ClearHistory();
		}

		[ComImport, Guid("3C374A40-BAE4-11CF-BF7D-00AA006946EE")]
		class UrlHistory { }

		[STAThread]
		static void Main(string[] args)
		{
			IUrlHistoryStg2 uhs2 = (IUrlHistoryStg2)new UrlHistory();
			IEnumSTATURL estaturl = uhs2.EnumUrls();
			STATURL staturl;
			uint fetched;

			while (0 == estaturl.Next(1, out staturl, out fetched))
				Console.WriteLine(staturl.pwcsUrl);

			Console.ReadLine();
		}
	}
}
