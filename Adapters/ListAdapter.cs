﻿using ItemListView.Contracts;
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
        // List view instance bound with this adapter
        public ItemListView listView;

        private int position = 5;
        private readonly int factor = 2;

        public ListAdapter(List<Item> items)
        {
            dataItems = items;
            uiListElements = new List<Control>();
            PopulateUIElementsList();
        }

        public void Bind(UserControl listView)
        {
            this.listView = listView as ItemListView;
            itemsPanel = this.listView.Panel;
            foreach(var element in uiListElements)
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
