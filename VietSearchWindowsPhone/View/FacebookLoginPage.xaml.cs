using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Facebook;
using System.IO.IsolatedStorage;
using VietSearchWindowsPhone.FacebookUtility;
using System.Windows.Navigation;
using VietSearchWindowsPhone.Tools;
using VietSearchWindowsPhone.ViewModels;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace VietSearchWindowsPhone.View
{
    public partial class FacebookLoginPage : PhoneApplicationPage
    {
        FacebookClient client;
        AccountViewModel accountViewModel;
        public FacebookLoginPage()
        {
            accountViewModel  = new AccountViewModel();
            InitializeComponent();
            client = new FacebookClient();
            mWebBrowser.Source =FacebookClientHelper.Instance.GetFacebookLoginUrl();
        }
        
        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {

            FacebookOAuthResult oauthResult;
            if (!client.TryParseOAuthCallbackUrl(e.Uri, out oauthResult))
            {
                return;
            }
            if (oauthResult.IsSuccess)
            {
                //Process result
                client.AccessToken = oauthResult.AccessToken;
                FacebookClientHelper.Instance.accessToken = client.AccessToken;
                LoginSucceded(client.AccessToken);
            }
            else
            {
                //Process Error
                MessageBox.Show(oauthResult.ErrorDescription);
                mWebBrowser.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void LoginSucceded(string accessToken)
        {
            var fb = new FacebookClient(accessToken);

            fb.GetCompleted += (o, e) =>
            {
                if (e.Error != null)
                {
                    Dispatcher.BeginInvoke(() => MessageBox.Show(e.Error.Message));
                    return;
                }

                var result = (IDictionary<string, object>)e.GetResultData();
                FacebookClientHelper.Instance.userId = (string)result["id"];

                
                accountViewModel.accountId = FacebookClientHelper.Instance.userId;
                accountViewModel.accountName = (string)result["name"];
                string username = (string)result["username"];
                accountViewModel.isLock = false;

                string uri = "http://graph.facebook.com/" + username + "/picture?type=large";
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
                request.BeginGetResponse(new AsyncCallback(GetAvatarCallBack), request);
                
            };

            fb.GetAsync("me");
        }

        private void GetAvatarCallBack(IAsyncResult asynchronousResult)
        {
            
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    accountViewModel.accountPicture = response.ResponseUri.ToString();
                    
                    response.Close();
                }
                catch
                {
                }
               
                CheckExistAccount(accountViewModel.accountId);
            });
        }

        public void CheckExistAccount(string accountId)
        {
            string uri = App.ACCOUNT_URI + "/Get?accountId=" + accountId;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.BeginGetResponse(new AsyncCallback(GetAccountCallBack), request);
        }
        private void GetAccountCallBack(IAsyncResult asynchronousResult)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();
                    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(AccountViewModel));
                    AccountViewModel accountTemp = (AccountViewModel)jsonSerializer.ReadObject(stream);
                    if (accountTemp == null)
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        DataContractJsonSerializer jsonSer =
                        new DataContractJsonSerializer(typeof(AccountViewModel));
                        jsonSer.WriteObject(memoryStream, accountViewModel);
                        memoryStream.Position = 0;
                        StreamReader sr = new StreamReader(memoryStream);
                        var json = sr.ReadToEnd();
                        var webClient = new WebClient();
                        webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                        webClient.UploadStringCompleted += this.InsertAccountCallBack;
                        webClient.UploadStringAsync(new Uri(App.ACCOUNT_URI), "POST", json);

                    }
                    else
                    {
                        var rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
                        if (rootFrame != null)
                            rootFrame.GoBack();
                        response.Close();
                    }
                }
                catch
                {
                }
            });
        }

        private void InsertAccountCallBack(object sender, UploadStringCompletedEventArgs e)
        {

            var rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame != null)
                rootFrame.GoBack();
        }
        private void InsertAccountCallBack(IAsyncResult asynchronousResult)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                try
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)asynchronousResult.AsyncState;
                    WebResponse response = httpRequest.EndGetResponse(asynchronousResult);
                    Stream stream = response.GetResponseStream();
                    response.Close();
                    
                }
                catch
                {
                } 

            });
        }

        
    }
}