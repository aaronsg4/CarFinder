using CarFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CarFinder.Controllers
{
    /// <summary>
    /// Prefix
    /// </summary>
    [RoutePrefix("api/Cars")]
    public class CarsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Get all years
        /// </summary>
        /// <returns></returns>
        [Route("Year")]
        public async Task<List<string>> GetYears()
        {
            return await db.GetYears();
        }

        /// <summary>
        /// HP above 300
        /// </summary>
        /// <returns></returns>
        [Route("HP")]
        [HttpGet]
        public async Task<List<Car>> HP()
        {
            return await db.HP();
        }

        /// <summary>
        /// SUV
        /// </summary>
        /// <param name="bodystyle"></param>
        /// <returns></returns>
        [Route("SUVs")]
        [HttpGet]
        public async Task<List<Car>> SUVs(string bodystyle)
        {
            return await db.SUVs(bodystyle);
        }
        /// <summary>
        /// Get all makes for a year
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Route("Make")]
        public async Task<List<string>> GetMakes(string year)
        {
            return await db.GetMakes(year);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("Model")]
        public async Task<List<string>> GetModels(string year, string make)
        {
            return await db.GetModels(year, make);
        }
        
            /// <summary>
            /// 
            /// </summary>
            /// <param name="year"></param>
            /// <param name="make"></param>
            /// <param name="model"></param>
            /// <returns></returns>
        [Route("Trim")]
        public async Task<List<string>> GetTrims(string year, string make, string model)
        {
            return await db.GetTrims(year, make,model);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        [Route("CarsByYear")]
        [HttpGet]
        public async Task<List<Car>> CarsByYear(string year)
        {
            return await db.CarsByYear(year);

        }
        /// <summary>
        /// Cars by year and make
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("CarsByYearAndMake")]
        [HttpGet]
        public async Task<List<Car>> CarsByYearandMake(string year, string make)
        {
            return await db.CarsByYearandMake(year,make);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <returns></returns>
        [Route("CarsByYearAndMake")]
        [HttpGet]
        //public async Task<List<Car>> CarsByYearMakeModel(string year, string make string model)
        //{
        //    return await db.CarsByYearMakeModel(year, make, model);

        //}












        /// <summary>
        /// Request Recall Data
        /// </summary>
        /// <param name="year"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("carRecall")]
        //Request Recall Data from NHTSA  In order to consume their API we have to send URL request so we are building URL here:
        public async Task<CarViewModel> getCarRecall(string year, string make, string model, string trim)
        {
            HttpResponseMessage response;
            var content = "";
            var car = new CarViewModel();
            car.Recalls = content;

            //get car recall info from NTHSA

            //instantiate new http client
            using(var client = new HttpClient())
            {
                //setting domain address and then concatenating
                client.BaseAddress = new Uri("https://one.nhtsa.gov/");
                try
                {
                    response = await client.GetAsync("webapi/api/Recalls/vehicle/modelyear/" + year + "/make/" + make + "/model/" + model + "?format=json");
                    content = await response.Content.ReadAsStringAsync();

                }
             catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    await Task.FromResult(e);
                }
            }
            //Bing Cognitive Image Search
            var query = year + " " + make + " " + model + " " + trim;
            var url = $"https://api.cognitive.microsoft.com/bing/v5.0/images/" +
                $"search?q={WebUtility.UrlEncode(query)}" + 
                $"&count=3&offset=0&mkt=en-us&safeSearch=Strict";
            var requestHeaderKey = "Ocp-Apim-Subscription-Key";
            var requestHeaderValue = ConfigurationManager.AppSettings["BingSearchKey"];
            try
            {
                using(var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add(requestHeaderKey, requestHeaderValue);
                    var json = await client.GetStringAsync(url);
                    var result = JsonConvert.DeserializeObject<SearchResult>(json);
                    car.ImgSearchResult = result.Images.Select(i => new ImageResults
                    {
                        ContextLink = i.HostPageUrl,
                        FileFormat = i.EncodingFormat,
                        ImageLink = i.ContentUrl,
                        ThumbnailLink = i.ThumbnailUrl,
                        Title = i.Name
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
            return car;
        }

    }
}
