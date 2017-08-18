using Xamarin.Forms;

namespace Sample
{
    public class MinimumListView : ListView
    {
        //Retain固定
        public MinimumListView() : base(ListViewCachingStrategy.RetainElement){}
    }
}
