﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystem_EL.ResultModels
{
    public interface IDataResult<T> : IResult
    {
        public T Data { get; set; }
    }
}
