using UserWebApi.Data;
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
                Name = userInfo.Name,
                Email = userInfo.Email,
                Password = userInfo.Password,
                Role = userInfo.Role,
                //temporary put user
                CreatedBy = "user",
                DateTimeCreated = DateTime.Now,
                //temporary put user
                UpdatedBy = "user",
                DateTimeUpdated = DateTime.Now,
            };
        }

        public UserProfileViewModel MapEntityToUserProfileViewModel(User user)
        {
            return new UserProfileViewModel
            {
                Id = user.UserId,
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
