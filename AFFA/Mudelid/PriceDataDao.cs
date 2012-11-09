using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    public class PriceDataDao
    {
        private List<PriceData> _priceDatas;

        public void AddData(PriceData[] priceDatas)
        {
            _priceDatas = priceDatas.ToList();
        }


        public List<PriceData> PriceDatas
        {
            get { return _priceDatas; }
            set { _priceDatas = value; }
        }

        public void SortPriceDatas()
        {
            _priceDatas.Sort((x, y) => y.PriceDate.CompareTo(x.PriceDate)); // desc sort
            //_priceDatas.Sort((x, y) => x.PriceDate.CompareTo(y.PriceDate)); // asc sort
        }
    }
}
