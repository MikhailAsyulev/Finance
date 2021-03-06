﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using Common.Enum;
using DAL.Interfaces;

namespace DAL.Implementation
{
    public class UserRepository:IUserRepository
    {
        public List<UserProfile> GetUsers()
        {
            using (var context = new FinanceEntities())
            {
                return context.UserProfile.ToList();
            }
        }

        public void AddOrUpdateUser(UserProfile user)
        {
            try
            {
                using (var context = new FinanceEntities())
                {
                    if (user.UserId == 0)
                    {
                        context.UserProfile.Add(user);

                    }
                    else
                    {
                        var dbuser = context.UserProfile.Include(u => u.webpages_Roles).Single(u => u.UserId == user.UserId);
                        context.Entry(dbuser).CurrentValues.SetValues(user);
                        dbuser.webpages_Roles = new List<webpages_Roles>();
                        foreach (var role in user.webpages_Roles)
                        {
                            dbuser.webpages_Roles.Add(context.webpages_Roles.Single(r => r.RoleId == role.RoleId));
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException e)
            {
                var message = e.Message;
                throw;
            }
            
        }

        public UserProfile GetUserByLogin(String login)
        {
            using (var context = new FinanceEntities())
            {
                return context.UserProfile.FirstOrDefault(item => item.Login == login);
            }
        }

        public UserProfile GetUserByPassportNumber(String passportNumber)
        {
            using (var context = new FinanceEntities())
            {
                return context.UserProfile.FirstOrDefault(user => user.PassportIdentificationNumber == passportNumber);
            }
        }

        public UserProfile GetUserById(int userId)
        {
            using (var context = new FinanceEntities())
            {
                return context.UserProfile
                    .Include(user => user.webpages_Roles)
                    .FirstOrDefault(user => user.UserId == userId);
            }
        }

        public List<UserProfile> GetUsers(int page, string sortColumnName)
        {
            using (var context = new FinanceEntities())
            {
                return context.UserProfile
                    .Include(u => u.webpages_Roles)
                    .OrderBy(sortColumnName)
                    .Skip((page - 1) * 20).Take(20).ToList();
            }
        }

        public void RemoveUser(int userId)
        {
            using (var context = new FinanceEntities())
            {
                context.UserProfile.Remove(context.UserProfile.First(u => u.UserId == userId));
            }
        }
    }
}
