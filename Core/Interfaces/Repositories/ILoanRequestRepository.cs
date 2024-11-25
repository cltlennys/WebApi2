﻿using Core.DTOs.LoanRequest;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ILoanRequestRepository
    {
        Task AddAsync(LoanRequest loanRequest);
        Task<LoanRequest> GetByIdAsync(int id);
        Task UpdateAsync(LoanRequest loanRequest);
        Task<LoanRequest> VerifyId(int loanId);
        Task<TermIR> VerifyMonths(int months);
        


    }
}
