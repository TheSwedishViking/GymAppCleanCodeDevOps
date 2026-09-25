using GymSwipe.Application.DTOs;
using GymSwipe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Application.Services
{
    public class UserService : IUserService
    {
        public Task<string> GenerateFriendCode()
        {
            string friendCode = "";
            for (int i = 0; i < 4; i++)
            {
                for (int y = 0; y < 4; y++)
                {
                    friendCode += Random.Shared.Next(0, 9);
                }
                if (i != 3)
                {
                    friendCode += "-";
                }
            }
            return Task.FromResult(friendCode);
        }

        public Task<GymUserDTO> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<GymUserDTO> GetUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
