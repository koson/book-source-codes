using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UnitOfWork.Core
{
    [Table("Projects")]
    public class Project
    {
        [Key]
        public Guid ProjectID { get; set; }
        [Required]
        public string ProjectName { get; set; }
    }
}
