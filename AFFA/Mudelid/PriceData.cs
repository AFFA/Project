using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace AFFA.Mudelid
{
    /// <summary>
    /// CSV formaadis loetud hinnaandmete objekt
    /// </summary>
    [DelimitedRecord(","), IgnoreFirst(1)]
    public class PriceData
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime PriceDate;
        public decimal OpenPrice;
        public decimal HighPrice;
        public decimal LowPrice;
        public decimal ClosePrice;
        public int Volume;
        public decimal AdjClose;

    }
}
