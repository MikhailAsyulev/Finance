﻿using System;
using System.Net;
using System.Runtime.Caching;
using System.Web.Mvc;
using BankSystem.Models;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Enums;
using BLL.Models.ViewModel;

namespace BankSystem.Controllers
{
    public class CreditController : BaseController
    {
        private ICreditService creditService;

        private IRequestService requestService;

        private ICreditPaymentService creditPaymentService;

        private IDateService dateService;

        public CreditController(IUserService userService, IRequestService requestService, ICreditService creditService,
            ICreditPaymentService creditPaymentService, IDateService dateService) : base(userService)
        {
            this.requestService = requestService;
            this.creditService = creditService;
            this.creditPaymentService = creditPaymentService;
            this.dateService = dateService;
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult OpenCredit(int requestId)
        {
            var request = requestService.GetRequestDetails(requestId);
            if (request == null)
            {
                return new HttpNotFoundResult();
            }
            if (request.Type != RequestType.Credit)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(request);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult ConfirmCredit(int requestId)
        {
            var request = requestService.GetRequestDetails(requestId);
            if (request == null)
            {
                return new HttpNotFoundResult();
            }
            if (request.Type != RequestType.Credit)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            creditService.OpenCredit(request);
            var credit = creditService.FindByRequestId(requestId);
            if (credit != null)
            {
                return RedirectToAction("EmployeeDetails", new { creditId = credit.Id });
            }
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult EmployeeDetails(int creditId)
        {
            var currentDate = dateService.GetCurrentDate();
            ViewBag.CurrentDate = currentDate.Date;
            var credit = creditService.GetCreditById(creditId);
            if (credit == null)
            {
                return new HttpNotFoundResult();
            }
            try
            {
                if (credit.EndDate > currentDate)
                {
                    credit.MonthlyPayment = creditService.CalculateMonthPayment(credit.StartAmount, credit.CreditType.ReturnTerm, credit.CreditType.Percent);
                }
                else
                {
                    credit.MonthlyPayment = null;
                }
                var cache = MemoryCache.Default;
                var refreshCode = cache.Get("RefreshCode");
                if (refreshCode == null)
                {
                    refreshCode = new RefreshCodeModel(); 
                }
                credit.ReturnCodeModel = (RefreshCodeModel) refreshCode;
            }
            catch (Exception)
            {
                credit.MonthlyPayment = null;
            }
            return View(credit);
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult CreditsByClient(int clientId)
        {
            var credits = creditService.GetClientCredits(clientId);
            var client = userService.GetUserById(clientId);
            if (client == null)
            {
                return new HttpNotFoundResult();
            }
            var model = new ClientCreditsModel
            {
                Client = client,
                Credits = credits
            };
            return View(model);
        }

        [Authorize(Roles = "Client")]
        public ActionResult CreditsForClient()
        {
            var credits = creditService.GetClientCredits(CurrentUser.UserId);
            return View(credits);
        }

        [Authorize(Roles = "Client")]
        public ActionResult ClientDetails(int creditId)
        {
            var currentDate = dateService.GetCurrentDate();
            ViewBag.CurrentDate = currentDate.Date;
            var credit = creditService.GetCreditById(creditId);
            if (credit == null)
            {
                return new HttpNotFoundResult();
            }
            if (credit.ClientId != CurrentUser.UserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                if (credit.EndDate > currentDate)
                {
                    credit.MonthlyPayment = creditService.CalculateMonthPayment(credit.StartAmount, credit.CreditType.ReturnTerm, credit.CreditType.Percent);
                }
                else
                {
                    credit.MonthlyPayment = null;
                }
            }
            catch (Exception)
            {
                credit.MonthlyPayment = null;
            }
            return View(credit);
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult CancelPayment(int paymentId)
        {
            var payment = creditPaymentService.GetPaymentById(paymentId);
            if (payment == null)
            {
                return new HttpNotFoundResult();
            }
            creditPaymentService.CancelPayment(paymentId);
            return RedirectToAction("EmployeeDetails", new { creditId = payment.CreditId });
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        public ActionResult IncomePayment(int creditId)
        {
            var credit = creditService.GetCreditById(creditId);
            if (credit == null)
            {
                return new HttpNotFoundResult();
            }
            var paymentModel = new CreditPaymentModel
            {
                CreditModel = credit,
                CreditId = creditId
            };
            return View(paymentModel);
        }

        [Authorize(Roles = "Admin, Operator, SecurityWorker")]
        [HttpPost]
        public ActionResult IncomePayment(int creditId, CreditPaymentModel paymentModel)
        {
            var credit = creditService.GetCreditById(creditId);
            if (credit == null)
            {
                return new HttpNotFoundResult();
            }
            if (ModelState.IsValidField("MainAmount") && paymentModel.MainAmount > 0 &&
                paymentModel.MainAmount <= 1000000000 &&
                paymentModel.MainAmount <= credit.MainDebt + credit.PercentageDebt)
            {
                paymentModel.Type = CreditPaymentType.Payment;
                creditPaymentService.AddPayment(paymentModel);
                return RedirectToAction("EmployeeDetails", new {creditId = creditId});
            }
            paymentModel.CreditModel = credit;
            ModelState.Clear();
            ModelState.AddModelError("", "Некорректное значение суммы");
            return View(paymentModel);
        }
    }
}