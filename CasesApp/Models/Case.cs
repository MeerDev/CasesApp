using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasesApp.Models
{
    public class Case
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public IdentityUser Worker { get; set; }
        public IdentityUser Reviewer { get; set; }       
        public IdentityUser Approver { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset? ReviewDate { get; set; }
        public DateTimeOffset? ApproveDate { get; set; }
        //Status values are Pending, Reviewed and Approved
        public CaseStatus Status { get; set; }
    }

    public enum CaseStatus
    {
        Pending = 0,
        Reviewed = 1,
        Approved = 2
    }
}

