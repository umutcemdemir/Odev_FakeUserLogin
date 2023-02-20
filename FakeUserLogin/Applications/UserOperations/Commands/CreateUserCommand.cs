using AutoMapper;
using FakeUserLogin.DbOperations;
using FakeUserLogin.Models;
using FakeUserLogin.ViewModels.UserViewModels;


namespace FakeUserLogin.Applications.UserOperations.Commands
{
    public class CreateUserCommand
    {
        public CreateUserViewModel Model { get; set; }

        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public void Handle()
        {
            User? user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user is not null)
                throw new InvalidOperationException("Kulanıcı Zaten Mevcut");


            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
