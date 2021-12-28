using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;

namespace Domain.Entities
{

    public class WebsiteUptimeScore
    {
        [Key] public int IdWebsiteUptimeScore { get; set; }
        [ForeignKey("Website")] public Guid IdWebsite { get; set; }
        public string StatusNumber { get; set; }
        public int ElapsedTime { get; set; }
        public DateTime TestTime { get; set; }
        public Website Website { get; set; }
    }
}