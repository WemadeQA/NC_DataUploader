using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExcelDataReader;
using System.Data;
using WemadeQA.Common;

namespace GDF_DATA
{
    public enum MODIFY
    {
        NONE = 0,
        ADDED = 1,
        MODIFY = 2
    }
    static class Program
    {

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MIR4_GDF_UPLOADER());
            return;
        }
    }
}
