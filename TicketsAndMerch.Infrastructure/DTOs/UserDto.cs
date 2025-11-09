namespace TicketsAndMerch.Infrastructure.DTOs
{
    public partial class UserDto
    {
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Contrasenia { get; set; } = null!;

        public string Rol { get; set; } = null!;

    }
}