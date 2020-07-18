using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Web;
using DataTier;
namespace BusinessWebService.Models
{
    public class DataModel
    {
		//Connect to data tier via .NET remoting and allows for usual services (getnumentries
		//and so on.
		private static DataModel singletonDataModel = null;
		private uint LogNum = 0;
		private int nEntries = 0;
		private DataServerInterface dataServer;

		//Singleton implementation
		public static DataModel getInstance() 
		{
			if (singletonDataModel == null)
			{
				singletonDataModel = new DataModel();
			}
			return singletonDataModel;
		}
		private DataModel()
		{
			//Connect to the DataTier remote server and store in a local variable for use throughout the class.
			ChannelFactory<DataServerInterface> cFactory;
			NetTcpBinding tcp = new NetTcpBinding();

			string URL = "net.tcp://localhost:8100/DataService";
			cFactory = new ChannelFactory<DataServerInterface>(tcp, URL);
			dataServer = cFactory.CreateChannel();
		}

		//Method to get num of entries!
		public int GetNumEntries()
		{
			log("Request for number of entries in DB was executed ");
			nEntries = dataServer.GetNumEntries();
			return nEntries;
		}

		//Method to get values for entry.
		public void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName)
		{
			log("Search for index was executed for index:" + " " + index + " ");
			dataServer.GetValuesForEntry(index, out aN, out pin, out bal, out fName, out lName);
		}
		
		//Method to search via last name.
		public void SearchByLastName(string lastNameSearchString, out uint aN, out uint pin, out int bal, out string fName, out string lName)
		{
			log("Search by last name was executed for:" + " " + lastNameSearchString + " ");
			//Set initial values so we know if search failed or not.
			int i, searchBal;
			uint searchAn, searchPin;
			string searchLName, searchFName;
			aN = 0;
			pin = 0;
			bal = 0;
			fName = "Unknown";
			lName = "Uknown";

			//Go through entries and.... search!
			for (i = 0; i < nEntries; i++)
			{
				GetValuesForEntry(i, out searchAn, out searchPin, out searchBal, out searchFName, out searchLName);
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

		//Logging method.
		[MethodImpl(MethodImplOptions.Synchronized)]
		private void log(string logString)
		{
			LogNum++;
			Debug.WriteLine(logString + ", {0} task's have been completed. \n", LogNum);
		}
	}
}