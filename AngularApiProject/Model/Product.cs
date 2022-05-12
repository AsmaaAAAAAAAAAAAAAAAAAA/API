using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AngularApiProject.Model
{
    public class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        [ForeignKey("Category")]
        public int catID  { get; set; }
        public string img { get; set; }
        public DateTime date { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
