using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    public class FinDataDao
    {
        private Collection<FinData> _finDatas;

        public Collection<FinData> FinDatas
        {
            get { return _finDatas; }
        }

        public FinDataDao()
        {
            _finDatas = new Collection<FinData>();
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
    }
}
