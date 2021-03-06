﻿using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using BLL.Models;
using BLL.Models.Enums;
using DAL;
using DAL.Interfaces;

namespace BLL.Implementations
{
    public class RequestService : IRequestService
    {
        private IRequestRepository requestRepository;

        private IDateService dateService;

        public RequestService(IRequestRepository requestRepository, IDateService dateService)
        {
            this.requestRepository = requestRepository;
            this.dateService = dateService;
        }

        public void CreateRequest(RequestModel requestModel)
        {
            var date = dateService.GetCurrentDate();
            var request = new Request
            {
                ClientId = requestModel.ClientId,
                Type = (int) requestModel.Type,
                State = (int) requestModel.State,
                Amount = requestModel.Amount,
                MonthIncome = requestModel.MonthIncome,
                CreditTypeId = requestModel.CreditTypeId,
                DepositTypeId = requestModel.DepositTypeId,
                Date = date
            };
            requestRepository.CreateRequest(request);
        }

        public List<RequestModel> GetRequestsByClient(int clientId)
        {
            var requests = requestRepository.GetClientRequests(clientId);
            return requests.Select(item => new RequestModel(item)).OrderByDescending(item => item.Date).ToList();
        }

        public RequestModel GetRequestDetails(int requestId)
        {
            var request = requestRepository.GetRequestById(requestId);
            if (request != null)
            {
                return new RequestModel(request, true);
            }
            return null;
        }

        public List<RequestModel> GetRequestQueForEmployee(int employeeId)
        {
            var requests = requestRepository.GetUnassignedAndPersonalRequests(employeeId, (int) RequestState.Pending);
            return requests.Select(item => new RequestModel(item)).OrderBy(item => item.Date).ToList();
        }

        public List<RequestModel> GetCheckedRequestQueForEmployee(int employeeId)
        {
            var requests = requestRepository.GetSecurityChechedRequests(employeeId);
            return requests.Select(item => new RequestModel(item)).OrderBy(item => item.Date).ToList();
        }

        public List<RequestModel> GetRequestsQueForSecurityWorker(int employeeId)
        {
            var requests = requestRepository.GetUnassignedAndPersonalRequests(employeeId,
                (int) RequestState.SecurityCheck);
            return requests.Select(item => new RequestModel(item)).OrderBy(item => item.Date).ToList();
        }

        public void AssignRequestToOperator(int requestId, int employeeId)
        {
            var request = requestRepository.GetRequestById(requestId);
            request.AssignedOperatorId = employeeId;
            requestRepository.UpdateRequest(request);
        }

        public void AssignRequestToSecurityWorker(int requestId, int employeeId)
        {
            var request = requestRepository.GetRequestById(requestId);
            request.AssignedSecurityWorkerId = employeeId;
            requestRepository.UpdateRequest(request);
        }

        public void ChangeRequestState(int requsetId, RequestState state)
        {
            var request = requestRepository.GetRequestById(requsetId);
            request.State = (int)state;
            requestRepository.UpdateRequest(request);
        }

        public void UpdateAmount(int requestId, int amount)
        {
            var request = requestRepository.GetRequestById(requestId);
            if (request != null)
            {
                request.Amount = amount;
            }
            requestRepository.UpdateRequest(request);
        }
    }
}
