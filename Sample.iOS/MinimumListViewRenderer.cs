using System;
using System.Collections;
using Foundation;
using Sample;
using Sample.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MinimumListView), typeof(MinimumListViewRenderer))]
namespace Sample.iOS
{
    public class MinimumListViewRenderer : ViewRenderer<MinimumListView, UITableView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<MinimumListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null) {
                var tableView = new UITableView();
                tableView.ScrollEnabled = true;
                tableView.RowHeight = 44;
                tableView.Source = new MyTableSource(Element);

                SetNativeControl(tableView);
            }
        }
    }

    public class MyTableSource : UITableViewSource
    {
        MinimumListView _listview;
        IList _source;
        bool _disposed;

        public MyTableSource(MinimumListView listview)
        {
            _listview = listview;
            _source = _listview.ItemsSource as IList;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //FormsCell取得 TemplatedItemsはEditorBrowsableState.Neverなのでインテリセンスに出てこない
            var cell = _listview.TemplatedItems[indexPath.Row];

            var id = cell.GetType().FullName;
            var renderer = (CellRenderer)Xamarin.Forms.Internals.Registrar.Registered.GetHandler<IRegisterable>(cell.GetType());

            //リサイクルCell取得
            var reusableCell = tableView.DequeueReusableCell(id);
            //NativeCell取得
            var nativeCell = renderer.GetCell(cell, reusableCell, tableView);

            return nativeCell;
        }

        //セクションの数
        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;  
        }

        //1セクションの行数
        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _source.Count;
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing) {
                _source = null;
                _listview = null;
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}
