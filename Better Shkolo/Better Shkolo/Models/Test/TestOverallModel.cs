﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BetterShkolo.Models.Test
{
    public class TestOverallModel
    {
        public int Id { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime TestDate { get; set; }
        public string SubjectName { get; set; } = null!;
        public string TeacherFullName { get; set; } = null!;
        public int SubjectId { get; set; }
        public int Term { get; set; }
    }
}
