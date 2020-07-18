using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using RestSharp;
using Business_GUIClasses;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean waitingState = false;
        private int nEntries = 0;
        private RestClient client;
        public MainWindow()
        {
            InitializeComponent();
            string URL = "https://localhost:44316/";
            client = new RestClient(URL);
        }

        //Method for initiating a service request. 
        private IRestResponse requestService(string url)
        {
            RestRequest request = new RestRequest(url);
            IRestResponse numOfThings = client.Get(request);
            //Error Checking
            if (numOfThings.ResponseStatus == ResponseStatus.Error)
            {
                //Did an error occur, mostly likely because something happen to the BusinessTier.
                throw new ServiceNotAvailableException("Cannot Contact BusinessTier");
            }
            if (numOfThings.IsSuccessful)
            {
                //This won't detect if a http error occurs. 
                Debug.WriteLine("\n" + "Request was successful" + "\n");
            }
            else if(!numOfThings.IsSuccessful)
            {
                //For Debugging Purposes
                Debug.WriteLine("\n" + numOfThings.StatusCode + numOfThings.StatusDescription + "\n");
            }

            return numOfThings;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Perform the index search.
            int i = 0;
            string firstName = "", lastName = "";
            int bal = 0;
            uint acct = 0, pin = 0;
            output.Content = " ";
            try
            {
                //Get Num of entries in the app
                nEntries = Int32.Parse(requestService("api/values/2").Content);
                entryField.Content = nEntries;
                
                i = Int32.Parse(inputfield_index.Text);
                //Bounds check
                if ((i >= 0) && (i < nEntries))
                {
                    //Perform Search
                    IRestResponse response = requestService("api/getvalues/" + i.ToString());
                    Debug.WriteLine("\n" + response.Content + "\n");
                    DataIntermediate data = JsonConvert.DeserializeObject<DataIntermediate>(response.Content);

                    firstName = data.fname;
                    lastName = data.lname;
                    bal = data.bal;
                    acct = data.acct;
                    pin = data.pin;
                    
                    //If the default DataIntermediate was returned, ie no entry was found that matched throw.
                    if (acct == 0 && pin == 0 && bal == 0 && firstName.Equals("") && lastName.Equals(""))
                    {
                        throw new SearchFailedException(" Unable to find an entry with index: " + i);
                    }
                }
                else
                {
                    output.Content = "Please enter a valid index, make sure it is within range";
                }
            }
            catch (FormatException ex)
            {
                output.Content = "Please enter a valid index, make sure it is an integer";
            }
            catch (ArgumentNullException ex)
            {
                output.Content = "Please enter a valid index, make sure you actually input something";
            }
            catch (OverflowException ex)
            {
                output.Content = "Overflow Error";
            }
            catch (ServiceNotAvailableException ex)
            {
                disable();
                output.Content = "There was an error connecting to the business server, Please restart the client!";
            }
            catch (SearchFailedException ex)
            {
                output.Content = "Your search returned negative, no entry had the lastname you specified!";
            }

            //Display
            field_fname.Text = firstName;
            field_lname.Text = lastName;
            field_bal.Text = bal.ToString("C");
            field_actnum.Text = acct.ToString();
            field_pin.Text = pin.ToString("D4");
        }

        public delegate void delegateSearchOp(string searchLastName,out uint acct,out uint pin,out int bal,out string firstName,out string lastName);

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            //Search last name. Haven't re-implemented delegates from Prac 2, touched on this in report.
            delegateSearchOp searchOperation;
            AsyncCallback cBack;
            string firstName = "", lastName = "", searchLastName;
            int bal = 0;
            uint acct = 0, pin = 0;
            output.Content = " ";

            try
            {
                //Get num of values.
                nEntries = Int32.Parse(requestService("api/values/2").Content);
                entryField.Content = nEntries;

                //Create a search obj
                SearchData search = new SearchData();
                search.searchString = inputfield_lastname.Text;

                //Perform search
                RestRequest req = new RestRequest("api/search/");
                req.AddJsonBody(search);
                IRestResponse response = client.Post(req);

                DataIntermediate data = JsonConvert.DeserializeObject<DataIntermediate>(response.Content);

                //Reassign data since we don't like globals :(
                firstName = data.fname;
                lastName = data.lname;
                pin = data.pin;
                acct = data.acct;
                bal = data.bal;

                //Check if it actually found something
                if (acct == 0 && pin == 0 && bal == 0 && firstName.Equals("Unknown") && lastName.Equals("Uknown"))
                {
                    throw new SearchFailedException(" Unable to find an entry with: " + search.searchString);
                }

                //If we did find what we were looking for, display it!
                field_fname.Text = firstName;
                field_lname.Text = lastName;
                field_bal.Text = bal.ToString("C");
                field_actnum.Text = acct.ToString();
                field_pin.Text = pin.ToString("D4");


            }
            catch (FormatException)
            {
                output.Content = "Please enter a valid string";
            }
            catch (ArgumentNullException)
            {
                output.Content = "Please enter a valid string";
            }
            catch (ServiceNotAvailableException ex)
            {
                disable();
                output.Content = "There was an error connecting to the business server, Please restart the client!";
            }
            catch (SearchFailedException ex)
            {
                output.Content = "Your search returned negative, no entry had the lastname you specified!";
            }

            /*Increase the number of entries generated, error handling*/
        }

        //Old Method that isn't used but i'll leave it here for completition sake.
        private void onLastnameSearchCompletion(IAsyncResult asyncResult)
        {
            string firstName = "", lastName = "", searchLastName;
            int bal = 0;
            uint acct = 0, pin = 0;
            delegateSearchOp searchOperation;
            AsyncResult asyncObj = (AsyncResult)asyncResult;

            if (asyncObj.EndInvokeCalled == false)
            {
                searchOperation = (delegateSearchOp)asyncObj.AsyncDelegate;
                searchOperation.EndInvoke(out acct, out pin, out bal, out firstName, out lastName, asyncObj);
                field_fname.Text = firstName;
                field_lname.Text = lastName;
                field_bal.Text = bal.ToString("C");
                field_actnum.Text = acct.ToString();
                field_pin.Text = pin.ToString("D4");
                changeWaitingState();
            }
            asyncObj.AsyncWaitHandle.Close();
        }

        /*Used to change the state of the UI, used in exception handling to stop the program from crashing 
         and was used in the delegate stuff. */
        private void changeWaitingState()
        {
            if (waitingState == true)
            {
                inputfield_index.IsReadOnly = false;
                inputfield_lastname.IsReadOnly = false;
                button_search.IsEnabled = true;
                button_searchLastname.IsEnabled = true;
                waitingState = false;
            }
            else if (waitingState == false)
            {
                disable();
            }
        }

        //More disabling, made this it's own method as the exceptions would need to call it.
        private void disable()
        {
            inputfield_index.IsReadOnly = true;
            inputfield_lastname.IsReadOnly = true;
            button_search.IsEnabled = false;
            button_searchLastname.IsEnabled = false;
            waitingState = true;
        }
    }

    //Exception implementations for my custom exceptions.
    [Serializable]
    internal class SearchFailedException : Exception
    {
        public SearchFailedException()
        {
        }

        public SearchFailedException(string message) : base(message)
        {
        }

        public SearchFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SearchFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class ServiceNotAvailableException : Exception
    {
        public ServiceNotAvailableException()
        {
        }

        public ServiceNotAvailableException(string message) : base(message)
        {
        }

        public ServiceNotAvailableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ServiceNotAvailableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
