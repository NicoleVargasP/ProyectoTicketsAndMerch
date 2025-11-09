namespace TicketsAndMerch.Infrastructure.DTOs
{
    public class MerchDto
    {
        public int Id { get; set; }

        public string MerchName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? TypeMerch { get; set; }

        public int Stock { get; set; }

    }
} 
