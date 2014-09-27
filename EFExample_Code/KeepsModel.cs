using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class Keep
    {
        [Key]
        //[ForeignKey("Reminder")]
        public int KeepId { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Caption")]
        public string UserCaption { get; set; }
        public string ContentUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string MetaCaption { get; set; }
        public bool IsPublic { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Timestamp { get; set; }
        [NotMapped]
        [JsonIgnore]
        public ICollection<ApplicationUser> MutualUsers { get; set; }
        [NotMapped]
        [JsonIgnore]
        public double SimilarityPercentageByGenre { get; set; }
        [NotMapped]
        [JsonIgnore]
        public double SimilarityPercentageByTags { get; set; }
        public int? PlacesMetaDataId { get; set; }
        [ForeignKey("PlacesMetaDataId")]
        public virtual PlacesMetaData PlacesMetaData { get; set; }
        public int? SoundCloudMetaDataId { get; set; }
        [ForeignKey("SoundCloudMetaDataId")]
        public virtual SoundCloudMetaData SoundCloudMetaData { get; set; }
        public int? YouTubeMetaDataId { get; set; }
        [ForeignKey("YouTubeMetaDataId")]
        public virtual YouTubeMetaData YouTubeMetaData { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        //public int ReminderId { get; set; }
        //[JsonIgnore]
        //public virtual Reminder Reminder { get; set; }


        [JsonIgnore]
        public ICollection<Group> Groups { get; set; }
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> FollowingUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }
    }

    //public class Reminder
    //{
    //    public int ReminderId { get; set; }
    //    public string ReminderType { get; set; }
    //    public double Latitude { get; set; }
    //    public double Longitude { get; set; }
    //    public DateTime Time { get; set; }
    //    [Required]
    //    public int KeepId { get; set; }
    //    [JsonIgnore]
    //    public virtual Keep Keep { get; set; }
    //    [Required]
    //    public int StatusId { get; set; }
    //    public virtual Status Status { get; set; }
    //}

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryValue { get; set; }
        /// <summary>
        /// Used by WeKeep Api
        /// </summary>
        public string ColorValue { get; set; }
        /// <summary>
        /// Possible values: WeKeepApi, PlacesApi, YouTubeApi, SoundCloudApi
        /// </summary>
        public string ApiName { get; set; }
        //[JsonIgnore]
        //public int? YouTubeMetaDataId { get; set; }
        //[JsonIgnore]
        //public virtual YouTubeMetaData YouTubeMetaData { get; set; }
        [JsonIgnore]
        public virtual ICollection<Keep> Keeps { get; set; }
        [JsonIgnore]
        public virtual ICollection<SoundCloudMetaData> SoundCloudMetaDatas { get; set; }
    }
}
