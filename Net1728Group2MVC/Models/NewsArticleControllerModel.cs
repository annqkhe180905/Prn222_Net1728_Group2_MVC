﻿namespace Net1728Group2MVC.Models
{
    public class NewsArticleControllerModel
    {
        public string? NewsArticleId { get; set; }

        public string? NewsTitle { get; set; }

        public string Headline { get; set; } = string.Empty;

        public DateTime? CreatedDate { get; set; }

        public string? NewsContent { get; set; }

        public string? NewsSource { get; set; }

        public short? CategoryId { get; set; }

        public bool? NewsStatus { get; set; }

        public short? CreatedById { get; set; }

        public short? UpdatedById { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public List<int> TagIds { get; set; } = new List<int>();
    }
}
