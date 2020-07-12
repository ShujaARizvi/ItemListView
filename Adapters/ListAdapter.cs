using ItemListView.Contracts;
using ItemListView.EventArgs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ItemListView.Adapters
{
    public abstract class ListAdapter<Control, Item> : IListAdapter<Control, Item> where Control : System.Windows.Forms.Control, new()
    {
        private System.Windows.Forms.Control optionalHeader;
        private List<Control> uiListElements;
        private List<Item> dataItems;
        private Panel itemsPanel;
        // List view instance bound with this adapter
        private ItemListView listView;

        private int position = 5;
        private readonly int factor = 2;

        public ListAdapter(List<Item> items)
        {
            dataItems = items;
            uiListElements = new List<Control>();
            PopulateUIElementsList();
        }

        public ListAdapter(List<Item> items, System.Windows.Forms.Control header)
        {
            dataItems = items;
            optionalHeader = header;
            uiListElements = new List<Control>();
            PopulateUIElementsList();
        }

        public void Bind(UserControl listView)
        {
            this.listView = listView as ItemListView;
            itemsPanel = this.listView.Panel;
            if (optionalHeader != null)
            {
                itemsPanel.Controls.Add(optionalHeader);
                optionalHeader.Top = position;
                position = optionalHeader.Top + optionalHeader.Height + factor;
            }
            foreach (var element in uiListElements)
            {
                itemsPanel.Controls.Add(element);
                element.Top = position;
                position = element.Top + element.Height + factor;
            }
        }

        public void NotifyDataSetChanged()
        {
            PopulateUIElementsList();
            itemsPanel.Controls.Clear();
            position = 5;
            this.listView.SelectedIndex = -1;
            this.Bind(listView);
        }

        private void PopulateUIElementsList()
        {
            uiListElements?.Clear();
            int index = 0;
            foreach (var item in dataItems)
            {
                Control curElement = this.Draw(new Control(), item);
                curElement.Name = index.ToString();
                index++;
                curElement.Click += (s, e) =>
                {
                    this.listView.TriggerItemClick(s, Convert.ToInt32(curElement.Name), curElement);
                };
                uiListElements.Add(curElement);
            }
        }
        

        public abstract Control Draw(Control control, Item item);

        System.Windows.Forms.Control.ControlCollection IBaseAdapter.GetListControls()
        {
            return itemsPanel.Controls;
        }
    }
}
