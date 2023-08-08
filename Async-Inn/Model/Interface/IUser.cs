﻿using Async_Inn.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Async_Inn.Model.Interface
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUser, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);
    }
}
