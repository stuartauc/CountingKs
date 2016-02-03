using CountingKs.Data;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class MeasuresController : BaseApiController
    {


        public MeasuresController(ICountingKsRepository repo) : base(repo)
        {

        }
        public IEnumerable<MeasureModel> Get(int id)
        {
            var results = TheRepository.GetMeasuresForFood(id)
                .ToList()
                .Select(m => TheModelFactory.Create(m));
            return results;
        }
        public MeasureModel Get(int id, int measuresid)
        {
            var results = TheRepository.GetMeasure(measuresid);
            if(results.Food.Id == id)
            {
                return TheModelFactory.Create(results);
            }
            return null;
        }
    }
}
