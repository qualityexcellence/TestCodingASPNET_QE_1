using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_BAL
{
    public class CarMasterRepository : TestCodingASPNETRepository<CarMaster>, ICarMasterRepository
    {
        public CarMasterRepository(DbContext context) : base(context) { }
    }
}
