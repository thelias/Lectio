using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeKeepsService.Entities
{
    public class PlacesMetaData
    {
        [Key]
        public int PlacesMetaDataId { get; set; }
        public string Vicinity { get; set; }
        public virtual ICollection<Tag> PlacesTags { get; set; }
    }


    public class YouTubeMetaData
    {
        [Key]
        public int YouTubeMetaDataId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class SoundCloudMetaData
    {
        [Key]
        public int SoundCloudMetaDataId { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public virtual ICollection<Category> GenreCategories { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public int TagId { get; set; }
        public string TagText { get; set; }
        public string ApiName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Keep> Keeps { get; set; }
        [JsonIgnore]
        public virtual ICollection<SoundCloudMetaData> SoundCloudMetaData { get; set; }
        [JsonIgnore]
        public virtual ICollection<PlacesMetaData> PlacesMetaData { get; set; }
    }
}
