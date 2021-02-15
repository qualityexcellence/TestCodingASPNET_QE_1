using TestCodingASPNET_QE_DAL;
using System.Collections.Generic;

namespace TestCodingASPNET_QE_BAL
{
    public interface IUserMasterRepository : ITestCodingASPNETRepository<UserMaster>
    {
        List<UserMaster> Login(string EmailID, string UserPassword);
    }
}