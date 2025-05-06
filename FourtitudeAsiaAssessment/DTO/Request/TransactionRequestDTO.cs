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
        [Range(1, long.MaxValue, ErrorMessage = "TotalAmount value need to be positive value (greater than 0) only.")]
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
        [Range(1, 5, ErrorMessage = "Qty must be between 1 and 5.")]
        public int? Qty { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "UnitPrice value need to be positive value (greater than 0) only.")]
        public long? UnitPrice { get; set; }
    }
}
