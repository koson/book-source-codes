using System;
using System.Management;

namespace Win32_DiskPartition
{
	class Class1
	{
		[STAThread]
		static void Main(string[] args)
		{
			WqlObjectQuery query = new WqlObjectQuery(
				"Select * from Win32_DiskPartition");
			ManagementObjectSearcher find = 
				new ManagementObjectSearcher(query);
			foreach (ManagementObject mo in find.Get())
			{
				Console.WriteLine("Block size." + mo["BlockSize"] + " Bytes");
				Console.WriteLine("Partition is labeled as bootable. " + mo["Bootable"]);
				Console.WriteLine("Boot partition active. " +	mo["BootPartition"]);
				Console.WriteLine("Caption.." + mo["Caption"]);
				Console.WriteLine("Description." + mo["Description"]);
				Console.WriteLine("Unique identification of partition.." +	mo["DeviceID"]);
				Console.WriteLine("Index number of the disk with that partition." + mo["DiskIndex"]);
				Console.WriteLine("Detailed description of error in LastErrorCode." + mo["ErrorDescription"]);
				Console.WriteLine("Type of error detection and correction." + mo["ErrorMethodology"]);
				Console.WriteLine("Hidden sectors in partition." + mo["HiddenSectors"]);
				Console.WriteLine("Index number of the partition." + mo["Index"]);
				Console.WriteLine("Last error by device." + mo["LastErrorCode"]);
				Console.WriteLine("Total number of consecutive blocks." + mo["NumberOfBlocks"]);
				Console.WriteLine("Partition labeled as primary." + mo["PrimaryPartition"]);
				Console.WriteLine("Free description of media purpose. " + mo["Purpose"]);
				Console.WriteLine("Total size of partition." + 	mo["Size"] + " bytes");
				Console.WriteLine("Starting offset of the partition " +	mo["StartingOffset"]);
				Console.WriteLine("Status." + mo["Status"]);
				Console.WriteLine("Type of the partition." + mo["Type"]);
			}
		}
	}
}
