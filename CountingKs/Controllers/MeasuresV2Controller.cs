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
    public class MeasuresV2Controller : BaseApiController
    {


        public MeasuresV2Controller(ICountingKsRepository repo) : base(repo)
        {

        }
        public IEnumerable<MeasureV2Model> Get(int id)
        {
            var results = TheRepository.GetMeasuresForFood(id)
                .ToList()
                .Select(m => TheModelFactory.Create2(m));
            return results;
        }
        public MeasureV2Model Get(int id, int measuresid)
        {
            var results = TheRepository.GetMeasure(measuresid);
            if(results.Food.Id == id)
            {
                return TheModelFactory.Create2(results);
            }
            return null;
        }
    }
}
