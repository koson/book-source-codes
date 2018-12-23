using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UnitOfWork.Core
{
    [Table("TeamMembers")]
    public class TeamMember
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamMemberID { get; set; }
        [Required]
        public Guid ProjectID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
