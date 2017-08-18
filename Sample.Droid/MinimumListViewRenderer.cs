using System.Collections;
using Android.Content;
using Android.Views;
using Android.Widget;
using Sample;
using Sample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MinimumListView), typeof(MinimumListViewRenderer))]
namespace Sample.Droid
{
    public class MinimumListViewRenderer : ViewRenderer<MinimumListView, Android.Widget.ListView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MinimumListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var listview = new Android.Widget.ListView(Context);
                listview.Focusable = false;
                listview.DescendantFocusability = DescendantFocusability.AfterDescendants;
                listview.Adapter = new AiListViewAdapter(Context, Element);
                AutoPackage = false;
                SetNativeControl(listview);
            }
        }

    }

    public class AiListViewAdapter : BaseAdapter<object>
    {
        MinimumListView _listview;
        IList _source;
        Context _context;

        public AiListViewAdapter(Context context, MinimumListView listview)
        {
            _context = context;
            _listview = listview;
            _source = _listview.ItemsSource as IList;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            //FormsCell取得 TemplatedItemsはEditorBrowsableState.Neverなのでインテリセンスに出てこない
            var formsCell = _listview.TemplatedItems[position];
           
            //NativeCell取得（AndroidはCellFactoryなる便利なものがあったのでありがたく使わせてもらう）
            var nativeCell = CellFactory.GetCell(formsCell, convertView, parent, _context, _listview);

            //セルの高さ設定
            //（ViewCellはこの指定では反映されないのでListViewのRowHeightプロパティで設定する）
            nativeCell.SetMinimumHeight((int)_context.ToPixels(44));

            return nativeCell;
        }

        //データソースのItemを返すインデクサ
        public override object this[int position] {
            get {
                return _source[position];
            }
        }

        //Listの全行数
        public override int Count {
            get {
                return _source.Count;
            }
        }

        //Idを返す（特に無いのでpositionを返しておく）
        public override long GetItemId(int position)
        {
            return position;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                _source = null;
                _listview = null;
            }
            base.Dispose(disposing);
        }

    }
}
