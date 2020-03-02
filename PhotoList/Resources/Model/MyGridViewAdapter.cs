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
    public class MyGridViewAdapter : BaseAdapter<MySimpleItem>
    {
        private readonly MySimpleItemLoader _mySimpleItemLoader;
        private readonly Context _context;

        public MyGridViewAdapter(Context context, MySimpleItemLoader mySimpleItemLoader)
        {
            _context = context;
            _mySimpleItemLoader = mySimpleItemLoader;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _mySimpleItemLoader.MySimpleItems[position];

            View itemView = convertView ?? LayoutInflater.From(_context).Inflate(Resource.Layout.MyGridViewCell, parent, false);
            var tvDisplayName = itemView.FindViewById<ImageView>(Resource.Id.tvDisplayName);
            var imgThumbail = itemView.FindViewById<ImageView>(Resource.Id.imgThumbnail);

            imgThumbail.SetScaleType(ImageView.ScaleType.CenterCrop);
            imgThumbail.SetPadding(8, 8, 8, 8);

            //new DataService().GetImages();

            tvDisplayName.SetImageBitmap(item.ImageBitmap);
            //imgThumbail.SetImageResource(Resource.Drawable.Icon);

            return itemView;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return _mySimpleItemLoader.MySimpleItems.Count; }
        }

        public override MySimpleItem this[int position]
        {
            get { return _mySimpleItemLoader.MySimpleItems[position]; }
        }
    }
}