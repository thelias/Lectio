using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.EC2.Model;

namespace WeKeepsService.Entities
{
    public class QueryResult
    {
        public int QueryResultId { get; set; }
        [ForeignKey("Keep")]
        public int? KeepId { get; set; }
        public virtual Keep Keep { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string QueryResults { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? Timestamp { get; set; }
    }
}
