using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemListView.Contracts
{
    public interface IListAdapter<Control, Item> : IBaseAdapter
    {
        /// <summary>
        /// Tells how to populate the given Item object data on the Control aka helpers in drawing the UI.
        /// </summary>
        /// <param name="control">Control to be drawn on UI.</param>
        /// <param name="item">Object that contains the data for the UI, aka, the Model.</param>
        /// <returns></returns>
        Control Draw(Control control, Item item);
    }
}
