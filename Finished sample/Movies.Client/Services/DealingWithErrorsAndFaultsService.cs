using Marvin.StreamExtensions;
using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class DealingWithErrorsAndFaultsService : IIntegrationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();

        public DealingWithErrorsAndFaultsService(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task Run()
        {
            // await GetMovieAndDealWithInvalidResponses(_cancellationTokenSource.Token);
            await PostMovieAndHandleValdationErrors(_cancellationTokenSource.Token);
        }

        private async Task GetMovieAndDealWithInvalidResponses(
            CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "api/movies/030a43b0-f9a5-405a-811c-bf342524b2be");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            using (var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                if (!response.IsSuccessStatusCode)
                {
                    // inspect the status code
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // show this to the user
                        Console.WriteLine("The requested movie cannot be found.");
                        return;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        // trigger a login flow
                        return;
                    }
                    response.EnsureSuccessStatusCode();
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var movie = stream.ReadAndDeserializeFromJson<Movie>();
            }
        }

        private async Task PostMovieAndHandleValdationErrors(
            CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("MoviesClient");

            var movieForCreation = new MovieForCreation()
            {
                Title = "Pulp Fiction",
                Description = "Too short",
                DirectorId = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                ReleaseDate = new DateTimeOffset(new DateTime(1992, 9, 2)),
                Genre = "Crime, Drama"
            };

            var serializedMovieForCreation = JsonConvert.SerializeObject(movieForCreation);

            using (var request = new HttpRequestMessage(
                HttpMethod.Post,
                "api/movies"))
            {
                request.Headers.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Content = new StringContent(serializedMovieForCreation);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.SendAsync(request,
                        HttpCompletionOption.ResponseHeadersRead,
                        cancellationToken))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                        {
                            var errorStream = await response.Content.ReadAsStreamAsync();
                            var validationErrors = errorStream.ReadAndDeserializeFromJson();
                            Console.WriteLine(validationErrors);
                            return;
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                        }
                    }

                    var stream = await response.Content.ReadAsStreamAsync();
                    var movie = stream.ReadAndDeserializeFromJson<Movie>();
                }
            }
        }
    }
}
