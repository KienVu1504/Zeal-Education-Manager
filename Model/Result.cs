namespace ZealEducationManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Result")]
    public partial class Result
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ExamId { get; set; }

        [StringLength(300)]
        public string ResultQuiz { get; set; }

        public string ResultEssay { get; set; }

        [StringLength(300)]
        public string StartDateQuiz { get; set; }

        [StringLength(20)]
        public string StartTimeQuiz { get; set; }

        [StringLength(20)]
        public string FinishTimeQuiz { get; set; }

        public DateTime? StartDateEssay { get; set; }

        [StringLength(20)]
        public string StartTimeEssay { get; set; }

        [StringLength(20)]
        public string FinishTimeEssay { get; set; }

        public bool? Status { get; set; }

        [StringLength(10)]
        public string Score { get; set; }
    }
}
