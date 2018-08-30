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
        List<Case> GetAll();
        List<Case> GetCasesToWork();
        List<Case> GetCasesToReview();       
        List<Case> GetCasesToApprove();
        List<Case> GetApprovedCases();
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
            //caseToUpdate.WorkerID = editedCase.WorkerID;
            //caseToUpdate.WorkerEmail = editedCase.WorkerEmail;
            //caseToUpdate.ReviewerID = editedCase.ReviewerID;
            //caseToUpdate.ReviewerEmail = editedCase.ReviewerEmail;
            //caseToUpdate.ApproverID = editedCase.ApproverID;
            //caseToUpdate.ApproverEmail = editedCase.ApproverEmail;
            //caseToUpdate.DateCreated = editedCase.DateCreated;
            //caseToUpdate.DateReviewed = editedCase.DateReviewed;
            //caseToUpdate.DateApproved = editedCase.DateApproved;
            caseToUpdate.Status = editedCase.Status;

            _dbContext.SaveChanges();

            return caseToUpdate;

        }

        public Case Get(int caseID)
        {
            Case caseToGet = _dbContext.Case
                .FirstOrDefault(x => x.ID == caseID);

            return caseToGet;
        }

        public List<Case> GetAll()
        {
            List<Case> casesToReview = _dbContext.Case
                .Include(x => x.Title)
                .OrderByDescending(x => x.Title).ToList();
            return casesToReview;
        }

        public List<Case> GetCasesToWork()
        {
            List<Case> casesToWork = _dbContext.Case
                .Where(x => x.Status == CaseStatus.Pending)
                .OrderByDescending(x => x.Title).ToList();
            return casesToWork;
        }
        public List<Case> GetCasesToReview()
        {
            List<Case> casesToReview = _dbContext.Case
                .Where(x => x.Status == CaseStatus.PendingReview)
                .OrderByDescending(x => x.Title).ToList();
            return casesToReview;
        }

        public List<Case> GetCasesToApprove()
        {
            List<Case> casesToApprove = _dbContext.Case
                .Where(x => x.Status == CaseStatus.PendingApproval)
                .OrderByDescending(x => x.Title).ToList();
            return casesToApprove;
        }

        public List<Case> GetApprovedCases()
        {
            List<Case> approvedCases = _dbContext.Case
                .Where(x => x.Status == CaseStatus.Approved)
                .OrderByDescending(x => x.Title).ToList();
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
            readyCase.DateReviewed = DateTime.UtcNow;
            return Edit(readyCase);
        }

        
        public Case Approve(Case caseToApprove)
        {
            caseToApprove.Status = CaseStatus.Approved;
            caseToApprove.DateApproved = DateTime.UtcNow;
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
