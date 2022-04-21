namespace ZealEducationManager.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Exam")]
    public partial class Exam
    {
        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(200)]
        public string QuestionList { get; set; }

        [StringLength(200)]
        public string AnswerList { get; set; }

        public long? ProductId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public short? TotalScore { get; set; }

        public short? Time { get; set; }

        public short? TotalQuestion { get; set; }

        //study vs exam
        [StringLength(1)]
        public string Type { get; set; }

        public bool? Status { get; set; }

        public string QuestionEssay { get; set; }

        //which users can join this exam?
        [StringLength(3000)]
        public string UserList { get; set; }

        [StringLength(200)]
        public string ScoreList { get; set; }
    }
}
