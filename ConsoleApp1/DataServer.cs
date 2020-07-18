using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DatabaseLibrary;
namespace DataTier
{
	//switch the dataserverinterface
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,UseSynchronizationContext = false)]
	internal class DataServer : DataServerInterface
	{
		private DatabaseClass dc;
		public DataServer()
		{
			 dc = new DatabaseClass();
		}

		//Method to get the total number of entries.
		public int GetNumEntries()
		{
			return dc.GetNumRecords();
		}

		//Method for getting values for a single entry
		public void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName)
		{
			aN = dc.GetAcctNoByIndex(index);
			pin = dc.GetPINByIndex(index);
			bal = dc.GetBalanceByIndex(index);
			fName = dc.GetFirstNameByIndex(index);
			lName = dc.GetLastNameByIndex(index);
		}

		//As instructed from Prac 2, create the remote server and host the data server.
		static void Main(string[] args)
		{
			Console.WriteLine("My DataServer");
			ServiceHost h;
			NetTcpBinding tcp = new NetTcpBinding();

			h = new ServiceHost(typeof(DataServer));

			h.AddServiceEndpoint(typeof(DataServerInterface), tcp, "net.tcp://0.0.0.0:8100/DataService");
			h.Open();
			Console.WriteLine("Data Server is fully operational");
			Console.ReadLine();
			h.Close();
		}


	}
}
