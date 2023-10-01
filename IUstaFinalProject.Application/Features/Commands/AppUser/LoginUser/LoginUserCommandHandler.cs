using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IUstaFinalProject.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> userManager;
        readonly SignInManager<Domain.Entities.Identity.AppUser> signInManager;

        public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Identity.AppUser appUser =  await userManager.FindByNameAsync(request.UsernameOrEmail);
            if(appUser == null)
                appUser = await userManager.FindByEmailAsync(request.UsernameOrEmail);

            if (appUser == null)
                throw new Exception("Username or password is incorrect");
           SignInResult result =  await signInManager.CheckPasswordSignInAsync(appUser,request.Password,false);
            if (result.Succeeded)
            {
                // ....
            }

            return new();
        }
    }
}
