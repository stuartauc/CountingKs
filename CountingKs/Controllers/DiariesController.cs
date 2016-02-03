using CountingKs.Data;
using CountingKs.Filters;
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
    [CountingKsAuthorize]
    public class DiariesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiariesController(ICountingKsRepository repo, ICountingKsIdentityService identityService) : base(repo)
        {
            _identityService = identityService;
        }
        public IEnumerable<DiaryModel> Get()
        {
            var userName = _identityService.CurrentUser;
            var results = TheRepository.GetDiaries(userName)
                            .OrderByDescending(d => d.CurrentDate)
                            .Take(10)
                            .ToList()
                            .Select(d => TheModelFactory.Create(d));
            return results;
        }

        public HttpResponseMessage Get(DateTime diaryId)
        {
            var userName = _identityService.CurrentUser;
            var result = TheRepository.GetDiary(userName, diaryId);
            if(result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);

            }
            return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.Create(result));

        }
    }
}
