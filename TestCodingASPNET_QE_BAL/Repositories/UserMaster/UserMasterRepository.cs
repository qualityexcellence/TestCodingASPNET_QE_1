using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_BAL
{
    public class UserMasterRepository : TestCodingASPNETRepository<UserMaster>, IUserMasterRepository
    {
        public UserMasterRepository(DbContext context) : base(context) { }
        public List<UserMaster> Login(string EmailID, string UserPassword)
        {
            if (!string.IsNullOrEmpty(EmailID) && !string.IsNullOrEmpty(UserPassword))
                return Get(x => x.EmailID == EmailID && x.UserPassword == UserPassword, null, "").ToList();
            else
                return new List<UserMaster>();
        }
    }
}
