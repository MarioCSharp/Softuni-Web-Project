﻿using System.ComponentModel.DataAnnotations;

namespace BetterShkolo.Models.Activity
{
    public class ActivityAddModel
    {
        [Required]
        [Display(Name = "Име на събитие")]
        public string Name { get; set; } = null!;
        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Required]
        [Display(Name = "Присъствие")]
        public string Presence { get; set; } = null!;
        [Required]
        [Display(Name = "Времеви диапазон")]
        public string Time { get; set; } = null!;
        [Required]
        [Display(Name = "Местоположение")]
        public string Location { get; set; } = null!;
        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; } = null!;

        public int SchoolId { get; set; }
    }
}
