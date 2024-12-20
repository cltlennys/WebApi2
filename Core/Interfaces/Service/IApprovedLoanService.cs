﻿using Core.DTOs.ApprovedLoan;

namespace Core.Interfaces.Service
{
    public interface IApprovedLoanService
    {
        Task<LoanDetailsResponse> GetApprovedLoanDetails(int approvedLoanId);
    }
}
