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
    public abstract class BaseApiController : ApiController
    {

        ICountingKsRepository Therepository;
        ModelFactory _modelFactory;

        public BaseApiController(ICountingKsRepository repo)
        {
            Therepository = repo;
            
        }
        protected ICountingKsRepository TheRepository
        {
            get
            {
                return Therepository;
            }
        }
        protected ModelFactory TheModelFactory
        {
            get
            {
                if(_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request, this.TheRepository);
                }
                return _modelFactory;
            }
        }
    }
}
