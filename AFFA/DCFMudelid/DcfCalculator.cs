using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AFFA.Mudelid;

namespace AFFA.DCFMudelid
{
    public static class DcfCalculator
    {

        public static void GenerateDcfData(List<FinData> finDatas, DcfDataDao dcfDataDao)
        {
            // meetodile antakse findata list ette
            // nüüd tuleb võtta findatast viimase nt 5 aasta andmed ehk 20 kvartalit
            // ja nende põhjal genereerida olemasolevate kvartalite DcfData objektid

            for (int i = (finDatas.Count - 21); i < finDatas.Count; i++)
            {
                // siin täidame findata andmetega loodavad DcfData objektid
                dcfDataDao.AddDcfData(new DcfData(finDatas[i]));


            }

            // nüüd oleks vaja luua ka tulevikuprognooside DcfData objektid
            // loome need näiteks veel 5 aasta kohta ehk 20 kvartalit

            for (int i = 0; i < 20; i++)
            {
                // kõigepealt tuleks genereerida vastavad kuupäevad järgneva 20 kvartali lõpu jaoks
                // ja siis luua tühjad DcfData objektid
                // TODO
                // umbes nagu:
                dcfDataDao.AddDcfData(new DcfData(new DateTime())); // TODO

            }
        }

        public static void Calculate(List<DcfData> dcfDatas, DcfInput dcfInput)
        {


            for (int i = 0; i <dcfDatas.Count; i++)
            {
                // TODO
                // hakata välja arvutama DcfData välju, mis varem täidetud ei ole
                // olenevalt sellest, kas IsPrognosis on true või false, tuleb erinevalt arvutada
                // kui IsPrognosis=true, siis tuleb kasutada eelduste klassis DcfInput olevat infot, juba toimunud kvartalite puhul tavaliselt mitte 
                // võimalik, et on vaja vahemuutujaid vms

                dcfDatas[i].Capex = null; // TODO
                dcfDatas[i].NetWorkingCapitalChange = dcfDatas[i].NetWorkingCapital - dcfDatas[i-1].NetWorkingCapital; // TODO

            }
        }
    }
}
