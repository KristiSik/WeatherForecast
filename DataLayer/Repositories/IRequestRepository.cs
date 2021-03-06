﻿using DataLayer.Models;
using System;
using System.Collections.Generic;

namespace DataLayer.Repositories
{
    public interface IRequestRepository : IRepository<Request>
    {
        IEnumerable<Request> GetAllRequests();
    }
}