using Xamarin.Forms;

namespace Sample
{
    public class MinimumListView : ListView
    {
        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = newValue as Element;
            if (element == null)
                return;
            element.Parent = (Element)bindable;
        }

        //Retain固定
        public MinimumListView() : base(ListViewCachingStrategy.RetainElement){}
    }
}
