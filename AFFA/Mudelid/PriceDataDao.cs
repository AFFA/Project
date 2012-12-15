using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    /// <summary>
    /// Hinnaandmete (sh nii aktsia kui indeksi) Data Access Object
    /// </summary>
    public class PriceDataDao
    {
        private List<PriceData> _priceDatas;
        private List<PriceData> _indexDatas;

        public void AddData(PriceData[] priceDatas)
        {
            _priceDatas = priceDatas.ToList();
        }

        public void AddIndexData(PriceData[] priceDatas)
        {
            _indexDatas = priceDatas.ToList();
        }


        public List<PriceData> PriceDatas
        {
            get { return _priceDatas; }
            set { _priceDatas = value; }
        }

        public List<PriceData> IndexDatas
        {
            get { return _indexDatas; }
            set { _indexDatas = value; }
        }

        /// <summary>
        /// Sorteeri aktsia hinnad kuupäeva järgi suurimast väiksemani
        /// </summary>
        public void SortPriceDatas()
        {
            _priceDatas.Sort((x, y) => y.PriceDate.CompareTo(x.PriceDate)); // desc sort
            //_priceDatas.Sort((x, y) => x.PriceDate.CompareTo(y.PriceDate)); // asc sort
        }

        /// <summary>
        /// Leiab FinData jaoks  hinna, mis on võrdne või väiksem FinData kuupäevast (FinData kuupäev võib olla ka nädalavahetusel, kus börsihinda samal kuupäeval pole)
        /// </summary>
        /// <param name="kp">Kuupäev</param>
        /// <param name="priceDatas">Hindade list</param>
        /// <returns>Double? array [Close, AdjClose]</returns>
        public double?[] GetClosePrice(DateTime kp, List<PriceData> priceDatas) // FinData jaoks leiab hinna, mis on võrdne või väiksem FinData kuupäevast
        {
            PriceData pdd = priceDatas.Find(pd => kp.CompareTo(pd.PriceDate) >= 0);
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
