using System;
using System.Windows.Forms;
using ItemListView.Contracts;
using ItemListView.EventArgs;

namespace ItemListView
{
    public partial class ItemListView : UserControl
    {
        private IBaseAdapter listAdapter;
        internal event Func<object,OnItemClickEventArgs<Control>, object> OnItemClick;
        
        public event EventHandler<SelectedIndexChangedEventArgs> SelectedIndexChanged;

        public int SelectedIndex { get; internal set; }

        /// <summary>
        /// UI Elements that are displayed on the list.
        /// Access to them give developers ability to modify the controls at their will.
        /// </summary>
        public ControlCollection ListElements {
            get {
                return listAdapter.GetListControls();
            }
        }

        internal Panel Panel 
        {
            get 
            {
                return this.ListPanel;
            }
        }

        public ItemListView()
        {
            InitializeComponent();
            SelectedIndex = -1;
        }

        internal void TriggerItemClick(object sender, int index, Control control)
        {
            SelectedIndex = index;
            this.OnItemClick?.Invoke(sender, new OnItemClickEventArgs<Control> { Index = index, Item = control });
            this.SelectedIndexChanged?.Invoke(sender, new SelectedIndexChangedEventArgs { Index = index });
        }

        public void SetAdapter(IBaseAdapter adapter)
        {
            this.listAdapter = adapter;
            adapter.Bind(this);
        }

        public void SetOnItemClickListener(Func<object, OnItemClickEventArgs<Control>, object> itemClickListener)
        {
            OnItemClick += itemClickListener;
        }

        public void Deselect()
        {
            this.SelectedIndex = -1;
        }


        public void Deselect(Func<object, object> method)
        {
            method(new object());
        }
    }
}
