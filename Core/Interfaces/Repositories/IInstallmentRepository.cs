﻿using Core.DTOs.Installments;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IInstallmentRepository
    {
        Task CreateInstallment(SimulateInstallment simulateInstallment);
        Task<TermIR> VerifyMonths(int months);
        Task AddAsync(Installment installment);
        Task<List<Installment>> GetInstallments(int loanId);
    }
}
