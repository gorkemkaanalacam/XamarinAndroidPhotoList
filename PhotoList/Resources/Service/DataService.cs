using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotoList.Resources.Model;
using RestSharp;

namespace PhotoList.Resources.Service
{
    public class DataService
    {
        public List<string> GetImages()
        {
            var client = new RestClient("https://www.flickr.com");

            var request = new RestRequest("services/rest/")
             .AddParameter("method", "flickr.photos.getRecent")
             .AddParameter("api_key", "f5699978eb7579e89c0e8bccc9ff935c")
             .AddParameter("per_page", 20)
             .AddParameter("page", 1)
             .AddParameter("format", "json")
             .AddParameter("nojsoncallback", 1);

            var response = client.Get<RootObject>(request);

            var urlList = new List<string>();

            foreach (var photo in response.Data.photos.photo)
            {
                urlList.Add(String.Format("https://farm{0}.staticflickr.com/{1}/{2}_{3}.jpg", photo.farm, photo.server, photo.id, photo.secret));
            }

            return urlList;
        }
    }
}