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

        public double?[] GetClosePrice(DateTime kp) // FinData jaoks leiab hinna, mis on võrdne või väiksem FinData kuupäevast
        {
            PriceData pdd = _priceDatas.Find(pd => kp.CompareTo(pd.PriceDate) >= 0);
            if (pdd != null)
            {
                return new double?[2] { (double)pdd.ClosePrice, (double)pdd.AdjClose };
            }
            else
            {
                return new double?[2] { null, null };
            }
            /*foreach (var priceData in _priceDatas)
            {
                if (kp.CompareTo(priceData.PriceDate) >= 0)
                {
                    return (double) priceData.ClosePrice;
                }
            }
            return null;*/
        }
    }
}
