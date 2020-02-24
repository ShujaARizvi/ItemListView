using ItemListView.Contracts;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ItemListView.Adapters
{
    public abstract class ListAdapter<Control, Item> : IListAdapter<Control, Item> where Control : System.Windows.Forms.Control, new()
    {
        private List<Control> uiListElements;
        private List<Item> dataItems;
        private Panel itemsPanel;
        public ListAdapter(List<Item> items)
        {
            dataItems = items;
            uiListElements = new List<Control>();
            foreach (var item in dataItems)
            {
                uiListElements.Add(Draw(new Control(), item));
            }
        }
        public void Bind(Panel listPanel)
        {
            itemsPanel = listPanel;
            foreach(var element in uiListElements)
            {
                listPanel.Controls.Add(element);
            }
        }
        
        public void NotifyDataSetChanged()
        {
            uiListElements.Clear();
            foreach (var item in dataItems)
            {
                uiListElements.Add(Draw(new Control(), item));
            }
            itemsPanel.Controls.Clear();
            this.Bind(itemsPanel);
        }

        
        public abstract Control Draw(Control control, Item item);

    }
}
