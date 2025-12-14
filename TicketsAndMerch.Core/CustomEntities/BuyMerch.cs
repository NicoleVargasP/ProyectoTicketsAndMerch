using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.CustomEntities
{
    /// <summary>
    /// Representa una transacción de compra de merchandising.
    /// </summary>
    public class BuyMerch : BaseEntity
    {
        public int UserId { get; set; }
        public int MerchId { get; set; } // FK a Merch.Id
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
        public string PaymentState { get; set; } = "Pendiente";
        public string PaymentMethod { get; set; } = null!;
    }
}
