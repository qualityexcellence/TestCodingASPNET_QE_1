using System;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_BAL
{
    public class TestCodingASPNETQEUOW : ITestCodingASPNETQEUOW, IDisposable
    {
        private TechnicalCodingTestEntities TechnicalCodingTestEntities { get; set; }
        public TestCodingASPNETQEUOW()
        {
            TechnicalCodingTestEntities = new TechnicalCodingTestEntities();
        }

        private IUserMasterRepository _UserMasters;
        public IUserMasterRepository UserMasters
        {
            get
            {
                if (_UserMasters == null)
                    _UserMasters = new UserMasterRepository(TechnicalCodingTestEntities);
                return _UserMasters;
            }
        }

        private ICarMasterRepository _CarMasters;
        public ICarMasterRepository CarMasters
        {
            get
            {
                if (_CarMasters == null)
                    _CarMasters = new CarMasterRepository(TechnicalCodingTestEntities);
                return _CarMasters;
            }
        }

        public int CommitChanges()
        {
           int result = TechnicalCodingTestEntities.SaveChanges();
           return result;
        }
        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (TechnicalCodingTestEntities != null)
                {
                    TechnicalCodingTestEntities.Dispose();
                }
            }
        }

        #endregion
    }
}