﻿using Core.DTOs.LoanRequest;

namespace Core.Interfaces.Service
{
    public interface ILoanRequestService 
    { 
        Task<string> CreateLoanRequest(CreateLoanRequest createLoanRequest);
        Task<string> RejectedLoan(int loanId, string reason);
        Task<string> AproveLoan(int loanId);
    }
}
