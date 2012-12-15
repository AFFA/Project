using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    /// <summary>
    /// FinData Data Access Object, mis sisaldab listi kõigist FinData objektidest
    /// </summary>
    public class FinDataDao
    {
        private List<FinData> _finDatas;

        public List<FinData> FinDatas
        {
            get { return _finDatas; }
        }

        public FinDataDao()
        {
            _finDatas = new List<FinData>();
        }

        /// <summary>
        /// Listi uue FinData objekti lisamine, kui sama kuupäevaga objekt juba eksisteerib, siis uuendatakse objekti andmeid, muidu luuakse uus objekt.
        /// </summary>
        /// <param name="fd"></param>
        public void AddFinData(FinData fd)
        {
            FinData finData = GetFinData(fd.Kuupaev);
            if (finData == null)
            {
                _finDatas.Add(fd);
            }
            else
            {
                finData.CopyValues(fd);
            }

        }

        /// <summary>
        /// Kuupäeva järgi FinData objekti leidmine
        /// </summary>
        /// <param name="kp">Kuupäev</param>
        /// <returns>FinData objekt</returns>
        public FinData GetFinData(DateTime kp)
        {
            foreach (FinData finData in _finDatas)
            {
                if (kp.Equals(finData.Kuupaev))
                {
                    return finData;
                }
            }
            return null;
        }

        /// <summary>
        /// FinData listi sorteerimine väiksemast suuremani kuupäeva järgi
        /// </summary>
        public void SortFinDatas()
        {
            //_finDatas.Sort((x, y) => DateTime.Compare(x.Kuupaev, y.Kuupaev)); // asc sort
            //_finDatas.Reverse(); // asc reverse
            //_finDatas.Sort((x, y) => y.Kuupaev.CompareTo(x.Kuupaev)); // desc sort
            _finDatas.Sort((x, y) => x.Kuupaev.CompareTo(y.Kuupaev)); // asc sort
        }


    }
}
