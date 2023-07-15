using Client.Helper;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Client.Pages
{
    public class IndexModel : PageModel
    {
        private readonly string baseUrl = "http://localhost:5000";

        private readonly ILogger<IndexModel> _logger;

        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly ApiOdataHelper _apiODataHelper = new ApiOdataHelper(_httpClient);
        public List<Director>? Directors { get; set; }
        public Director Director { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public DirectorFilter? Filter { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public async Task OnGet(int? id)
        {
            string[] filters = new string[0];
            if (Filter != null)
            {
                if (Filter.Nationality != null)
                {
                    filters = filters.Append($"tolower(nationality) eq '{Filter.Nationality.ToLower()}'").ToArray();
                }
                if (Filter.Gender != null)
                {
                    string isMale = Filter.Gender.Equals("Male") || Filter.Gender.Equals("male") ? "true" : "false";
                    filters = filters.Append($"male eq {isMale}").ToArray();
                }
            }

            var oDataParams = new Dictionary<string, string>
            {
                { "$expand", "movies" },
                { "$top", "10" },
                { "$skip", "0" }
            };

            if (filters.Length > 0)
            {
                oDataParams["$filter"] = string.Join(" and ", filters);
            }

            ODataObject<List<Director>> result = await _apiODataHelper.GetWithODataAsync<ODataObject<List<Director>>>($"{baseUrl}/odata/directors", oDataParams);
            Directors = result.Value;

            if(id != null)
            {
                var oDataParamsDetail = new Dictionary<string, string>
                {
                    { "$expand", "movies" },
                };

                Director = await _apiODataHelper.GetOneWithODataAsync<Director>($"{baseUrl}/odata/directors", id.ToString(), oDataParamsDetail);
            }
        }
    }
}