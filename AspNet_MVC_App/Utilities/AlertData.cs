using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNet_MVC_App.Utilities
{
    public class AlertData
    {
        public string Text { get; set; }
        public string Type { get; set; }

        public AlertData() { }
        public AlertData(string jsonString)
        {
            var data = JsonSerializer.Deserialize<AlertData>(jsonString);
            this.Text = data.Text;
            this.Type = data.Type;
        }
    }
}
