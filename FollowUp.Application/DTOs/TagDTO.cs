using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowUp.Application.DTOs
{
    public class TagDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}
