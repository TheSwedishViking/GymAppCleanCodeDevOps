using GymSwipe.Application.DTOs;
using GymSwipe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymSwipe.Infrastructure.Repos
{
    public class UserRepository : IUserRepository
    {
        public Task AddUser(GymUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<GymUserDTO> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
