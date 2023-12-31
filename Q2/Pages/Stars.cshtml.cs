using Client.Dto;
using Client.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Q1.Dto;
using Q1.Models;
using Q2.Dto;

namespace Q2.Pages
{
    public class StarsModel : PageModel
    {
        private readonly string baseUrl = "http://localhost:5270";
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly ApiOdataHelper _apiODataHelper = new ApiOdataHelper(_httpClient);

        public List<Star>? Stars { get; set; }

        public List<Movie>? Movies { get; set; }
        public Star? Star { get; set; }

        [BindProperty(SupportsGet = true)]
        public StarFilter? StarFilter { get; set; }


        [BindProperty(SupportsGet = true)]
        public StarRequest? StarRequest { get; set; }


        public async Task OnGet(int? id)
        {
            string[] filters = new string[0];
            if (StarFilter != null)
            {
                if (StarFilter.Nationality != null)
                {
                    filters = filters.Append($"tolower(nationality) eq '{StarFilter.Nationality.ToLower()}'").ToArray();
                }
                if (StarFilter.Gender != null)
                {
                    string isMale = StarFilter.Gender.Equals("Male") || StarFilter.Gender.Equals("male") ? "true" : "false";
                    filters = filters.Append($"male eq {isMale}").ToArray();
                }
            }

            var oDataParams = new Dictionary<string, string>
            {
                { "$expand", "movies" },
                { "$top", "30" },
                { "$skip", "0" }
            };

            if (filters.Length > 0)
            {
                oDataParams["$filter"] = string.Join(" and ", filters);
            }

            ODataObject<List<Star>>? result = await _apiODataHelper.GetWithODataAsync<ODataObject<List<Star>>>($"{baseUrl}/odata/stars", oDataParams);
            Stars = result?.Value;

            if (id != null)
            {

                Star = await _apiODataHelper.GetOneWithODataAsync<Star>($"{baseUrl}/odata/stars", id.ToString(), null);
            }

            ODataObject<List<Movie>>? moviesOdata = await _apiODataHelper.GetWithODataAsync<ODataObject<List<Movie>>>($"{baseUrl}/odata/Movies", null);
            Movies = moviesOdata?.Value;
        }

        public async Task OnPost(int? id)
        {
            if (StarRequest != null)
            {
                if(StarRequest.MovieIds == null)
                {
                    StarRequest.MovieIds = new List<int>();
                }
                await _apiODataHelper.PostWithODataAsync<Star>($"{baseUrl}/odata/Stars", StarRequest);
            }

        }

        public async Task<IActionResult> OnPostDelete(int? id)
        {
            if (id != null)
            {
                await _apiODataHelper.DeleteWithODataAsync<Star>($"{baseUrl}/odata/Stars", id.ToString());
            }

            return RedirectToPage();
        }

    }
}
