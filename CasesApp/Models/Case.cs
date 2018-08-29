﻿using Microsoft.AspNetCore.Identity;
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
        public string WorkerID { get; set; }
        public string ReviewerID { get; set; }       
        public string ApproverID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateReviewed { get; set; }
        public DateTime? DateApproved { get; set; }
        //Status values are Pending, Reviewed and Approved
        public CaseStatus Status { get; set; }
    }

    public enum CaseStatus
    {
        Pending,
        PendingReview,
        PendingApproval,
        Approved
    }
}

