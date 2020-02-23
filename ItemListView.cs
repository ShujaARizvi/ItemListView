using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections;
using System;
using ItemListView.EventArguments;
using System.Diagnostics;
using ItemListView.Contracts;

namespace ItemListView
{
    public partial class ItemListView<T> : UserControl
    {
        private IList itemList;
        private BaseAdapter listAdapter;
        private event EventHandler<ListItemAddEventArgs> OnAdd;
        private event EventHandler<ListItemAddEventArgs> OnRemove;

        public ItemListView()
        {
            InitializeComponent();
            this.OnAdd += ItemListView_OnAdd;
            this.OnRemove += ItemListView_OnRemove;
        }

        /// Adds an Item to the list.
        /// </summary>
        public void Add(T item)
        {
            var insertionIndex = itemList.Add(item);
            OnAdd(this, new ListItemAddEventArgs
            {
                Index = insertionIndex
            });
        }

        /// <summary>
        /// Removes the given item from the list.
        /// </summary>
        /// <typeparam name="T">Generic type given for the list.</typeparam>
        /// <param name="item">Item to be removed from list.</param>
        /// <returns>Returns true if removal is successful, false if item is not found.</returns>
        public bool Remove(T item)
        {
            itemList.Remove(item);
            return true;
        }

        /// <summary>
        /// Removes an item at the given index.
        /// </summary>
        /// <param name="index">The list index to remove item at.</param>
        public void RemoveAt(int index)
        {
            itemList.RemoveAt(index);
        }

        public void SetAdapter(BaseAdapter adapter)
        {
            this.listAdapter = adapter;
        }

        #region EVENT HANDLERS
        private void ItemListView_OnAdd(object sender, ListItemAddEventArgs e)
        {
            Debug.WriteLine(e.Index);
        }

        private void ItemListView_OnRemove(object sender, ListItemAddEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
