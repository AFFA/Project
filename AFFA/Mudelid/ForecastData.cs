using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AFFA.Mudelid
{
    /// <summary>
    /// Forecast vaate tabeli jaoks vajalike andmete hoidja. Andmed hoitakse lisatakse tabelisse stringina. 
    /// </summary>
    public class ForecastData
    {
        private IList<String> _values;


        public ForecastData()
        {
            _values = new List<string>();
        }

        public void AddData(string s)
        {
            _values.Add(s);
        }


        public IList<string> Values
        {
            get { return _values; }
            set { _values = value; }
        }

        public int GetSize()
        {
            return _values.Count;
        }
    }
}
