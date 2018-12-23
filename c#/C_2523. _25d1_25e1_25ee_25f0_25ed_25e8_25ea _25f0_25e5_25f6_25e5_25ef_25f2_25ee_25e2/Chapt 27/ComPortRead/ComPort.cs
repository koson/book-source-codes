using System;
using System.Runtime.InteropServices;

namespace ComPortRead
{
	public enum Parity : byte
	{ 
		No    = 0,
		Odd   = 1,
		Even  = 2,
		Mark  = 3,
		Space = 4
	}

	public enum StopBits : byte
	{
		Bits1   = 0,
		Bits1_5 = 1,
		Bits2   = 2
	}

	class CommPort 
	{
		private int  PortNum; 
		private int  BaudRate;
		private byte ByteSize;
		private Parity   parity; 
		private StopBits stopBits; 
		private int  hPortHanle = INVALID_HANDLE_VALUE;
		
		public CommPort(int PortNum, int BaudRate, byte ByteSize, Parity parity, StopBits stopBits)
		{
			this.PortNum	= PortNum;
			this.BaudRate	= BaudRate;
			this.ByteSize	= ByteSize;
			this.parity		= parity;
			this.stopBits	= stopBits;
		}

		
		public bool Open() 
		{
			// Открытие порта
			hPortHanle = CreateFile("COM" + PortNum ,GENERIC_READ | GENERIC_WRITE,0, 0,OPEN_EXISTING,0,0);
			if(hPortHanle == INVALID_HANDLE_VALUE) 
			{
				return false;
			}
		
			// Настройка порта
			DCB dcbCommPort = new DCB();
			GetCommState(hPortHanle, ref dcbCommPort);
			dcbCommPort.BaudRate = BaudRate;
			dcbCommPort.Parity   = (byte)parity;
			dcbCommPort.ByteSize = ByteSize;
			dcbCommPort.StopBits = (byte)stopBits;
			if (!SetCommState(hPortHanle, ref dcbCommPort))
			{
				return false;
			}

			return true;
		}
		
		// Возврашает true, если порт открыт
		public bool IsOpen()
		{
			return(hPortHanle!=INVALID_HANDLE_VALUE);
		}

		// Закрытие порта
		public void Close() 
		{
			if (IsOpen()) 
			{
				CloseHandle(hPortHanle);
			}
		}
		
		// Чтение данных
		public byte[] Read(int NumBytes) 
		{
			byte[] BufBytes;
			byte[] OutBytes;
			BufBytes = new byte[NumBytes];
			if (hPortHanle!=INVALID_HANDLE_VALUE) 
			{
				int BytesRead=0;
				ReadFile(hPortHanle, BufBytes, NumBytes, ref BytesRead, 0);
				OutBytes = new byte[BytesRead];
				Array.Copy(BufBytes, OutBytes, BytesRead);
			} 
			else 
			{
				throw(new ApplicationException("Порт не был открыт"));
			}
			return OutBytes;
		}
		
		// Передача данных
		public void Write(byte[] WriteBytes) 
		{
			if (hPortHanle!=INVALID_HANDLE_VALUE) 
			{
				int BytesWritten = 0;
				WriteFile(hPortHanle,WriteBytes,WriteBytes.Length,ref BytesWritten, 0);
			}
			else 
			{
				throw(new ApplicationException("Порт не был открыт"));
			}		
		}
		// Описание констант Win32 API
		private const uint GENERIC_READ  = 0x80000000;
		private const uint GENERIC_WRITE = 0x40000000;
		private const int OPEN_EXISTING  = 3;		
		private const int INVALID_HANDLE_VALUE = -1;
		
		[StructLayout(LayoutKind.Sequential)]
		public struct DCB 
		{
			public int DCBlength;           
			public int BaudRate;            
			/*
				public int fBinary;          
				public int fParity;          
				public int fOutxCtsFlow;     
				public int fOutxDsrFlow;     
				public int fDtrControl;      
				public int fDsrSensitivity;  
				public int fTXContinueOnXoff;
				public int fOutX;         
				public int fInX;          
				public int fErrorChar;    
				public int fNull;         
				public int fRtsControl;   
				public int fAbortOnError; 
				public int fDummy2;       
				*/
			public uint flags;
			public ushort wReserved;       
			public ushort XonLim;          
			public ushort XoffLim;         
			public byte ByteSize;          
			public byte Parity;            
			public byte StopBits;          
			public char XonChar;           
			public char XoffChar;          
			public char ErrorChar;         
			public char EofChar;           
			public char EvtChar;           
			public ushort wReserved1;      
		}
	
		[DllImport("kernel32.dll")]
		private static extern int CreateFile(
			string lpFileName,                
			uint dwDesiredAccess,             
			int dwShareMode,                  
			int lpSecurityAttributes,		  
			int dwCreationDisposition,        
			int dwFlagsAndAttributes,         
			int hTemplateFile                 
			);
		[DllImport("kernel32.dll")]
		private static extern bool GetCommState(
			int hFile,		// дескриптор файла (порта)
			ref DCB lpDCB   // структура DCB
			);	
		[DllImport("kernel32.dll")]
		private static extern bool SetCommState(
			int hFile,		// дескриптор файла (порта)
			ref DCB lpDCB   // структура DCB
			);
		[DllImport("kernel32.dll")]
		private static extern bool ReadFile(
			int hFile,					// дескриптор файла (порта)
			byte[] lpBuffer,            // буфер
			int nNumberOfBytesToRead,	// размер буфера
			ref int lpNumberOfBytesRead,// реально прочитано
			int  lpOverlapped			// 0 для синхронных операций
		);
		[DllImport("kernel32.dll")]	
		private static extern bool WriteFile(
			int hFile,						// дескриптор файла (порта)
			byte[] lpBuffer,                // буфер данных
			int nNumberOfBytesToWrite,      // число байт данных
			ref int lpNumberOfBytesWritten, // реально переданное число байт
			int lpOverlapped				// 0 для синхронных операций
		);
		[DllImport("kernel32.dll")]
		private static extern bool CloseHandle(
			int hObject   // дескриптор файла (порта)
		);
	}

}