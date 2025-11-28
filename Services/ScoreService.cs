using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using BrickGame.Models;

namespace BrickGame.Services
{
    public class ScoreService
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        private string GetPath(string? path) =>
            string.IsNullOrWhiteSpace(path) ? Path.Combine(AppContext.BaseDirectory, "scores.json") : path!;

        public async Task<List<PlayerScore>> LoadAsync(string? path = null)
        {
            var filePath = GetPath(path);

            try
            {
                if (!File.Exists(filePath))
                    return new List<PlayerScore>();

                await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true);
                if (stream.Length == 0)
                    return new List<PlayerScore>();

                var result = await JsonSerializer.DeserializeAsync<List<PlayerScore>>(stream, _options);
                return result ?? new List<PlayerScore>();
            }
            catch (JsonException jex)
            {
                throw new InvalidOperationException($"B³¹d parsowania JSON w pliku '{filePath}': {jex.Message}", jex);
            }
            catch (Exception ex)
            {
                throw new IOException($"Nie mo¿na wczytaæ pliku '{filePath}': {ex.Message}", ex);
            }
        }

        public async Task SaveAsync(IEnumerable<PlayerScore> scores, string? path = null)
        {
            var filePath = GetPath(path);

            try
            {
                var directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                await JsonSerializer.SerializeAsync(stream, scores, _options);
                await stream.FlushAsync();
            }
            catch (Exception ex)
            {
                throw new IOException($"Nie mo¿na zapisaæ pliku '{filePath}': {ex.Message}", ex);
            }
        }
    }
}