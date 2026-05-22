using System.ComponentModel.DataAnnotations;
using System;
using Azure;
using Azure.Data.Tables;

namespace FeedbackUsers.Models
{
    public class FeedbackModel : ITableEntity
    {
        public string PartitionKey { get; set; } = "FeedbackPartition";
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset? Timestamp { get; set; } 
        public ETag ETag { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Feedback is required.")]
        public string Feedback { get; set; } = string.Empty;
    }
}
