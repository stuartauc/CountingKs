﻿using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class TokenController : BaseApiController
    {
        public TokenController(ICountingKsRepository repo) : base(repo)
        {

        }
        public HttpResponseMessage Post([FromBody]TokenRequestModel model)
        {
            try
            {
                var apiUser = TheRepository.GetApiUsers().Where(x => x.AppId == model.ApiKey).FirstOrDefault();
                if (apiUser != null)
                {
                    var secret = apiUser.Secret;

                    var key = Convert.FromBase64String(secret);
                    var provider = new System.Security.Cryptography.HMACSHA256(key);

                    var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(apiUser.AppId));
                    var signature = Convert.ToBase64String(hash);

                    if(signature == model.Signature)
                    {
                        var rawTokenInfo = String.Concat(apiUser.AppId + DateTime.UtcNow.ToString("d"));
                        var rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
                        var token = provider.ComputeHash(rawTokenByte);
                        var authToken = new AuthToken()
                        {
                            Token = Convert.ToBase64String(token),
                            Expiration = DateTime.UtcNow.AddDays(7),
                            ApiUser = apiUser
                        };
                        if(TheRepository.Insert(authToken) && TheRepository.SaveAll())
                        {
                            return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(authToken));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
            
        }
    }
}
