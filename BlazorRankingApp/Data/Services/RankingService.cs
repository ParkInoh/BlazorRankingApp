using Newtonsoft.Json;
using SharedData.Models;
using System.Text;

namespace BlazorRankingApp.Data.Services
{
    public class RankingService
    {
        HttpClient _httpClient;

        public RankingService(HttpClient client)
        {
            _httpClient= client;
        }

        // Create
        public async Task<GameResult> AddGameResult(GameResult gameResult)
        {
            // Post
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("api/ranking", content);

            // 실패
            if (result.IsSuccessStatusCode == false)
                throw new Exception("AddGameResult failed.");

            var resultContent = await result.Content.ReadAsStringAsync();
            // GameResult로 변환
            GameResult resultGameResult = JsonConvert.DeserializeObject<GameResult>(resultContent);

            return resultGameResult;
        }

        // Read
        public async Task<List<GameResult>> GetGameResultsAsync()
        {
            var result = await _httpClient.GetAsync("api/ranking");

            var resultContent = await result.Content.ReadAsStringAsync();
            // List<GameResult>로 변환
            List<GameResult> resultGameResults = JsonConvert.DeserializeObject<List<GameResult>>(resultContent);
            return resultGameResults;
        }

        // Update
        public async Task<bool> UpdateGameResult(GameResult gameResult)
        {
            // Post
            string jsonStr = JsonConvert.SerializeObject(gameResult);
            var content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync("api/ranking", content);

            // 실패
            if (result.IsSuccessStatusCode == false)
                throw new Exception("UpdateGameResult failed.");

            return true;
        }

        // Delete
        public async Task<bool> DeleteGameResult(GameResult gameResult)
        {
            var result = await _httpClient.DeleteAsync($"api/ranking/{gameResult.Id}");

            // 실패
            if (result.IsSuccessStatusCode == false)
                throw new Exception("DeleteGameResult failed.");

            return true;
        }
    }
}
