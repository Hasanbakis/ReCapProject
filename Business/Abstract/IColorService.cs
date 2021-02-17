﻿using Core.Ultities;
using Core.Ultities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult <List<Color>> GetAll();
        IDataResult <Color> GetById(int colorId);
        IResult Delete(Color color);
        IResult Add(Color color);

    }
}
