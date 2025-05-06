using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourtitudeAsiaAssessment.Infrastructure.Helper
{
    public class ConvertRinggitToCentHelper
    {
        public static long ConvertRinggitToCent(decimal ringgit)
        {
            return (long)(ringgit * 100);
        }
    }
}
