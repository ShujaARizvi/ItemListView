using System;

namespace ItemListView.EventArguments
{
    public class ListItemAddEventArgs : EventArgs
    {
        /// <summary>
        /// Index in the list where the item was inserted.
        /// </summary>
        public int Index { get; set; }
    }
}
