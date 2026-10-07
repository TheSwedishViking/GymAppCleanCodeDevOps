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

        public async Task<GymUser> AddUser(GymUserDTO dto)
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
            return user;
        }

        public async Task<GymUserDTO> GetUserById(int id)
        {
            GymUser foundUser = _db.Users.FirstOrDefault(u => u.Id == id);
            Console.WriteLine();
            foundUser.SetFriendCode();

            return new GymUserDTO
            {
                Id = foundUser.Id,
                Firstname = foundUser.Firstname,
                Surname = foundUser.Surname,
                Email = foundUser.Email,
                HeightCm = foundUser.HeightCm,
                WeightKg = foundUser.WeightKg,
                Gender = foundUser.Gender,
                FriendCode = foundUser.FriendCode
            };
        }

        public async Task<bool> GetUserByEmail(string email) // bad naming But a hazzle to change
        {
            GymUser foundUser = _db.Users.FirstOrDefault(u => u.Email == email);
            Console.WriteLine();

            if (foundUser == null)
            {
                return true;
            }

            return false;
        }


        public async Task<GymUserDTO> GetCreatedUserByEmail(string email)
        {
            GymUser foundUser = _db.Users.FirstOrDefault(u => u.Email == email);
            Console.WriteLine();
            foundUser.SetFriendCode();

            return new GymUserDTO
            {
                Id = foundUser.Id,
                Firstname = foundUser.Firstname,
                Surname = foundUser.Surname,
                Email = foundUser.Email,
                HeightCm = foundUser.HeightCm,
                WeightKg = foundUser.WeightKg,
                Gender = foundUser.Gender,
                FriendCode = foundUser.FriendCode
            };
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
