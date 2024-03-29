﻿namespace InfinityCinema.Data.Common
{
    public class DataValidation
    {
        public class MovieValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 200;

            public const int DescriptionMaxLength = 5000;

            public const int TrailerPathMaxLength = 2048;

            public const int DurationAsStringMaxLength = 50;
        }

        public class ActorValidation
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 60;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 60;

            public const int ImageUrlMaxLength = 2048;
            public const int InformationLinkMaxLength = 2048;

            public const int FullNameMinLength = FirstNameMinLength + LastNameMinLength;
            public const int FullNameMaxLength = FirstNameMaxLength + LastNameMaxLength;
        }

        public class CommentValidation
        {
            public const int ContentMaxLength = 600;
        }

        public class GenreValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;

            public const int ImageUrlMaxLength = 2048;

            public const int DescriptionMaxLength = 1000;
        }

        public class LanguageValidation
        {
            public const int NameMaxLength = 150;
        }

        public class PlatformValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;

            public const int PathUrlMaxLength = 2048;

            public const int IconUrlMaxLength = 2048;
        }

        public class ApplicationUserValidation
        {
            public const int FullNameMaxLength = 120;
        }

        public class ImageValidation
        {
            public const int UrlMaxLength = 2048;
        }

        public class DirectorValidation
        {
            public const int FirstNameMinLength = 2;
            public const int FirstNameMaxLength = 60;

            public const int LastNameMinLength = 2;
            public const int LastNameMaxLength = 60;

            public const int InformationUrlMaxLength = 2048;

            public const int FullNameMinLength = FirstNameMinLength + LastNameMinLength;
            public const int FullNameMaxLength = FirstNameMaxLength + LastNameMaxLength;
        }

        public class CountryValidation
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 60;

            public const int AbbreviationMaxLength = 10;
        }

        public class StarRatingValidation
        {
            public const int StarMaLength = 10;
        }

        public class ForumCategoryValidation
        {
            public const int TitleMaxLength = 100;
            public const int DescriptionMaxLength = 1000;
            public const int UrlMaxLength = 2048;
        }

        public class ForumPostValidation
        {
            public const int TitleMaxLength = 100;
            public const int ContentMaxLength = 10000;
        }

        public class ForumCommentValidation
        {
            public const int ContentMaxLength = 1500;
        }
    }
}
