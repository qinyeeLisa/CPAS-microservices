﻿using UserWebApi.Data;
using UserWebApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UserWebApi.Models.ViewModel;
using UserWebApi.Models;

namespace UserWebApi.Services
{
    public class UserService
    {
        public User MapUserInfoDtoToEntity(UserInfoDto userInfo) 
        {
            return new User
            {
                UserId = userInfo.Id,
                Username = userInfo.Username,
                Name = userInfo.Name,
                Email = userInfo.Email,
                Password = userInfo.Password,
                Role = userInfo.Role
            };
        }

        public UserProfileViewModel MapEntityToUserProfileViewModel(User user)
        {
            return new UserProfileViewModel
            {
                Id = user.UserId,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedBy = user.CreatedBy,
                DateTimeCreated = user.DateTimeCreated,
                UpdatedBy = user.UpdatedBy,
                DateTimeUpdated = user.DateTimeUpdated
            };
        }
    }
}
