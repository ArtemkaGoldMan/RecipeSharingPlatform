using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegistrationDTO registrationDTO);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO);
    }
}
