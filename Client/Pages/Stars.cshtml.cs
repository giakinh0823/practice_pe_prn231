using Client.Dto;
using Client.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PE_BEST_PRACTICE.Models;
using System.IO;

namespace Client.Pages
{
    public class StarsModel : PageModel
    {
        private readonly string baseUrl = "http://localhost:5000";
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly ApiHelper _apiHelper = new ApiHelper(_httpClient);

        public List<Star>? Stars { get; set; }
        public Star? Star { get; set; }

        [BindProperty(SupportsGet = true)]
        public StarFilter? StarFilter { get; set; }


        public async Task OnGet(int? id)
        {
            if (StarFilter == null) {
                StarFilter = new StarFilter();
            }
            if (StarFilter.Nationality == null)
            {
                StarFilter.Nationality = "usa";
            }
            if (StarFilter.Gender == null)
            {
                StarFilter.Gender = "male";
            }

            Stars = await _apiHelper.GetAsync<List<Star>>($"{baseUrl}/api/Star/GetStars/" + StarFilter.Nationality + "/" + StarFilter.Gender);
            if (id != null)
            {
                Star = await _apiHelper.GetAsync<Star>($"{baseUrl}/api/Star/GetStar/" + id);
            }
        }
    }
}
