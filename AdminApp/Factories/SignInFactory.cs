using AdminApp.ViewModels;
using Infrastructure.Dtos;

namespace AdminApp.Factories
{
    public class SignInFactory
    {
        public static SignInDto CreateSignInDto(SignInViewModel viewModel)
        {
            return new SignInDto
            {
                Email = viewModel.Email,
                Password = viewModel.Password,
                RememberMe = viewModel.RememberMe
            };
        }
    }
}
