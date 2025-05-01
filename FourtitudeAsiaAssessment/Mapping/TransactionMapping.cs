using AutoMapper;
using FourtitudeAsiaAssessment.Domain;
using FourtitudeAsiaAssessment.DTO.Request;

namespace FourtitudeAsiaAssessment.Mapping
{
    public class TransactionMapping : Profile
    {
        public TransactionMapping()
        {
            CreateMap<Transaction, TransactionRequestDTO>().ReverseMap();
            CreateMap<ItemDetail, ItemDetailDTO>().ReverseMap();
        }
    }
}
