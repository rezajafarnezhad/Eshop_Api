﻿using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.UserAgg.Enums;
using Shop.Domain.UserAgg.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shop.Domain.UserAgg.Events;

namespace Shop.Domain.UserAgg
{
    public class User : AggregateRoot
    {
        private User()
        {

        }
        public User(string name, string family, string phoneNumber, string email,
            string password, Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);

            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Password = password;
            Gender = gender;
            AvatarName = "avatar.png";
            IsActive = true;
            Roles = new();
            Wallets = new();
            Addresses = new();
            UserTokens = new();
        }

        public string Name { get; private set; }
        public string Family { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string AvatarName { get; set; }
        public bool IsActive { get; set; }
        public Gender Gender { get; private set; }
        public List<UserRole> Roles { get; private set; }
        public List<Wallet> Wallets { get; private set; }
        public List<UserAddress> Addresses { get; private set; }
        public List<UserToken> UserTokens { get; private set; }

        public void Edit(string name, string family, string phoneNumber, string email,
            Gender gender, IUserDomainService userDomainService)
        {
            Guard(phoneNumber, email, userDomainService);
            Name = name;
            Family = family;
            PhoneNumber = phoneNumber;
            Email = email;
            Gender = gender;
        }

        public static User RegisterUser(string phoneNumber, string password, IUserDomainService userDomainService)
        {
            var user = new User("", "", phoneNumber, null, password, Gender.None, userDomainService);
            user.AddDomainEvent(new UserRegistered(user.Id, phoneNumber));
            return user;
        }

        public void ChangePassword(string pass)
        {
            Password = pass;
        }

        public void SetAvatar(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                imageName = "avatar.png";

            AvatarName = imageName;
        }
        public void AddAddress(UserAddress address)
        {
            address.UserId = Id;
            Addresses.Add(address);
        }

        public void DeleteAddress(long addressId)
        {
            var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
            if (oldAddress == null)
                throw new NullOrEmptyDomainDataException("Address Not found");

            Addresses.Remove(oldAddress);
        }

        public void EditAddress(UserAddress address, long addressId)
        {
            var oldAddress = Addresses.FirstOrDefault(f => f.Id == addressId);
            if (oldAddress == null)
                throw new NullOrEmptyDomainDataException("Address Not found");


            oldAddress.Edit(address.Shire, address.City, address.PostalCode, address.PostalAddress, address.PhoneNumber,
                address.Name, address.Family, address.NationalCode);
        }

        public void SetAddressActive(long addressId)
        {
            Addresses.ForEach(c =>
            {
                c.ActiveAddress = false;
            });
            var address = Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                throw new NullOrEmptyDomainDataException("Address Not found");
            address.SetActive();
        }


        public void ChargeWallet(Wallet wallet)
        {
            wallet.UserId = Id;
            Wallets.Add(wallet);
        }

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(f => f.UserId = Id);
            Roles.Clear();
            Roles.AddRange(roles);
        }


        public void AddToken(string hashJwtToken, string hashJwtRefreshToken, DateTime expireDateToken, DateTime expireDateRefreshToken, string device)
        {
            var activeTokensCount = UserTokens.Count(c => c.ExpireDateRefreshToken > DateTime.Now);
            if (activeTokensCount == 3)
                throw new InvalidDomainDataException("امکان استفاده از 4 دستگاه وجود ندارد");

            var token = new UserToken(hashJwtToken, hashJwtRefreshToken, expireDateToken, expireDateRefreshToken, device);
            token.UserId = Id;
            UserTokens.Add(token);
        }

        public void RemoveToken(long tokenId)
        {
            var token = UserTokens.FirstOrDefault(c => c.Id == tokenId);

            if (token == null)
            { throw new InvalidDomainDataException("Token not Found"); }

            UserTokens.Remove(token);
        }

        public void Guard(string phoneNumber, string email, IUserDomainService userDomainService)
        {
            NullOrEmptyDomainDataException.CheckString(phoneNumber, nameof(phoneNumber));


            if (phoneNumber.Length != 11)
                throw new InvalidDomainDataException("شماره موبایل نامعتبر است");

            if (!string.IsNullOrWhiteSpace(email))
                if (email.IsValidEmail() == false)
                    throw new InvalidDomainDataException("ایمیل  نامعتبر است");

            if (phoneNumber != PhoneNumber)
                if (userDomainService.PhoneNumberIsExist(phoneNumber))
                    throw new InvalidDomainDataException("شماره موبایل تکراری است");

            if (email != Email)
                if (userDomainService.IsEmailExist(email))
                    throw new InvalidDomainDataException("ایمیل تکراری است");
        }
    }
}