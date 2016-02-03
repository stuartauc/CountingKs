using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class DiarySummaryController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;
        public DiarySummaryController(ICountingKsRepository repo, ICountingKsIdentityService identityService) : base(repo)
        {
            _identityService = identityService;
        }
        //public HttpResponseMessage Get(DateTime diaryId)
        //{
        //    try
        //    {
        //        var diary = 
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

    }
}
