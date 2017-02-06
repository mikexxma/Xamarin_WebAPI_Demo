using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinWebApi
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            string s = CallWebService("/languages/true");
            System.Diagnostics.Debug.WriteLine("Mike " + s);
            MyLabel.Text = s;
        }



        public string CallWebService(string ps_URI)
        {
            HttpClient lobj_HTTPClient = null;
            HttpResponseMessage lobj_HTTPResponse = null;
            string ls_Response = "";
            //string ls_Prefix = "";
            //We assume the internet is available. 
            try
            {
               // ls_Prefix = Device.OnPlatform<string>(App.APISecurePrefix, App.APIPrefix, App.APIPrefix);
                //Get the Days of the Week
                //lobj_HTTPClient = new HttpClient(new NativeMessageHandler());
                lobj_HTTPClient = new HttpClient();
                lobj_HTTPClient.BaseAddress = new Uri("https://www.cgsapi.com/");
                lobj_HTTPClient.DefaultRequestHeaders.Accept.Clear();
                lobj_HTTPClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var lobj_Result = lobj_HTTPClient.GetAsync(ps_URI);

                while (!lobj_Result.IsCompleted)
                {
                    System.Diagnostics.Debug.WriteLine(lobj_Result);
                    Task.Delay(100);
                }
                //GEt the http response object
                lobj_HTTPResponse = lobj_Result.Result;

                if (!lobj_HTTPResponse.IsSuccessStatusCode)
                {
                    //App.ProcessException(new Exception(lobj_HTTPResponse.ReasonPhrase));
                    System.Diagnostics.Debug.WriteLine("Mike "+lobj_HTTPResponse.IsSuccessStatusCode);
                }
                else
                {
                    var lobj_DataResult = lobj_HTTPResponse.Content.ReadAsStringAsync();

                    while (!lobj_DataResult.IsCompleted)
                    {
                        System.Diagnostics.Debug.WriteLine("Mike " + "WebAPICaller-CallWebService-2");
                        Task.Delay(100);
                    }
                    ls_Response = lobj_DataResult.Result;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Mike ex" + ex.Data);
            }
            finally
            {
                if (lobj_HTTPClient != null)
                    lobj_HTTPClient.Dispose();
                if (lobj_HTTPResponse != null)
                {
                    lobj_HTTPResponse.Dispose();
                }
                System.Diagnostics.Debug.WriteLine("Mike" + "WebAPICaller-CallWebService-1: Done");
            }

            return ls_Response;

        }
    }
}
