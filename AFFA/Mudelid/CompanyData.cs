using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFFA.Mudelid
{
    public class CompanyData
    {
        private string _dataFieldDescription;
        private string _dataFieldContent;

        public CompanyData()
        {
        }

        public CompanyData(string dataFieldDescription, string dataFieldContent)
        {
            this._dataFieldContent = dataFieldContent;
            this._dataFieldDescription = dataFieldDescription;
        }

        public string DataFieldDescription
        {
            get { return this._dataFieldDescription; }
            set { this._dataFieldDescription = value; }
        }

        public string DataFieldContent
        {
            get { return this._dataFieldContent; }
            set { this._dataFieldContent = value; }
        }
    }
}
