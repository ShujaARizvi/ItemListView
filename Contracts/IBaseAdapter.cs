using System.Collections;
using System.Windows.Forms;

namespace ItemListView.Contracts
{
    public interface IBaseAdapter
    {
        Control.ControlCollection GetListControls();
        /// <summary>
        /// Populates the given panel with UI Elements.
        /// </summary>
        /// <param name="listPanel">Panel to be populated.</param>
        void Bind(UserControl listView);
        /// <summary>
        /// Notifies the adapter that the list/dataset has changed, hence triggering the re-rendering.
        /// </summary>
        void NotifyDataSetChanged();
    }
}
