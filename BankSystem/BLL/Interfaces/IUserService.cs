﻿using System.Collections.Generic;
using BLL.Models.ViewModel;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        List<UserViewModel> GetUserViewModels();

        void AddClientUser(UserViewModel userModel);
    }
}