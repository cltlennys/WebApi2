﻿using Core.DTOs.ApprovedLoan;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using Mapster;

namespace Infrastructure.Service
{
    public class ApprovedLoanService : IApprovedLoanService
    {
        private readonly IApprovedLoanRepository _approvedLoanRepository;
        private readonly IInstallmentRepository _installmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IGeneralService _generalService;
        public ApprovedLoanService (IApprovedLoanRepository approvedLoanRepository, 
                IInstallmentRepository installmentRepository,
                ICustomerRepository customerRepository,
                IGeneralService generalService)
        {
            _approvedLoanRepository = approvedLoanRepository;
            _installmentRepository = installmentRepository;
            _customerRepository = customerRepository;
            _generalService = generalService;
        }
        public async Task<LoanDetailsResponse> GetApprovedLoanDetails(int approvedLoanId)
        {
            var approved = await _approvedLoanRepository.GetLoanById(approvedLoanId);
            var installments = await _installmentRepository.GetInstallmentsByApprovedLoanId(approvedLoanId);
            var customer = await _customerRepository.GetCustomerById(approved.CustomerId);

            var paidInstallments = installments.Count(i => i.PaymentDate.HasValue);
            var pendingInstallments = installments.Count - paidInstallments;
            
            var nextInstallment = installments.FirstOrDefault(i => !i.PaymentDate.HasValue);
            string nextDueDateMessage = nextInstallment != null
                ? nextInstallment.DueDate.ToString("yyyy-MM-dd")
                : "Todas las cuotas estan pagadas";

            var response = approved.Adapt<LoanDetailsResponse>();
            response.CustomerName = $"{customer.FirstName} {customer.LastName}";
            response.TotalAmount = Math.Round(_generalService
                .CalculateTotalAmount(approved.InterestRate, approved.Amount, approved.Months));

            response.PaidInstallments = paidInstallments;
            response.Profit = Math.Round(response.TotalAmount - response.Amount) ;
            response.PendingInstallments = pendingInstallments;
            response.NextDueDate = nextDueDateMessage;
            return response;
        }
    }
}
