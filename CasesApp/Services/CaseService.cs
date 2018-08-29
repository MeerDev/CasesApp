using CasesApp.Data;
using CasesApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CasesApp.Services
{
    public interface ICaseService
    {
        Case Add(Case caseToAdd);
        Case Edit(Case caseToUpdate);
        Case ReadyForReview(Case caseToFlag);
        Case ReadyForApproval(Case caseToFlag);
        Case Approve(Case caseToApprove);
        Case SendBackToWorker(Case caseToSendBack);
        Case SendBackToReviewer(Case caseToSendBack);
        IEnumerable<Case> GetAll();
        IEnumerable<Case> GetCasesToReview();
        IEnumerable<Case> GetCasesToApprove();
        IEnumerable<Case> GetApprovedCases();
    }

    public class CaseService : ICaseService
    {
        private ApplicationDbContext _dbContext;

        public CaseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Case Add(Case caseToAdd)
        {
            caseToAdd.Status = CaseStatus.Pending;
            caseToAdd.DateCreated = DateTime.Now;
            _dbContext.Case.Add(caseToAdd);
            _dbContext.SaveChanges();

            return caseToAdd;

        }

        public Case Edit(Case editedCase)
        {
            Case caseToUpdate = Get(editedCase.ID);

            caseToUpdate.Title = editedCase.Title;
            caseToUpdate.Details = editedCase.Details;
            caseToUpdate.WorkerID = editedCase.WorkerID;
            caseToUpdate.ReviewerID = editedCase.ReviewerID;
            caseToUpdate.ApproverID = editedCase.ApproverID;
            caseToUpdate.DateCreated = editedCase.DateCreated;
            caseToUpdate.DateReviewed = editedCase.DateReviewed;
            caseToUpdate.DateApproved = editedCase.DateApproved;
            caseToUpdate.Status = editedCase.Status;

            _dbContext.SaveChanges();

            return caseToUpdate;

        }

        public Case Get(int caseID)
        {
            Case caseToGet = _dbContext.Case
                .Include(x => x.Title)
                .Include(x => x.Details)
                .Include(x => x.DateCreated)
                .Include(x => x.DateReviewed)
                .Include(x => x.DateApproved)
                .FirstOrDefault(x => x.ID == caseID);

            return caseToGet;
        }

        public IEnumerable<Case> GetAll()
        {
            IEnumerable<Case> casesToReview = _dbContext.Case
                .Include(x => x.Title)
                .Include(x => x.WorkerID)
                .Include(x => x.ReviewerID)
                .Include(x => x.ApproverID)
                .OrderByDescending(x => x.Title);
            return casesToReview;
        }

        public IEnumerable<Case> GetCasesToReview()
        {
            IEnumerable<Case> casesToReview = _dbContext.Case
                .Include(x => x.Title)
                .Include(x => x.WorkerID)
                .Include(x => x.ReviewerID)
                .Include(x => x.ApproverID)
                .Where(x => x.Status == CaseStatus.PendingReview)
                .OrderByDescending(x => x.Title);
            return casesToReview;
        }

        public IEnumerable<Case> GetCasesToApprove()
        {
            IEnumerable<Case> casesToApprove = _dbContext.Case
                .Include(x => x.Title)
                .Include(x => x.WorkerID)
                .Include(x => x.ReviewerID)
                .Include(x => x.ApproverID)
                .Where(x => x.Status == CaseStatus.PendingApproval)
                .OrderByDescending(x => x.Title);
            return casesToApprove;
        }

        public IEnumerable<Case> GetApprovedCases()
        {
            IEnumerable<Case> approvedCases = _dbContext.Case
                .Include(x => x.Title)
                .Include(x => x.WorkerID)
                .Include(x => x.ReviewerID)
                .Include(x => x.ApproverID)
                .Where(x => x.Status == CaseStatus.Approved)
                .OrderByDescending(x => x.Title);
            return approvedCases;
        }


        public Case ReadyForReview(Case readyCase)
        {
            readyCase.Status = CaseStatus.PendingReview;
            return Edit(readyCase);
        }

        public Case ReadyForApproval(Case readyCase)
        {
            readyCase.Status = CaseStatus.PendingApproval;
            return Edit(readyCase);
        }

        
        public Case Approve(Case caseToApprove)
        {
            caseToApprove.Status = CaseStatus.Approved;
            return Edit(caseToApprove);
        }

        public Case SendBackToWorker(Case caseToSendBack)
        {
            caseToSendBack.Status = CaseStatus.Pending;
            return Edit(caseToSendBack);
        }

        public Case SendBackToReviewer(Case caseToSendBack)
        {
            caseToSendBack.Status = CaseStatus.PendingReview;
            return Edit(caseToSendBack);
        }
    }
}
