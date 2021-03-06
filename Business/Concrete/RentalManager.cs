﻿using Business.Abstract;
using Business.Constantss;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Ultities;
using Core.Ultities.Results;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly ICarService _carService;
        private readonly ICustomerService _customerService;

        public RentalManager(IRentalDal rentalDal,ICarService carService,ICustomerService customerService)
        {
            _rentalDal = rentalDal;
            _carService = carService;
            _customerService = customerService;
        }

      //  [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(FindexScoreCheck(rental.CustomerId, rental.CarId));

            if (result != null)
                return result;

            _rentalDal.Add(rental);

            return new SuccessResult(Messages.RentaCar);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }


        public IDataResult<List<RentalDetailDto>> GetAllRentalDetails()
        {
                List<RentalDetailDto> rentalDetailDtos = _rentalDal.GetAllRentalDetails();
                if (rentalDetailDtos.Count > 0)
                    return new SuccessDataResult<List<RentalDetailDto>>(rentalDetailDtos, Messages.GetSucccesRentalMessage);
                else
                    return new ErrorDataResult<List<RentalDetailDto>>(Messages.GetErrorRentalMesssage);
            
        }

       

     
       
        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == rentalId));
        }

     

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private IResult FindexScoreCheck(int customerId, int carId)
        {
            var customerFindexPoint = _customerService.GetById(customerId).Data.FindexPoint;

            if (customerFindexPoint == 0)
            {
                return new ErrorResult(Messages.CustomerFindexZero);
            }
               

            var carFindexPoint = _carService.GetById(carId).Data.FindexPoint;

            if (customerFindexPoint < carFindexPoint)
            {
                return new ErrorResult(Messages.CustomerScoreIsInsufficient);
            }
              

            return new SuccessResult();
        }


    }
}
