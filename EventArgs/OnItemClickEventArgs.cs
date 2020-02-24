using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemListView.EventArgs
{
    public class OnItemClickEventArgs<T>
    {
        public T Item { get; set; }
        public int Index { get; set; }
    }
}
