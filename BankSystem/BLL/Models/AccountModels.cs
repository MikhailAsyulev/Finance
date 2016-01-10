﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using DAL;

namespace BankSystem.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [StringLength(100, ErrorMessage = "Пароль {0} должен быть не меньше {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Фамилия пользователя")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Отчество пользователя")]
        public string ThirdName { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime BirthDateTime { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Мобильный телефон")]
        [RegularExpression("\\+375(\\d){2}-(\\d){3}-(\\d){2}-(\\d){2}", ErrorMessage = "Неверный формат телефона")]
        public String MobilePhone { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Емейл адресс")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Номер паспорта")]
        [RegularExpression("(ab|AB|BM|bm|HB|hb|KH|kh|MP|mp|MC|mc|KB|kb|pp|PP)(\\d){7}", ErrorMessage = "Недествительный номер паспорта")]
        public String PasportNumber { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Идентификационный номер")]
        [StringLength(14,ErrorMessage = "Идентификационный номер содержит 14 символов")]
        public String PassportIdentificationNumber { get; set; }
        
        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Кем выдан")]
        public String PassportApprovel { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [DataType(DataType.Date)]
        [Display(Name = "Выдан до")]       
        public DateTime PassportEndDate { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Адресс регистрации")] 
        public String RegistrationAddress { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [Display(Name = "Ceкретная фраза")]
        public String SecretPhrase { get; set; }

        [Required(ErrorMessage = "Поле {0} нужно запомнить")]
        [StringLength(100, ErrorMessage = "Пароль {0} должен быть не меньше {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }


    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
