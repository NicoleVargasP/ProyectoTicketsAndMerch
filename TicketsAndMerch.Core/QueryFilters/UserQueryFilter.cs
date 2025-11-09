using Swashbuckle.AspNetCore.Annotations;

namespace TicketsAndMerch.Core.QueryFilters
{
    /// <summary>
    /// Filtros para usuarios
    /// </summary>
    public class UserQueryFilter : PaginationQueryFilter
    {
        [SwaggerSchema("Nombre de usuario")]
        public string? UserName { get; set; }

        [SwaggerSchema("Correo electrónico del usuario")]
        public string? Email { get; set; }

        [SwaggerSchema("Rol del usuario (Cliente, Administrador, etc.)")]
        public string? Rol { get; set; }
    }
}
