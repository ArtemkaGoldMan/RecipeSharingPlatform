using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary.DTOs;
using BaseLibrary.Entities;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegistrationDTO dto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
    }
}
