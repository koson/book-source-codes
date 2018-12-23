using System;
using System.Management;

namespace Win32_Processor
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Получаем все процессоры компьютера
			WqlObjectQuery query = new WqlObjectQuery(
				"Select * from Win32_Processor");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			int i = 0;
			foreach (ManagementObject mo in find.Get()) 
			{
				Console.WriteLine("-- Processor #" + i + " -");
				Console.WriteLine("Processor address width in bits." +
					mo["AddressWidth"]);
				Console.WriteLine("Architecture of processor." +
					GetArchitecture(mo));
				Console.WriteLine("Caption." + mo["Caption"]);
				Console.WriteLine("Processor address width in bits." +
					mo["AddressWidth"]);
				Console.WriteLine("Usage status of the processor." +
					GetCpuStatus(mo));
				Console.WriteLine("Current clock speed (in MHz)." +
					mo["CurrentClockSpeed"]);
				Console.WriteLine("Processor data width." + mo["DataWidth"]);
				Console.WriteLine("Unique string identification." +
					mo["DeviceID"]);
				Console.WriteLine("External clock frequency." +
					mo["ExtClock"]);
				Console.WriteLine("Processor family." + GetFamily(mo));
				Console.WriteLine("L2 cache size." + mo["L2CacheSize"]);
				Console.WriteLine("L2 cache speed." + mo["L2CacheSpeed"]);
				Console.WriteLine("Load percentage (average value for second)." + mo["LoadPercentage"]);
				Console.WriteLine("Manufacturer." + mo["Manufacturer"]);
				Console.WriteLine("Maximum speed (in MHz)." +
					mo["MaxClockSpeed"]);
				Console.WriteLine("Name." + mo["Name"]);
				Console.WriteLine("Support for power management." +
					mo["PowerManagementSupported"]);
				Console.WriteLine("Unique identificator describing processor"+
					mo["ProcessorId"]);
				Console.WriteLine("Processor type." + GetProcessorType(mo));
				Console.WriteLine("Role (CPU/math)." + mo["Role"]);
				Console.WriteLine("Socket designation." +
					mo["SocketDesignation"]);
				Console.WriteLine("Status." + mo["Status"]);
				Console.WriteLine("Status information." + GetStatusInfo(mo));
				Console.WriteLine("Processor version." + mo["Version"]);
				Console.WriteLine("Socket voltage." + mo["VoltageCaps"]);
				i++;
			}
		}

		private static string GetArchitecture(ManagementObject mo)
		{
			int i = Convert.ToInt32(mo["Architecture"]);
			switch (i)
			{
				case 0:  return "x86";
				case 1:  return "MIPS";
				case 2:  return "Alpha";
				case 3:  return "PowerPC";
				case 4:  return "ia64";
			}
			return "Undefined";
		}

		private static string GetCpuStatus(ManagementObject mo)
		{
			int i = Convert.ToInt32(mo["CpuStatus"]);
			switch(i)
			{
				case 0:  return "Unknown";
				case 1:  return "CPU Enabled";
				case 2:  return "CPU Disabled by User via BIOS Setup";
				case 3:  return "CPU Disabled By BIOS (POST Error)";
				case 4:  return "CPU is Idle";
				case 5:  return "This value is reserved.";
				case 6:  return "This value is reserved.";
				case 7:  return "Other";
			}
			return "Undefined";
		}

		private static string GetFamily(ManagementObject mo)
		{
			int i = Convert.ToInt32(mo["Family"]);
			switch (i)
			{
				case 1:  return "Other";
				case 2:  return "Unknown";
				case 3:  return "8086";
				case 4:  return "80286";
				case 5:  return "80386";
				case 6:  return "80486";
				case 7:  return "8087";
				case 8:  return "80287";
				case 9:  return "80387";
				case 10: return "80487";
				case 11: return "PentiumR brand";
				case 12: return "PentiumR Pro";
				case 13: return "PentiumR II";
				case 14: return "PentiumR processor with MMX technology";
				case 15: return "CeleronT";
				case 16: return "PentiumR II Xeon";
				case 17: return "PentiumR III";
				case 18: return "M1 Family";
				case 19: return "M2 Family";
				case 24: return "K5 Family";
				case 25: return "K6 Family";
				case 26: return "K6-2";
				case 27: return "K6-3";
				case 28: return "AMD AthlonT Processor Family";
				case 29: return "AMDR DuronT Processor";
				case 30: return "AMD2900 Family";
				case 31: return "K6-2+";
				case 32: return "Power PC Family";
				case 33: return "Power PC 601";
				case 34: return "Power PC 603";
				case 35: return "Power PC 603+";
				case 36: return "Power PC 604";
				case 37: return "Power PC 620";
				case 38: return "Power PC X704";
				case 39: return "Power PC 750";
				case 48: return "Alpha Family";
				case 49: return "Alpha 21064";
				case 50: return "Alpha 21066";
				case 51: return "Alpha 21164";
				case 52: return "Alpha 21164PC";
				case 53: return "Alpha 21164a";
				case 54: return "Alpha 21264";
				case 55: return "Alpha 21364";
				case 64: return "MIPS Family";
				case 65: return "MIPS R4000";
				case 66: return "MIPS R4200";
				case 67: return "MIPS R4400";
				case 68: return "MIPS R4600";
				case 69: return "MIPS R10000";
				case 80: return "SPARC Family";
				case 81: return "SuperSPARC";
				case 82: return "microSPARC II";
				case 83: return "microSPARC IIep";
				case 84: return  "UltraSPARC";
				case 85: return  "UltraSPARC II";
				case 86: return  "UltraSPARC IIi";
				case 87: return  "UltraSPARC III";
				case 88: return  "UltraSPARC IIIi";
				case 96: return  "68040";
				case 97: return  "68xxx Family";
				case 98: return  "68000";
				case 99: return  "68010";
				case 100: return  "68020";
				case 101: return  "68030";
				case 112: return  "Hobbit Family";
				case 120: return  "CrusoeT TM5000 Family";
				case 121: return  "CrusoeT TM3000 Family";
				case 128: return  "Weitek";
				case 130: return  "ItaniumT Processor";
				case 144: return  "PA-RISC Family";
				case 145: return  "PA-RISC 8500";
				case 146: return  "PA-RISC 8000";
				case 147: return  "PA-RISC 7300LC";
				case 148: return  "PA-RISC 7200";
				case 149: return  "PA-RISC 7100LC";
				case 150: return  "PA-RISC 7100";
				case 160: return  "V30 Family";
				case 176: return  "PentiumR III XeonT";
				case 177: return  "PentiumR III Processor with IntelR SpeedStepT Technology";
				case 178: return  "PentiumR 4";
				case 179: return  "IntelR XeonT";
				case 180: return  "AS400 Family";
				case 181: return  "IntelR XeonT processor MP";
				case 182: return  "AMD AthlonXPT Family";
				case 183: return  "AMD AthlonMPT Family";
				case 184: return  "IntelR ItaniumR 2";
				case 185: return  "AMD OpteronT Family";
				case 190: return  "K7";
				case 200: return  "IBM390 Family";
				case 201: return  "G4";
				case 202: return  "G5";
				case 250: return  "i860";
				case 251: return  "i960";
				case 260: return  "SH-3";
				case 261: return  "SH-4";
				case 280: return  "ARM";
				case 281: return  "StrongARM";
				case 300: return  "6x86";
				case 301: return  "MediaGX";
				case 302: return  "MII";
				case 320: return  "WinChip";
				case 350: return  "DSP";
				case 500: return  "Video Processor";
			}
			return "Undefined processor family";
		}

		private static string GetProcessorType(ManagementObject mo)
		{
			int i = Convert.ToInt32(mo["ProcessorType"]);
			switch (i)
			{
				case 1: return  "Other";
				case 2: return  "Unknown";
				case 3: return  "Central Processor";
				case 4: return  "Math Processor";
				case 5: return  "DSP Processor";
				case 6: return  "Video Processor";
			}
			return "Undefined type";
		}

		private static string GetStatusInfo(ManagementObject mo)
		{
			int i = Convert.ToInt32(mo["StatusInfo"]);
			switch (i)
			{
				case 1: return  "Other";
				case 2: return  "Unknown";
				case 3: return  "Enabled";
				case 4: return  "Disabled";
				case 5: return  "Not applicable";
			}
			return "StatusInfo not defined.";
		}
	}
}
