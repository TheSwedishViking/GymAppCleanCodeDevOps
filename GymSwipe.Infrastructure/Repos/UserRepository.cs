using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;

namespace GymSwipe.Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {


        private readonly GymAppDbContext _db;

        public UserRepository(GymAppDbContext db)
        {
            _db = db;
        }

        public async Task AddUser(GymUser model)
        {
            _db.Add(model);

            await _db.SaveChangesAsync();
        }





        public Task<GymUser> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
