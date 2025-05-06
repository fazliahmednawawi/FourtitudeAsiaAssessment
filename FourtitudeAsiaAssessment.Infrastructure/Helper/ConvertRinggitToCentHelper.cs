namespace FourtitudeAsiaAssessment.Infrastructure.Helper
{
    public class ConvertRinggitToCentHelper
    {
        public static long ConvertRinggitToCent(decimal number)
        {
            return (long)(number * 100);
        }

        public static long ConvertCentToRinggit(decimal number)
        {
            return (long)(number / 100);
        }
    }
}
