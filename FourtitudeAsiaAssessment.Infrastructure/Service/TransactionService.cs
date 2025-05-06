using FourtitudeAsiaAssessment.Application.IService;
using FourtitudeAsiaAssessment.Domain;
using FourtitudeAsiaAssessment.Infrastructure.Helper;
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
            long calculatedTotal = transaction.Items.Sum(x => x.Qty.GetValueOrDefault() * x.UnitPrice.GetValueOrDefault());

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

            if (totalAmount < ConvertRinggitToCentHelper.ConvertRinggitToCent(200))
            {
                totalDiscount += 0;
            }
            else if (totalAmount < ConvertRinggitToCentHelper.ConvertRinggitToCent(501))
            {
                totalDiscount += 0.05m;
            }
            else if (totalAmount < ConvertRinggitToCentHelper.ConvertRinggitToCent(801))
            {
                totalDiscount += 0.07m;
            }
            else if (totalAmount < ConvertRinggitToCentHelper.ConvertRinggitToCent(1201))
            {
                totalDiscount += 0.1m;
            }
            else
            {
                totalDiscount += 0.15m;
            }

            if (totalAmount > ConvertRinggitToCentHelper.ConvertRinggitToCent(500) && PrimeNumberHelper.IsPrimeNumber(totalAmount))
            {
                totalDiscount += 0.08m;
            }

            if (totalAmount > ConvertRinggitToCentHelper.ConvertRinggitToCent(900) && NumberEndWithHelper.NumberEndWithFive(totalAmount))
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
    }
}
