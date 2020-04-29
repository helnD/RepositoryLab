
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Grade
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [JsonPropertyName("value")]
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}