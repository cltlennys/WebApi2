﻿using Core.DTOs.PaymentInstallment;
using Core.Interfaces.Repositories;
using Core.Interfaces.Service;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi2.Controllers
{
    public class PaymentInstallmentController : BaseApiController
    {
        private readonly IPaymentService _paymentInstallamenService;
        private readonly IValidator<PaymentRequest> _paymentRequestValidator;

        public PaymentInstallmentController (IPaymentService paymentInstallamentService, IValidator<PaymentRequest> paymentValidator)
        {
            _paymentInstallamenService = paymentInstallamentService;
            _paymentRequestValidator = paymentValidator;
        }

        [HttpPost("pay-installments")]
        public async Task<IActionResult> PayInstallments([FromBody] PaymentRequest request)
        {
            var paymentInstallment = await _paymentInstallamenService.PayInstallmentsAsync(request.ApprovedLoanId, request.InstallmentIds);
            var validation = await _paymentRequestValidator.ValidateAsync(request);
            if (!validation.IsValid) return BadRequest(validation.Errors);

            if (paymentInstallment.StartsWith("Error")) return NotFound(paymentInstallment);

            return Ok(paymentInstallment);
        }
    }
}
