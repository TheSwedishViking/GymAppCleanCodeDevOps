using GymSwipe.ApplicationLayer.DTOs;
using GymSwipe.ApplicationLayer.Interfaces;
using GymSwipe.Domain.Models;
using GymSwipe.Infrastructure.Data;

namespace GymSwipe.Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {

        private readonly GymAppDbContext _db;
        public UserRepository(GymAppDbContext gymAppDbContext)
        {
            _db = gymAppDbContext;
        }

        public async Task AddUser(GymUserDTO dto)
        {


            GymUser user = new GymUser
            {
                Firstname = dto.Firstname,
                Surname = dto.Surname,
                Email = dto.Email,
                HeightCm = dto.HeightCm,
                WeightKg = dto.WeightKg,
                Gender = dto.Gender,
            };

            user.SetFriendCode();

            _db.Add(user);
            await _db.SaveChangesAsync();
        }

        public async Task<GymUser> GetUserById(int id)
        {
            GymUser foundUser = _db.Users.FirstOrDefault(u => u.Id == id);

            foundUser.SetFriendCode();


            return foundUser;


        }


        public async Task DeleteUserById(int userId)
        {

            GymUser foundUser = _db.Users.FirstOrDefault(u => u.Id == userId);
            if (foundUser != null)
            {
                _db.Users.Remove(foundUser);
                await _db.SaveChangesAsync();
            }

        }
    }
}
