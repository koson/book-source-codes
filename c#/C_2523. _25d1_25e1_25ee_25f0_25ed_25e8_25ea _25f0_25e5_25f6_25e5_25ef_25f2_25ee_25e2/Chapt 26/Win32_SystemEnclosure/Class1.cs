using System;
using System.Management;

namespace Win32_SystemEnclosure
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			// Перечисляем все модули компьютера
			WqlObjectQuery query = new WqlObjectQuery(
				"SELECT * FROM Win32_SystemEnclosure");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			int i = 0;
			foreach (ManagementObject mo in find.Get()) 
			{
				Console.WriteLine("--- Chasis setting #" + i);
				Console.WriteLine("Chasis type." + GetChasisType(mo));
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Depth of physical package (in inches)." +
					mo["Depth"]);
				Console.WriteLine("Height of physical package (in inches).." +
					mo["Height"]);
				Console.WriteLine("Width of physical package (in inches)." +
					mo["Width"]);
				Console.WriteLine("Weight." + mo["Weight"]);
				Console.WriteLine("Service philosophy " +
					GetServicePhilosophy(mo));
				Console.WriteLine("Status." + mo["Status"]);
				Console.WriteLine("Property includes visible alarm." +
					mo["VisibleAlarm"]);
				Console.WriteLine("Property includes visible alarm." +
					mo["VisibleAlarm"]);
				Console.WriteLine("--------------------------------");
				i++;
			}
		}

		private static string GetChasisType(ManagementObject mo)
		{
			System.UInt16[] type = (System.UInt16[])mo["ChassisTypes"];
			String returnType = "";
			for (int i=0; i<type.Length; i++)
			{
				if (i > 0) returnType += ", ";
				switch (type[i])
				{
					case 1:
						returnType += "Other";
						break;
					case 2:
						returnType += "Unknown";
						break;
					case 3:
						returnType += "Desktop";
						break;
					case 4:
						returnType += "Low Profile Desktop";
						break;
					case 5:
						returnType += "Pizza Box";
						break;
					case 6:
						returnType += "Mini Tower";
						break;
					case 7:
						returnType += "Tower";
						break;
					case 8:
						returnType += "Portable";
						break;
					case 9:
						returnType += "Laptop";
						break;
					case 10:
						returnType += "Notebook";
						break;
					case 11:
						returnType += "Hand Held";
						break;
					case 12:
						returnType += "Docking Station";
						break;
					case 13:
						returnType += "All in One";
						break;
					case 14:
						returnType += "Sub Notebook";
						break;
					case 15:
						returnType += "Space-Saving";
						break;
					case 16:
						returnType += "Lunch Box";
						break;
					case 17:
						returnType += "Main System Chassis";
						break;
					case 18:
						returnType += "Expansion Chassis";
						break;
					case 19:
						returnType += "SubChassis";
						break;
					case 20:
						returnType += "Bus Expansion Chassis";
						break;
					case 21:
						returnType += "Peripheral Chassis";
						break;
					case 22:
						returnType += "Storage Chassis";
						break;
					case 23:
						returnType += "Rack Mount Chassis";
						break;
					case 24:
						returnType += "Sealed-Case PC";
						break;
				}
			}
			return returnType;
		}

		private static string GetServicePhilosophy(ManagementObject mo)
		{
			int i = Convert.ToInt16(mo["ServicePhilosophy"]);
			switch (i)
			{
				case 0:
					return "Unknown";
				case 1:
					return "Other";
				case 2:
					return "Service From Top";
				case 3:
					return "Service From Front";
				case 4:
					return "Service From Back";
				case 5:
					return "Service From Side";
				case 6:
					return "Sliding Trays";
				case 7:
					return "Removable Sides";
				case 8:
					return "Moveable";
			}
			return "Service philosophy not defined.";
		}
	}
}
