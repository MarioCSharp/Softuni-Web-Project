﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BetterShkolo.Data.Models
{
    public class Consultation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int SubjectId { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; } = null!;
        [Required]
        public int Term { get; set; }
        [Required]
        public double Value { get; set; }
        [Required]
        public string Type { get; set; } = null!;
        [Required]
        public int GradeId { get; set; }
        [ForeignKey(nameof(GradeId))]
        public Grade Grade { get; set; } = null!;
        [Required]
        public string UserId { get; set; } = null!;
    }
}
