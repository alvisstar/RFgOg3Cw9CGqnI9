using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using Facebook;

namespace VietSearchWindowsPhone.FacebookUtility
{
    public class FacebookClientHelper
    {
        FacebookClient client;
        private static FacebookClientHelper instance;
        private string _accessToken;
        private string _userId;
        private static readonly IsolatedStorageSettings appSettings = IsolatedStorageSettings.ApplicationSettings;

        private String appId = "610255289024686";
        private String clientSecret = "35ad782b9afd911460f7178e490a4aff";
        private String scope = "user_about_me,read_stream,publish_stream";
        private const string extendedPermissions = "user_about_me,read_stream,publish_stream,user_checkins";
        public FacebookClientHelper()
        {
            try
            {
                _accessToken = (string)appSettings["accessToken"];
            }
            catch
            {
                _accessToken = "";
            }
            try
            {
                _userId = (string)appSettings["userId"];
            }
            catch
            {
                _userId = "";
            }
            client = new FacebookClient();
        }
        public Uri GetFacebookLoginUrl()
        {
            //var parameters = new Dictionary<string, object>();
            //parameters["client_id"] = appId;
            //parameters["redirect_uri"] = "https://www.facebook.com/connect/login_success.html";
            //parameters["response_type"] = "token";
            //parameters["display"] = "touch";

            return new Uri("https://m.facebook.com/dialog/oauth?client_id=" + appId + "&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=" + scope + "&display=touch&response_type=token");
            // add the 'scope' only if we have extendedPermissions.
            //if (!string.IsNullOrEmpty(extendedPermissions))
            //{
            //    // A comma-delimited list of permissions
            //    parameters["scope"] = extendedPermissions;
            //}

            //return client.GetLoginUrl(parameters);
        }
        private void PostToWall(string status)
        {
            //var parameters = new Dictionary<string, object>();
            //parameters["message"] = status;
            //parameters["place"] = "aaa";
            //client.PostAsync("me/checkins", parameters);
            //Dictionary<string, object> position = new System.Collections.Generic.Dictionary<string, object>();
           // position.Add("latitude", 25.04098367f);
           // position.Add("longitude", 121.54597771f);

            Dictionary<string, object> parameters = new System.Collections.Generic.Dictionary<string, object>();
           // parameters.Add("access_token", _accessToken);
            parameters.Add("message", status);
           // parameters.Add("place", "61025528902468");
           // parameters.Add("coordinates", position);
            client.PostAsync("me/news.reads", parameters);
        }

        public static FacebookClientHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new FacebookClientHelper();
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        public string accessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value;
                if (_accessToken.Equals(""))
                    appSettings.Remove("accessToken");
                else
                    appSettings.Add("accessToken", _accessToken);
            }
        }

        public string userId
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = value;
                if (_userId.Equals(""))
                    appSettings.Remove("userId");
                else
                    appSettings.Add("userId", _userId);
            }
        }


        public void getShop()
        {
            
            //return name
            
                //MessageBox.Show("you are login as");
                var fbClient = new FacebookClient(_accessToken);

                fbClient.GetCompleted += new EventHandler<FacebookApiEventArgs>(onGetShopCompleted);//another example

                Dictionary<string, object> searchParams = new Dictionary<string, object>();
                //searchParams.Add("q", "coffee");


                searchParams.Add("center", "10.779377" + "," + "106.696545");
                searchParams.Add("type", "place");
                searchParams.Add("distance", "1000");

                fbClient.GetAsync("/search", searchParams);
            
        }

        private void onGetShopCompleted(object sender, FacebookApiEventArgs e)
        {
            var a = App.Current as App;
            if (e.Error == null)
            {
                var result = (IDictionary<string, object>)e.GetResultData();
 
                // JSON extraction here
               
 
                
 
            }
            else
            {
                //MessageBox.Show(e.Error.Message);
            }
        
 
      }

        public virtual String GetLoginUrl()
        {
            return "https://m.facebook.com/dialog/oauth?client_id=" + appId + "&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=" + scope + "&display=touch";
        }

        public virtual String GetAccessTokenRequestUrl(string code)
        {
            return "https://graph.facebook.com/oauth/access_token?client_id=" + appId + "&redirect_uri=https://www.facebook.com/connect/login_success.html&client_secret=" + clientSecret + "&code=" + code;
        }

        public virtual String GetAccessTokenExchangeUrl(string accessToken)
        {
            return "https://graph.facebook.com/oauth/access_token?client_id=" + appId + "&client_secret=" + clientSecret + "&grant_type=fb_exchange_token&fb_exchange_token=" + accessToken;
        }

        public void PostMessageOnWall(string message, UploadStringCompletedEventHandler handler)
        {
            WebClient client = new WebClient();
            client.UploadStringCompleted += handler;
            client.UploadStringAsync(new Uri("https://graph.facebook.com/me/feed"), "POST", "message=" + HttpUtility.UrlEncode(message) + "&access_token=" + FacebookClientHelper.Instance.accessToken);
        }

        public void ExchangeAccessToken(UploadStringCompletedEventHandler handler)
        {
            WebClient client = new WebClient();
            client.UploadStringCompleted += handler;
            client.UploadStringAsync(new Uri(GetAccessTokenExchangeUrl(FacebookClientHelper.Instance.accessToken)), "POST", "");
        }
    }
}
