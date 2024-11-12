using Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAppAuthenticationService
    {
        Task<string?> SignInAsync(SignInDto viewModel);
        Task SignInUserWithTokenAsync(string token, bool rememberMe);
    }
}
