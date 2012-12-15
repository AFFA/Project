using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
    /// <summary>
    /// DcfData objektide Data Access Object
    /// </summary>
    public class DcfDataDao
    {
        private List<DcfData> _dcfDatas;

        public List<DcfData> DcfDatas
        {
            get { return _dcfDatas; }
        }

        public DcfDataDao()
        {
            _dcfDatas = new List<DcfData>();
        }
        /// <summary>
        /// Lisab DcfData objekte listi
        /// </summary>
        /// <param name="fd">DcfData objekt</param>
        public void AddDcfData(DcfData fd)
        {
            _dcfDatas.Add(fd);
        }

        public void ClearDcfData()
        {
            _dcfDatas = new List<DcfData>();
        }

        /// <summary>
        /// Leiab kuupäeva järgi DcfData objekti
        /// </summary>
        /// <param name="kp">Kuupäev</param>
        /// <returns>Leitud DcfData objekt</returns>
        public DcfData GetDcfData(DateTime kp)
        {
            foreach (var dcfData in _dcfDatas)
            {
                if (kp.Equals(dcfData.Kuupaev))
                {
                    return dcfData;
                }
            }
            return null;
        }

        /// <summary>
        /// Sorteerib objektid väiksemast suuremani
        /// </summary>
        public void SortDcfDatas()
        {
            //_finDatas.Sort((x, y) => DateTime.Compare(x.Kuupaev, y.Kuupaev)); // asc sort
            //_finDatas.Reverse(); // asc reverse
            //_finDatas.Sort((x, y) => y.Kuupaev.CompareTo(x.Kuupaev)); // desc sort
            _dcfDatas.Sort((x, y) => x.Kuupaev.CompareTo(y.Kuupaev)); // asc sort
        }
    }
}
