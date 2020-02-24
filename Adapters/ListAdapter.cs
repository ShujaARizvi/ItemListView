using ItemListView.Contracts;
using ItemListView.EventArgs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ItemListView.Adapters
{
    public abstract class ListAdapter<Control, Item> : IListAdapter<Control, Item> where Control : System.Windows.Forms.Control, new()
    {
        private List<Control> uiListElements;
        private List<Item> dataItems;
        private Panel itemsPanel;

        private int position = 5;
        private readonly int factor = 2;

        private event EventHandler<OnItemClickEventArgs<Control>> OnItemClick;

        public ListAdapter(List<Item> items)
        {
            dataItems = items;
            uiListElements = new List<Control>();
            OnItemClick += OnItemClickListener;
            PopulateUIElementsList();
        }


        public void Bind(Panel listPanel)
        {
            itemsPanel = listPanel;
            foreach(var element in uiListElements)
            {
                listPanel.Controls.Add(element);
                element.Top = position;
                position = element.Top + element.Height + factor;
            }
        }
        
        public void NotifyDataSetChanged()
        {
            PopulateUIElementsList();
            itemsPanel.Controls.Clear();
            position = 5;
            this.Bind(itemsPanel);
        }

        private void PopulateUIElementsList()
        {
            uiListElements?.Clear();
            int index = 0;
            foreach (var item in dataItems)
            {
                Control curElement = this.Draw(new Control(), item);
                curElement.Click += (s, e) =>
                {
                    OnItemClick(s, new OnItemClickEventArgs<Control> { Index = index++, Item = curElement });
                };
                uiListElements.Add(curElement);
            }
        }
        
        public virtual void OnItemClickListener(object sender, OnItemClickEventArgs<Control> e) {}

        public abstract Control Draw(Control control, Item item);
    }
}
