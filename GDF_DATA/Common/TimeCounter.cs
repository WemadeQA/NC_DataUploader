using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDF_DATA
{
    class TimeCounter
    {
        DateTime _preTime;

        public void RecordTime()
        {
            _preTime = DateTime.Now;
        }

        public string GetTimePasses()
        {
            long elapsedTricks = DateTime.Now.Ticks - _preTime.Ticks;

            TimeSpan elapsedSpan = new TimeSpan(elapsedTricks);
            

            return string.Format("total Time : {0:N2} seconds", elapsedSpan.TotalSeconds);
        }
    }
}
