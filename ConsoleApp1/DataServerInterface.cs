
//using System.ServiceModel;
namespace DataTier
{
	[System.ServiceModel.ServiceContract]
	public interface DataServerInterface
	{
		[System.ServiceModel.OperationContract]
		int GetNumEntries();
		[System.ServiceModel.OperationContract]
		void GetValuesForEntry(int index, out uint aN, out uint pin, out int bal, out string fName, out string lName);
	}
}
