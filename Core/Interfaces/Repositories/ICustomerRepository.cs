﻿

using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetById (int id);
}
