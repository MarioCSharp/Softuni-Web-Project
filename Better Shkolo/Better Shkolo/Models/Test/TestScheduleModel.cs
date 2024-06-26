﻿namespace BetterShkolo.Models.Test
{
    public class TestScheduleModel
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = null!;
        public int SubjectId { get; set; }
        public int Period { get; set; }
        public DateTime TestDate { get; set; }
        public string DateWeekDay { get; set; } = null!;
        public int DateWeekDayNumber { get; set; }
        public string TeacherName { get; set; } = null!;
        public int Week { get; set; }
    }
}
