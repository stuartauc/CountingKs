﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace CountingKs.Services
{
    public class CountingKsIdentityService : ICountingKsIdentityService
    {
        public string CurrentUser {
            get {
#if DEBUG
                return "shawnwildermuth";
#else
                return Thread.CurrentPrincipal.Identity.Name;
#endif

            }
        }

    }
}