using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using WemadeQA.Common;

namespace GDF_DATA
{
    class ColumnConverter
    {
        private static Dictionary<string, string> _dicColumnText = new Dictionary<string, string>();

        public static Dictionary<string, string> ReadColumnText()
        {
            string[] columnTexts = Properties.Resources.ColumnNameList.Split('\n');

            foreach (var col in columnTexts)
            {
                string[] splitstr = col.Split(',');

                if (!_dicColumnText.ContainsKey(splitstr[0]))
                    _dicColumnText.Add(splitstr[0], splitstr[1]);
            }

            return _dicColumnText;
        }
    }
}
