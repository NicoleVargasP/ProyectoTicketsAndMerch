using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.CustomEntities
{
    /// <summary>
    /// Representa una transacción de compra de tickets.
    /// </summary>
    public class BuyTicket : BaseEntity
    {
        /// <summary>
        /// Id del usuario que realiza la compra.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id del concierto al que pertenecen los tickets.
        /// </summary>
        public int ConcertId { get; set; }

        /// <summary>
        /// Cantidad de tickets comprados.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Monto total de la compra.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Fecha de la compra.
        /// </summary>
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Estado de la compra o del pago (Pendiente, Pagado, etc.)
        /// </summary>
        public string PaymentState { get; set; } = "Pendiente";
        public string PaymentMethod { get; set; } = null!;

    }
}
