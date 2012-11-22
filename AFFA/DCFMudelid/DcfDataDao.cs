using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.DCFMudelid
{
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

        public void AddDcfData(DcfData fd)
        {
            _dcfDatas.Add(fd);
        }

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
        public void SortDcfDatas()
        {
            //_finDatas.Sort((x, y) => DateTime.Compare(x.Kuupaev, y.Kuupaev)); // asc sort
            //_finDatas.Reverse(); // asc reverse
            //_finDatas.Sort((x, y) => y.Kuupaev.CompareTo(x.Kuupaev)); // desc sort
            _dcfDatas.Sort((x, y) => x.Kuupaev.CompareTo(y.Kuupaev)); // asc sort
        }
    }
}
