using CasesApp.Data;
using CasesApp.Models;
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

        Case SendForReview { get; set; }

        Case Review { get; set; }

        Case Approve { get; set; }


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
            caseToAdd.CreateDate = DateTimeOffset.Now;
            _dbContext.Case.Add(caseToAdd);
            _dbContext.SaveChanges();

            return caseToAdd;

        }

        public Case Edit(Case caseToUpdate)
        {
            
            return caseToUpdate;

        }
        public Case Review
        {
            get => throw new NotImplementedException(); set => throw new NotImplementedException();
        }

        public Case SendForReview
        {
            get => throw new NotImplementedException(); set => throw new NotImplementedException();
        }
        public Case Approve
        {
            get => throw new NotImplementedException(); set => throw new NotImplementedException();
        }
    }
}
