using FourtitudeAsiaAssessment.Application.IService;
using FourtitudeAsiaAssessment.Domain;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FourtitudeAsiaAssessment.Infrastructure.Service
{
    public class TransactionService : ITransactionService
    {
        public bool Authorization(Transaction transaction)
        {
            if (transaction == null) return false;

            try
            {
                var partnerDetails = new List<(string RefNo, string Key, string Password)>
                {
                    ("FG-00001", "FAKEGOOGLE", "FAKEPASSWORD1234"),
                    ("FG-00002", "FAKEPEOPLE", "FAKEPASSWORD4578")
                };

                string decodedPassword = Encoding.UTF8.GetString(Convert.FromBase64String(transaction.PartnerPassword));
                bool isExist = partnerDetails.Any(x => x.RefNo == transaction.PartnerRefNo && x.Key == transaction.PartnerKey && x.Password == decodedPassword);

                return isExist ? true : false;
            }
            catch
            {
                return false;
            }
            ;
        }

        public bool ValidTotalAmount(Transaction transaction)
        {
            long calculatedTotal = transaction.Items.Sum(item => item.Qty.GetValueOrDefault() * item.UnitPrice.GetValueOrDefault());

            return transaction.TotalAmount == calculatedTotal;
        }

        public bool ExpiredRequest(string timestamp)
        {
            try
            {
                DateTime requestTimestamp = DateTime.Parse(timestamp, null, System.Globalization.DateTimeStyles.AssumeUniversal);
                DateTime serverTime = DateTime.Now;
                DateTime validStartTime = serverTime.AddMinutes(-5);
                DateTime validEndTime = serverTime.AddMinutes(5);

                return requestTimestamp < validStartTime || requestTimestamp > validEndTime;
            }
            catch (Exception)
            {
                return true;
            }
        }

        public long CalculateDiscountAmount(long totalAmount)
        {
            decimal totalDiscount = 0;

            if (totalAmount < 200)
            {
                totalDiscount += 0;
            }
            else if (totalAmount <= 500)
            {
                totalDiscount += 0.05m;
            }
            else if (totalAmount <= 800)
            {
                totalDiscount += 0.07m;
            }
            else if (totalAmount <= 1200)
            {
                totalDiscount += 0.1m;
            }
            else
            {
                totalDiscount += 0.15m;
            }

            if (totalAmount > 500 && IsPrime(totalAmount))
            {
                totalDiscount += 0.08m;
            }

            if (totalAmount > 900 && Math.Abs(totalAmount).ToString().Length >= 5)
            {
                totalDiscount += 0.1m;
            }

            if (totalDiscount > 0.2m)
            {
                totalDiscount = 0.2m;
            }

            decimal discountAmount = totalAmount * totalDiscount;

            return (long)discountAmount;
        }

        public static bool IsPrime(long number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            int boundary = (int)Math.Sqrt(number);

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }
}
