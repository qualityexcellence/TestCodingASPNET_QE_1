using AutoMapper;
using JsonPatch;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TestCodingASPNET_QE_DAL;

namespace TestCodingASPNET_QE_BAL
{
    public class CarMasterService
    {
        private TestCodingASPNETQEUOW _uow = new TestCodingASPNETQEUOW();
        DomainProfile profile = new DomainProfile();
        private IMapper _mapper;
        public bool CheckDuplicateCarName(string CarName)
        {
            try
            {
                return _uow.CarMasters.Exists(c => c.Name == CarName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public int CarRegistration(CarVM carVM)
        {
            try
            {
                _mapper = profile.Map().CreateMapper();
                CarMaster carMaster = _mapper.Map<CarVM, CarMaster>(carVM);
                _uow.CarMasters.Add(carMaster);
                return _uow.CommitChanges();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateCar(int CarId, JsonPatchDocument<CarMaster> CarPath)
        {
            try
            {
                CarMaster Car = _uow.CarMasters.GetById(CarId);
                if (Car.CarMasterId > 0)
                {
                    CarPath.ApplyUpdatesTo(Car);
                    _uow.CarMasters.Update(Car);
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
        public List<CarMaster> GetCarByYearMade(int YearMade)
        {
            try
            {
                List<CarMaster> cars = _uow.CarMasters.Get(c => c.YearMade == YearMade).ToList();
                if (cars != null)
                {
                    return cars;
                }
                else
                {
                    return new List<CarMaster>();
                }
            }
            catch (Exception)
            {
                return new List<CarMaster>();
            }
        }
        public CarMaster GetCarById(int CarId)
        {
            try
            {
                CarMaster Car = _uow.CarMasters.GetById(CarId);
                if (Car != null)
                {
                    return Car;
                }
                else
                {
                    return new CarMaster();
                }
            }
            catch (Exception)
            {
                return new CarMaster();
            }
        }
        public int DeleteCar(int CarId)
        {
            try
            {
                _uow.CarMasters.Delete(CarId);
                return _uow.CommitChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
