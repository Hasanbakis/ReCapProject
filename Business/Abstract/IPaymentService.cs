﻿using Core.Ultities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult AddPayTheAmount(Payment payment);
    }
}
