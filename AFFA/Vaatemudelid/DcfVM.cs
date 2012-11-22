using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.DCFMudelid;
using AFFA.Mudelid;

namespace AFFA.Vaatemudelid
{
    public class DcfVM
    {
        private DcfDataDao _dcfDataDao;
        private FinDataDao _finDataDao;
        // TODO mingi observable collection, mis binditakse XAMLi


        public DcfDataDao DcfDataDao
        {
            get { return _dcfDataDao; }
            set { _dcfDataDao = value; }
        }

        public DcfVM(FinDataDao finDataDao)
        {
            _finDataDao = finDataDao; // anname konstruktoriga findata andmed
            _dcfDataDao=new DcfDataDao(); 

        }

        public void GetDcf()
        {
            // anname kalkulaatorile findata listi ja dcfdatadao, et calculaator paneks sinna genereeritavad dcf andmed
            DcfCalculator.GenerateDcfData(_finDataDao.FinDatas, _dcfDataDao);
            DcfCalculator.Calculate(_dcfDataDao.DcfDatas, new DcfInput());
            
        }

        private void GenerateTableData(List<FinData> finDatas)
        {
            // TODO tekitada FinAnalysisVM näitel sobival kujul observable collection
            // selles osas on lihtsam, et erinevalt FinAnalysisVM-st ei pea siin veergude muutude % eraldi arvutama
        }
    }
}
