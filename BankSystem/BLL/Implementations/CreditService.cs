﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Enums;
using BLL.Models.GridModels.Credit;
using DAL;
using DAL.Interfaces;

namespace BLL.Implementations
{
    public class CreditService : ICreditService
    {
        private ICreditTypeReporsitory creditTypeReporsitory;

        private ICreditRepository creditRepository;

        public CreditService(ICreditRepository creditRepository, ICreditTypeReporsitory creditTypeReporsitory)
        {
            this.creditRepository = creditRepository;
            this.creditTypeReporsitory = creditTypeReporsitory;
        }

        public void OpenCredit(RequestModel request)
        {
            if (request.Type != RequestType.Credit)
            {
                return;
            }
            var creditType = creditTypeReporsitory.GetCreditTypeById(request.CreditTypeId.Value);
            var credit = new Credit
            {
                ClientId = request.ClientId,
                MainDebt = request.Amount,
                StartDate = DateTime.Now,
                CreditTypeId = creditType.Id,
                RequestId = request.Id,
                PercentageDebt = 0
            };
            creditRepository.CreateCredit(credit);
        }

        public CreditModel FindByRequestId(int requestId)
        {
            var credit = creditRepository.FindByRequestId(requestId);
            if (credit != null)
            {
                return new CreditModel(credit);
            }
            return null;
        }

        public CreditModel GetCreditById(int creditId)
        {
            var credit = creditRepository.GetCreditById(creditId);
            if (credit != null)
            {
                return new CreditModel(credit);
            }
            return null;
        }

        public List<CreditModel> GetClientCredits(int clientId)
        {
            return
                creditRepository.GetClientCredits(clientId)
                    .OrderByDescending(item => item.StartDate)
                    .Select(item => new CreditModel(item))
                    .ToList();
        }

        public decimal CalculateMonthPayment(decimal amount, TimeSpan returnTerm, double yearPercent)
        {
            var monthCount = 12*returnTerm.TotalDays/365.0;
            var monthPercent = Math.Pow(1 + yearPercent, 1.0 / 12);
            var k = Math.Pow(monthPercent, monthCount);
            var result = amount*(decimal) (k*(monthPercent - 1)/(k - 1));
            return result;
        }

        public void Percents()
        {
            creditRepository.Percents();
        }

        public List<CreditRowModel> GetCreditGrid()
        {
            return creditRepository.GetCredits().Select( ct => Mapper.Map<CreditRowModel>(ct)).ToList();
        }
    }
}
