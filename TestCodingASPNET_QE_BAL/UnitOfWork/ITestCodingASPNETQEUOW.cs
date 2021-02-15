namespace TestCodingASPNET_QE_BAL
{
    public interface ITestCodingASPNETQEUOW
    {
        int CommitChanges();
        // Repositories
        IUserMasterRepository UserMasters { get; }
        ICarMasterRepository CarMasters { get; }
    }
}