using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System;
using System.Diagnostics;
using ItemListView.Contracts;
using ItemListView.Adapters;

namespace ItemListView
{
    public partial class ItemListView : UserControl
    {
        public IBaseAdapter listAdapter;

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
