using AutoMapper;
using JsonPatch;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_BAL
{
    public class UserMasterService
    {
        private TestCodingASPNETQEUOW _uow = new TestCodingASPNETQEUOW();
        DomainProfile profile = new DomainProfile();
        private IMapper _mapper;

        public List<UserMaster> Login(string EmailID, string UserPassword)
        {
            try
            {
                if (!string.IsNullOrEmpty(EmailID) && !string.IsNullOrEmpty(UserPassword))
                    return _uow.UserMasters.Get(x => x.EmailID == EmailID && x.UserPassword == UserPassword, null, "").ToList();
                else
                    return new List<UserMaster>();
            }
            catch (Exception ex)
            {
                return new List<UserMaster>();
            }
        }
        public bool CheckDuplicateEmail(string EmailID)
        {
            try
            {
                return _uow.UserMasters.Exists(u => u.EmailID == EmailID);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int UserRegistration(RegisterVM registerVM)
        {
            try
            {
                _mapper = profile.Map().CreateMapper();
                UserMaster userMaster = _mapper.Map<RegisterVM, UserMaster>(registerVM);
                _uow.UserMasters.Add(userMaster);
                return _uow.CommitChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateUser(int UserId, JsonPatchDocument<UserMaster> userPath)
        {
            try
            {
                UserMaster user = _uow.UserMasters.GetById(UserId);
                if (user.UserMasterId > 0)
                {
                    userPath.ApplyUpdatesTo(user);
                    _uow.UserMasters.Update(user);
                    return _uow.CommitChanges();
                }
                else
                {
                    return -2;
                }                   
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public UserMaster GetUserById(int UserId)
        {
            try
            {
                UserMaster user = _uow.UserMasters.GetById(UserId);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return new UserMaster();
                }
            }
            catch (Exception)
            {
                return new UserMaster();
            }
        }
        public int DeleteUser(int UserId)
        {
            try
            {
                _uow.UserMasters.Delete(UserId);
                return _uow.CommitChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
