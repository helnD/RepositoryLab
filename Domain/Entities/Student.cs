using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;




namespace Domain.Entities
{
    
    //Класс описывает студента
    public class Student
    {
        [JsonPropertyName("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("birthday")]
        public DateTime Birthday { get; set; }
        
        [JsonPropertyName("grades")]
        public List<Grade> Grades { get; set; } = new List<Grade>();

        public override string ToString()
        {
            return $"{Id} | {Name}";
        }
    }
}