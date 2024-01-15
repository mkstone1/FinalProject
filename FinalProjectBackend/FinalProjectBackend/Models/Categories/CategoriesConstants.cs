using FinalProjectBackend.Features.Cards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBackend.Features.Categories
{
    public static class CategoriesConstants
    {
        public const string DefaultEndpoint = "categories";
        public const string Container = "categories";
       
    }
}
