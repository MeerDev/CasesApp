using CasesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasesApp.Services
{
    public interface ICaseService
    {
        Case Add { get; set; }

        Case Review { get; set; }

        Case Approve { get; set; }


    }

    public class CaseService : ICaseService
    {
        public Case Add { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Case Review { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Case Approve { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
