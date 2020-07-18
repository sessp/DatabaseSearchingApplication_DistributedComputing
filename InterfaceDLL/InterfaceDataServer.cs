using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDLL
{
		[ServiceContract]
		public interface InterfaceDataServer
		{
			[OperationContract]
			int GetNumEntries();
			[OperationContract]
			void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName);
		}
}
