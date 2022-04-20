namespace ZealEducationManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public long Id { get; set; }

        [StringLength(3000)]
        public string Name { get; set; }

        public string Content { get; set; }

        [StringLength(4000)]
        public string Answer { get; set; }

        [StringLength(1)]
        public string Type { get; set; }

        public long? ProductId { get; set; }

        public bool? Status { get; set; }
    }
}
