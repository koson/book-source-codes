using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;

namespace Ping2
{
	class Program
	{
		static void Main(string[] args)
		{
			Ping pingSender = new Ping();

			PingOptions options = new PingOptions();
			options.DontFragment = true;

			// Буфер 32 байта
			string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
			byte[] buffer = Encoding.ASCII.GetBytes(data);
			int timeout = 120;
			PingReply reply = pingSender.Send("rsdn.ru", timeout, buffer, options);
			if (reply.Status == IPStatus.Success)
			{
				Console.WriteLine("Address: {0}", reply.Address.ToString());
				Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
				Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
				Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
				Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
			}
		}
	}
}



