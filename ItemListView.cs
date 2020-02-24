using System.Windows.Forms;
using ItemListView.Contracts;

namespace ItemListView
{
    public partial class ItemListView : UserControl
    {
        private IBaseAdapter listAdapter;

        public ItemListView()
        {
            InitializeComponent();
        }

        public void SetAdapter(IBaseAdapter adapter)
        {
            this.listAdapter = adapter;
            adapter.Bind(this.ListPanel);
        }
    }
}
