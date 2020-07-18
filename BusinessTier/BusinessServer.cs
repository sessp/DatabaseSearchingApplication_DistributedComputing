using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataTier;

namespace BusinessTier
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class BusinessServer : BusinessServerInterface
	{
		private uint LogNum = 0;
		private int nEntries = 0;
		private DataServerInterface dataServer;
		public BusinessServer()
		{
			ChannelFactory<DataTier.DataServerInterface> cFactory;
			NetTcpBinding tcp = new NetTcpBinding();

			string URL = "net.tcp://localhost:8100/DataService";
			cFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
			dataServer = cFactory.CreateChannel();
		}
		public int GetNumEntries()
		{
			log("Request for number of entries in DB was executed ");
			nEntries = dataServer.GetNumEntries();
			return nEntries;
		}

		public void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName)
		{
			log("Search for index was executed for index:" + " " + index + " ");
			dataServer.GetValuesForEntry(index, out aN, out pin, out bal, out fName, out lName);
		}

		static void Main(string[] args)
		{
			Console.WriteLine("My BusinessServer");
			ServiceHost h;
			NetTcpBinding tcp = new NetTcpBinding();

			h = new ServiceHost(typeof(BusinessServer));

			//"net.tcp://0.0.0.0:8100/DataService"
			h.AddServiceEndpoint(typeof(BusinessServerInterface), tcp, "net.tcp://0.0.0.0:8101/BService");
			h.Open();
			Console.WriteLine("Business Server is fully operational");
			Console.ReadLine();
			h.Close();
		}

		public void SearchByLastName(string lastNameSearchString, out uint aN, out uint pin, out int bal, out string fName, out string lName)
		{
			log("Search by last name was executed for:" + " " + lastNameSearchString + " ");
			int i, searchBal;
			uint searchAn, searchPin;
			string searchLName, searchFName;
			aN = 0;
			pin = 0;
			bal = 0;
			fName = "Unknown";
			lName = "Uknown";
			for (i = 0; i < nEntries; i++)
			{
				GetValuesForEntry(i, out searchAn, out searchPin, out searchBal,out searchFName, out searchLName);
				if (searchLName.Equals(lastNameSearchString))
				{
					aN = searchAn;
					pin = searchPin;
					bal = searchBal;
					fName = searchFName;
					lName = searchLName;
				}
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		private void log(string logString)
		{
			LogNum++;
			Console.WriteLine(logString + ", {0} task's have been completed. \n", LogNum);
		}
	}
}