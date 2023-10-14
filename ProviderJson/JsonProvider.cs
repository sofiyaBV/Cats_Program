using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats_Program.ProviderJson
{
    public class JsonProvider
    {
       [JsonProperty("data")]
       public List<string> Data { get; set; }
    }
}
