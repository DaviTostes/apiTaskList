using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiTaskList.Services
{
    public class UsuarioService
    {
        private readonly UserManager<Usuario> userManager;
        private readonly SignInManager<Usuario> signInManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public UsuarioService(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IMapper mapper, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<Result> Login(LoginUsuarioDTO login)
        {
            Result result;
            try
            {
                var resultSignIn = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

                if (!resultSignIn.Succeeded)
                {
                    result = new Result().WithError(new Error("Senha ou Email incorreto"));

                    return result;
                }

                var token = GeraToken(login);

                result = new Result().WithSuccess(new Success(token));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(new Error(ex.Message));

                return result;
            }
        }

        public async Task<Result> Register(RegisterUsuarioDTO register)
        {
            Result result;
            try
            {
                var user = mapper.Map<Usuario>(register);

                var resultRegister = await userManager.CreateAsync(user, register.Password);

                if (!resultRegister.Succeeded)
                {
                    result = new Result().WithError(new Error(resultRegister.Errors.FirstOrDefault().Description));

                    return result;
                }

                result = new Result().WithSuccess(new Success("Sucesso"));

                return result;
            }
            catch (Exception ex)
            {
                result = new Result().WithError(new Error(ex.Message));

                return result;
            }
        }

        private string GeraToken(LoginUsuarioDTO userInfo)
        {
            //define declarações do usuário
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //gera uma chave com base em um algoritmo simetrico
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
            //gera a assinatura digital do token usando o algoritmo Hmac e a chave privada
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Tempo de expiracão do token.
            var expiracao = configuration["Jwt:Expiration"];
            var expiration = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            // classe que representa um token JWT e gera o token
            var token = new JwtSecurityToken(
              issuer: configuration["Jwt:Issuer"],
              audience: configuration["Jwt:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            //retorna os dados com o token e informacoes
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
