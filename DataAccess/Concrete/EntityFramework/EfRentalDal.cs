﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, SouthwindContext>, IRentalDal
    {
        public List<RentalDetailDto> GetAllRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (SouthwindContext context = new SouthwindContext())
            {
                var result =
                    from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                    join customer in context.Customers
                        on rental.CustomerId equals customer.CustomerId
                    join user in context.Users
                         on customer.UserId equals user.Id
                    join car in context.Cars
                         on rental.CarId equals car.CarId
                    join brand in context.Brands
                         on car.BrandId equals brand.BrandId
                    join color in context.Colors
                         on car.ColorId equals color.ColorId
                    select new RentalDetailDto
                    {
                        RentDate =(DateTime) rental.RentDate,
                        ReturnDate = rental.ReturnDate,
                        RentalId = rental.RentalId,
                        BrandName = brand.BrandName,
                        Description = car.Description,
                        ColorName = color.ColorName,
                        CompanyName = customer.CompanyName,
                        DailyPrice = car.DailyPrice,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ModelYear = car.ModelYear
                    };

                return result.ToList();
            }
        }
    }

       
        
    
}
