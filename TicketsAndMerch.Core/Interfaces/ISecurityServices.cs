using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsAndMerch.Core.Entities;

namespace TicketsAndMerch.Core.Interfaces
{
    public interface ISecurityServices
    {
        Task<Security> GetLoginByCredentials(UserLogin login);

        Task RegisterUser(Security security);
    }
}
