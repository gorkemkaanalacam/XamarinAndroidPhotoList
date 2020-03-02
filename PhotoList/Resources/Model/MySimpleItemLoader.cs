using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotoList.Resources.Helper;
using PhotoList.Resources.Service;

namespace PhotoList.Resources.Model
{
    public class MySimpleItemLoader
    {
        public List<MySimpleItem> MySimpleItems { get; private set; }
        public bool IsBusy { get; set; }
        public int CurrentPageValue { get; set; }
        public bool CanLoadMoreItems { get; private set; }
        public List<string> Data { get; set; }

        public MySimpleItemLoader()
        {
            MySimpleItems = new List<MySimpleItem>();
        }

        public void LoadMoreItems(int itemsPerPage)
        {
            if(Data == null)
            {
                Data = new DataService().GetImages();
            }
            else
            {
                Data.AddRange(new DataService().GetImages());
            }
            IsBusy = true;
            for (int i = CurrentPageValue; i < CurrentPageValue + itemsPerPage; i++)
            {
                var imageBitmap = BitmapHelper.GetImageBitmapFromUrl(Data[i]);
                MySimpleItems.Add(new MySimpleItem() { ImageBitmap = imageBitmap });
            }
            // normally you'd check to see if the number of items returned is less than 
            // the number requested, i.e. you've run out, and then set this accordinly.
            CanLoadMoreItems = true;
            CurrentPageValue = MySimpleItems.Count;
            IsBusy = false;
        }
    }
}