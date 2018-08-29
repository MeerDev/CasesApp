using System;
using System.Collections.Generic;
using System.Text;
using CasesApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CasesApp.Services;
using CasesApp.Data;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CaseUnitTests
    {
        private CaseService _caseService;

        public CaseUnitTests()
        {
            var mockSet = new Mock<DbSet<Case>>();
            var mockContext = new Mock<ApplicationDbContext>();

            mockContext.Setup(m => m.Case).Returns(mockSet.Object);

            var _caseService = new CaseService(mockContext.Object);
        }

        [TestMethod]
        public void Add_CaseNotCreated_CaseIsCreated()
        {                     
            Case caseToAdd = new Case { Title = "Test Case 1", Details = "", WorkerID = "worker1id", DateReviewed = null, DateApproved = null, Status = CaseStatus.Pending };

            _caseService.Add(caseToAdd);

            //Get all cases since we don't know ID of the newly created case
            var cases = _caseService.GetAll();


            var addedCase = cases.FirstOrDefault(x => x.Title == "Test Case 1");

            Assert.IsNotNull(addedCase);
        }

        [TestMethod]
        public void Edit_DetailsNotSet_DetailsSet()
        {
            var testCase = _caseService.GetAll().FirstOrDefault();

            testCase.Details = "Test Details for the case";

            _caseService.Edit(testCase);

            var updatedCase = _caseService.Get(testCase.ID);

            Assert.AreEqual(updatedCase.Details, "Test Details for the case");
        }

        [TestMethod]
        public void ReadyForReview_CaseNotReadyForReview_CaseIsReadyForReview()
        {
            var testCase = _caseService.GetAll().FirstOrDefault();

            _caseService.ReadyForReview(testCase);

            var updatedCase = _caseService.Get(testCase.ID);

            Assert.AreEqual(updatedCase.Status, CaseStatus.PendingReview);

        }

        [TestMethod]
        public void ReadyForApproval_CaseNotReadyForApproval_CaseIsReadyForApproval()
        {
            var testCase = _caseService.GetAll().FirstOrDefault();

            _caseService.ReadyForApproval(testCase);

            var updatedCase = _caseService.Get(testCase.ID);

            Assert.AreEqual(updatedCase.Status, CaseStatus.PendingApproval);

        }

        [TestMethod]
        public void Approve_CaseIsNotApproved_CaseIsApproved()
        {
            var testCase = _caseService.GetAll().FirstOrDefault();

            _caseService.Approve(testCase);

            var updatedCase = _caseService.Get(testCase.ID);

            Assert.AreEqual(updatedCase.Status, CaseStatus.Approved);

        }


    }
}
