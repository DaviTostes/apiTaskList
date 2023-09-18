using AutoMapper;
using Data.UnitOfWork;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace apiTaskList.Services
{
    public class UnitOfService
    {
        public TarefaService TarefaServ { get; set; }
        public UsuarioService UsuarioServ { get; set; }

        public UnitOfWork context { get; set; }
        public IMapper mapper { get; set; }
        private UserManager<Usuario> userManager { get; set; }
        private SignInManager<Usuario> signInManager { get; set; }
        private IConfiguration configuration { get; set; }

        public UnitOfService(UnitOfWork context, IMapper mapper, UserManager<Usuario> userManager, 
            SignInManager<Usuario> signInManager, IConfiguration configuration)
        {
            this.context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }   

        public TarefaService TarefaService
        {
            get { return TarefaServ ??= new TarefaService(mapper, context); }
        }

        public UsuarioService UsuarioService
        {
            get { return UsuarioServ ??= new UsuarioService(userManager, signInManager, mapper, configuration); }
        }
    }
}
