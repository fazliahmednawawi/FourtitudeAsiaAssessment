namespace FourtitudeAsiaAssessment.Infrastructure.Helper
{
    public class NumberEndWithHelper
    {
        public static bool NumberEndWithFive(long number)
        {
            return number % 10 == 5;
        }
    }
}
