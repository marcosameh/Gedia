using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Geidea_integration.Pages
{
    [IgnoreAntiforgeryToken]
    public class CheckoutModel : PageModel
    {
        private readonly HttpClient httpClient;

        public CheckoutModel(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient.CreateClient("Geidea");
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostCreateSessionAsync()
        {
            //string merchantPublicKey = "32fcca09-11da-4438-aad7-6535c8ae7ac6";
            //string apiPassword = "821dcb92-85a0-47be-9548-852349aa5682";
            //string apiUrl = "https://api.merchant.geidea.net/payment-intent/api/v1/direct/session";


            var RequestUri = "/payment-intent/api/v1/direct/session";
            var requestData = new
            {
                amount = 10.00,
                currency = "USD",
                callbackUrl = "https://www.example.com/callback",
                merchantReferenceId = "ABC-123",
                language = "ar"
            };
            var response = await httpClient.PostAsJsonAsync(RequestUri, requestData);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
               
                return Content(responseContent, "application/json");
            }
            else
            {
                return BadRequest("Error creating Geidea Checkout session.");
            }


           
        }

    }
}