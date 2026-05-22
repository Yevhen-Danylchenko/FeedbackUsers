using Azure.Data.Tables;
using FeedbackUsers.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedbackUsers.Services
{
    public class AzureTableService
    {
        readonly TableClient _tableClient;

        public AzureTableService(IConfiguration configuration)
        {
            var connectionString = configuration["AzureStorage:ConnectionString"];
            var tableName = configuration["AzureStorage:TableName"];

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Azure Storage connection string must be provided in configuration.");
            }
            if (string.IsNullOrEmpty(tableName))
            {
                throw new InvalidOperationException("Azure Storage table name must be provided in configuration.");
            }

            _tableClient = new TableClient(connectionString, tableName);
            _tableClient.CreateIfNotExists();
        }

        public async Task AddFeedbackAsync(FeedbackModel obj)
        {
            var feedbackEntry = new FeedbackModel
            {
                Name = obj.Name,
                Email = obj.Email,
                Feedback = obj.Feedback,
                Timestamp = DateTimeOffset.UtcNow
            };
            await _tableClient.AddEntityAsync(feedbackEntry);
        }

        public async Task<List<FeedbackModel>> GetAllFeedbackAsync()
        {
            var listOfFeedback = new List<FeedbackModel>();
            await foreach (var feedback in _tableClient.QueryAsync<FeedbackModel>())
            {
                listOfFeedback.Add(feedback);
            }
            return listOfFeedback.ToList();
        }
    }
}
