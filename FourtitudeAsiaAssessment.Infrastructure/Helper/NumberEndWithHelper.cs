namespace FourtitudeAsiaAssessment.Infrastructure.Helper
{
    public class NumberEndWithHelper
    {
        public static bool NumberEndWithFive(long number)
        {
            long result = ConvertRinggitToCentHelper.ConvertCentToRinggit(number);

            return result % 10 == 5;
        }
    }
}
