﻿using BetterShkolo.Data;
using BetterShkolo.Models.Teacher;
using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Grade
{
    public class GradeCreateModel
    {
        [Required]
        [StringLength(Constants.Grade.GradeNameMaxLength, MinimumLength = Constants.Grade.GradeNameMinLength)]
        [Display(Name = "Паралелка")]
        public string GradeName { get; set; } = null!;
        [Required]
        [StringLength(Constants.Grade.GradeSpecialtyMaxLength, MinimumLength = Constants.Grade.GradeSpecialtyMinLength)]
        [Display(Name = "Специалност")]
        public string GradeSpecialty { get; set; } = null!;
        [Required]
        [Display(Name = "Класен ръководител")]
        public int TeacherId { get; set; }
        [Required]
        public int SchoolId { get; set; }

        public List<TeacherDisplayModel> Teachers { get; set; }
    }
}
