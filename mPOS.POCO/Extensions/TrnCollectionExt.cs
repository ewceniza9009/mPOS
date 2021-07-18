using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mPOS.POCO
{
    public partial class TrnCollection
    {
        public List<TrnCollectionLine> TrnCollectionLines
        {
            get => _TrnCollectionLines;
            set => SetProperty(ref _TrnCollectionLines, value);
        }
        private List<TrnCollectionLine> _TrnCollectionLines;
    }
}
