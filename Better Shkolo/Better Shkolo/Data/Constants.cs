﻿namespace BetterShkolo.Data
{
    public static class Constants
    {
        public class Grade
        {
            public const int GradeNameMaxLength = 3;
            public const int GradeNameMinLength = 2;
            public const int GradeSpecialtyMaxLength = 100;
            public const int GradeSpecialtyMinLength = 3;
        }

        public class Mark
        {
            public const int MarkMinValue = 2;
            public const int MarkMaxValue = 6;
        }

        public class Review
        {
            public const int DescriptionMaxLength = 100;
            public const int DescriptionMinLength = 10;
        }

        public class School
        {
            public const int NameMaxLength = 100;
            public const int NameMinLength = 10;
            public const int CityMaxLength = 15;
            public const int CityMinLength = 3;
        }

        public class Subject
        {
            public const int NameMaxLength = 300;
            public const int NameMinLength = 3;
        }

        public class User
        {
            public const int FirstNameMaxLength = 15;
            public const int FirstNameMinLength = 3;
            public const int LastNameMaxLength = 15;
            public const int LastNameMinLength = 3;
        }
    }
}
