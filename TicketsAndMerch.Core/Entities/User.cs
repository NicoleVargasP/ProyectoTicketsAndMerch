using System;
using System.Collections.Generic;

namespace TicketsAndMerch.Core.Entities
{
    /// <summary>
    /// Representa un usuario dentro del sistema Tickets & Merch.
    /// </summary>
    /// <remarks>
    /// Esta entidad almacena la información principal de los usuarios registrados, 
    /// incluyendo sus credenciales, rol y las órdenes asociadas a sus compras de tickets o merchandising.
    /// </remarks>
    public partial class User : BaseEntity
    {
        /// <summary>
        /// Nombre de usuario utilizado para iniciar sesión o identificarse en el sistema.
        /// </summary>
        /// <example>nicolav</example>
        public string UserName { get; set; } = null!;

        /// <summary>
        /// Dirección de correo electrónico del usuario.
        /// </summary>
        /// <example>nicole.valdez@ucb.edu.bo</example>
        public string Email { get; set; } = null!;

        /// <summary>
        /// Contraseña en formato encriptado o hash.
        /// </summary>
        /// <example>$2a$10$EixZaYVK1fsbw1ZfbX3OXePaWxn96p36h1yKxF1VjR8tTf4rW9IuC</example>
        public string Contrasenia { get; set; } = null!;

        /// <summary>
        /// Rol asignado al usuario dentro del sistema.
        /// </summary>
        /// <example>Admin</example>
        public string Rol { get; set; } = null!;

        /// <summary>
        /// Colección de órdenes asociadas al usuario.
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
