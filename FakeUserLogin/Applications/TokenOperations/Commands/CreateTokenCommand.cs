using AutoMapper;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.UserViewModels;

namespace FakeUserLogin.Applications.TokenOperations.Commands
{
    public class CreateTokenCommand
    {
        public CreateTokenViewModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(BookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }


        public Token Handle()
        {
            User? user = _dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

            if (user is null)
                throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı");


            TokenHandler handler = new(_configuration);
            Token token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpirationDate = token.Expiration.AddMinutes(5);


            _dbContext.SaveChanges();

            return token;
        }
    }
}
