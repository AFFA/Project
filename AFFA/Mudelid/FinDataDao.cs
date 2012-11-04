using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
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

        public void AddFinData(FinData fd)
        {
            _finDatas.Add(fd);
        }

        public FinData GetFinData(DateTime kp)
        {
            foreach (var finData in FinDatas)
            {
                if (kp.Equals(finData.Kuupaev))
                {
                    return finData;
                }
            }
            return null;
        }
        public void SortFinDatas()
        {
            //_finDatas.Sort((x, y) => DateTime.Compare(x.Kuupaev, y.Kuupaev)); // asc sort
            //_finDatas.Reverse(); // asc reverse
            //_finDatas.Sort((x, y) => y.Kuupaev.CompareTo(x.Kuupaev)); // desc sort
            _finDatas.Sort((x, y) => x.Kuupaev.CompareTo(y.Kuupaev)); // asc sort
        }
    }
}
