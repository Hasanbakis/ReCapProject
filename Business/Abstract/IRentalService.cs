﻿using Core.Ultities;
using Core.Ultities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IDataResult<List<Rental>> GetAll();
        IDataResult<Rental> GetById(int rentalId);
        IResult Delete(Rental rental);
        IResult Add(Rental rental);
        IResult Update(Rental rental);
        IDataResult <List<RentalDetailDto>> GetAllRentalDetails();
       
    }
}
