using Marvin.StreamExtensions;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Client.Services
{
    public class CancellationService : IIntegrationService
    {
        private static HttpClient _httpClient = new HttpClient(
          new HttpClientHandler()
          {
              AutomaticDecompression = System.Net.DecompressionMethods.GZip
          });
        private CancellationTokenSource _cancellationTokenSource = 
            new CancellationTokenSource();

        public CancellationService()
        {
            // set up HttpClient instance
            _httpClient.BaseAddress = new Uri("http://localhost:57863");
            _httpClient.Timeout = new TimeSpan(0, 0, 5);
            _httpClient.DefaultRequestHeaders.Clear();
        }

        public async Task Run()
        {
            //_cancellationTokenSource.CancelAfter(2000);
            //await GetTrailerAndCancel(_cancellationTokenSource.Token);
            await GetTrailerAndHandleTimeout();
        }

        private async Task GetTrailerAndCancel(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/trailers/{Guid.NewGuid()}");

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            //var cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.CancelAfter(2000);
            try
            { 
                using (var response = await _httpClient.SendAsync(request,
                   HttpCompletionOption.ResponseHeadersRead,
                   cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();
                    var trailer = stream.ReadAndDeserializeFromJson<Trailer>();
                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"An operation was cancelled with message {ocException.Message}.");
                // additional cleanup, ...
            }
        }

        private async Task GetTrailerAndHandleTimeout()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/movies/d8663e5e-7494-4f81-8739-6e0de1bea7ee/trailers/{Guid.NewGuid()}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            try
            {
                using (var response = await _httpClient.SendAsync(request,
                    HttpCompletionOption.ResponseHeadersRead))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    response.EnsureSuccessStatusCode();
                    var trailer = stream.ReadAndDeserializeFromJson<Trailer>();
                }
            }
            catch (OperationCanceledException ocException)
            {
                Console.WriteLine($"An operation was cancelled with message {ocException.Message}.");
                // additional cleanup, ...
            }
        }
    }
}
