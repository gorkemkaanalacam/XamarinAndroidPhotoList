using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using PhotoList.Resources.Model;
using Android.Util;

namespace PhotoList
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private GridView _gridView;
        private MySimpleItemLoader _mySimpleItemLoader;
        private MyGridViewAdapter _gridviewAdapter;

        private readonly object _scrollLockObject = new object();

        private const int ItemsPerPage = 20;
        private const int LoadNextItemsThreshold = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            _mySimpleItemLoader = new MySimpleItemLoader();
            _mySimpleItemLoader.LoadMoreItems(ItemsPerPage);

            _gridView = FindViewById<GridView>(Resource.Id.gridView);
            _gridviewAdapter = new MyGridViewAdapter(this, _mySimpleItemLoader);
            _gridView.Adapter = _gridviewAdapter;
            _gridView.Scroll += KeepScrollingInfinitely;
        }

        private void KeepScrollingInfinitely(object sender, AbsListView.ScrollEventArgs args)
        {
            lock (_scrollLockObject)
            {
                var mustLoadMore = args.FirstVisibleItem + args.VisibleItemCount >= args.TotalItemCount - LoadNextItemsThreshold;
                if (mustLoadMore && _mySimpleItemLoader.CanLoadMoreItems && !_mySimpleItemLoader.IsBusy)
                {
                    _mySimpleItemLoader.IsBusy = true;
                    Log.Info("tag", "Requested to load more items");
                    _mySimpleItemLoader.LoadMoreItems(ItemsPerPage);
                    _gridviewAdapter.NotifyDataSetChanged();
                    _gridView.InvalidateViews();
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}