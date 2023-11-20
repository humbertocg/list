
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace list.Models.API
{
	public class ApiEntries
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("entries")]
        public IEnumerable<Entry> Entries { get; set; }
    }

    public class Entry
    {
        [JsonProperty("API")]
        public string Api { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Auth")]
        public string Auth { get; set; }

        [JsonProperty("HTTPS")]
        public bool Https { get; set; }

        [JsonProperty("Cors")]
        public string Cors { get; set; }

        [JsonProperty("Link")]
        public Uri Link { get; set; }

        [JsonProperty("Category")]
        public string Category { get; set; }
    }
}

