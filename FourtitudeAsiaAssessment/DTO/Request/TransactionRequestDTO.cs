using System.ComponentModel.DataAnnotations;

namespace FourtitudeAsiaAssessment.DTO.Request
{
    public class TransactionRequestDTO
    {
        [MaxLength(50)]
        [Required]
        public string PartnerKey { get; set; }

        [MaxLength(50)]
        [Required]
        public string PartnerRefNo { get; set; }

        [MaxLength(50)]
        [Required]
        public string PartnerPassword { get; set; }

        [Required]
        [Range(0, long.MaxValue)]
        public long? TotalAmount { get; set; }

        public List<ItemDetailDTO> Items { get; set; }

        [Required]
        public string Timestamp { get; set; }

        [Required]
        public string Sig { get; set; }
    }

    public class ItemDetailDTO
    {
        [MaxLength(50)]
        [Required]
        public string PartnerItemRef { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int? Qty { get; set; }

        [Required]
        public long? UnitPrice { get; set; }
    }
}
