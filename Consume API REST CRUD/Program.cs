using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Consume_API_REST_CRUD
{
    internal class Program
    {
        private static string sUrlApi = "https://fakerestapi.azurewebsites.net/api/v1/Activities";
        static async Task Main(string[] args)
        {
            await GetBook();
            await UploadBook();
            await GetOneElement();
            Console.ReadLine();
        }

        private static async Task GetBook()
        {
            using (HttpClient oClient = new HttpClient())
            {
                HttpResponseMessage oResponse = await oClient.GetAsync(sUrlApi);

                oResponse.EnsureSuccessStatusCode();

                string sReplyGet = await oResponse.Content.ReadAsStringAsync();

                Console.WriteLine(sReplyGet);
            }
        }

        private static async Task UploadBook()
        {
            var oActivity = new
            {
                id = 1,
                title = "Activity 1",
                dueDate = "2023-10-24T05:16:54.9960759+00:00",
                completed = false
            };
            string sJson = JsonConvert.SerializeObject(oActivity);
            using (HttpClient oCliente = new HttpClient())
            {
                var oContent = new StringContent(sJson, Encoding.UTF8, "application/json");
                HttpResponseMessage oResponse = await oCliente.PostAsync(sUrlApi, oContent);

                if (oResponse.IsSuccessStatusCode)
                {
                     string sReplyUpload = await oResponse.Content.ReadAsStringAsync();
                    Console.WriteLine(sReplyUpload);
                }
            }
        }

        private static async Task GetOneElement()
        {
            Console.WriteLine("Enter the number of the activity you wish to consult: ");
            int iActivity = int.Parse(Console.ReadLine());

            using (HttpClient oClient = new HttpClient())
            {
                HttpResponseMessage oResponse = await oClient.GetAsync($"{sUrlApi}/{iActivity}");
                oResponse.EnsureSuccessStatusCode();
                if (oResponse.IsSuccessStatusCode)
                {
                    string sReplyGetOne = await oResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Activity Added: {sReplyGetOne}");
                }
            }
        }

        private static async Task DeleteOneElement()
        {
            Console.WriteLine("Enter the number of the activity you wish to delete: ");
            int iActivityDelete = int.Parse(Console.ReadLine());

            using (HttpClient oClient = new HttpClient())
            {
                HttpResponseMessage oResponse = await oClient.DeleteAsync($"{sUrlApi}/{iActivityDelete}");
                oResponse.EnsureSuccessStatusCode();
                if (oResponse.IsSuccessStatusCode)
                {
                    string sReplyDelete = await oResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Activity Deleted : {sReplyDelete}");
                }
            }
        }

        private static async Task GetCredentials()
        {
            using (HttpClient oClient = new HttpClient())
            {
                //Example / NOT APPLICABLE
                HttpResponseMessage oResponse = await oClient.PostAsync($"{sUrlApi}?sRFCColaborador={"RFCExample"}&sRFCEmpresa={"RFCExample"}", null);

                if (oResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //The information was validated
                }
                else
                {
                    //There was an error in the request
                }
            }
        }

    }
}
