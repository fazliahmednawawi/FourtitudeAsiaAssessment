using FourtitudeAsiaAssessment.Domain;

namespace FourtitudeAsiaAssessment.Application.IService
{
    public interface ITransactionService
    {
        bool Authorization(Transaction transaction);

        bool ValidTotalAmount(Transaction transaction);

        bool ExpiredRequest(string timestamp);

        long CalculateDiscountAmount(long totalAmount);
    }
}
