﻿using System;

namespace FrannHammer.Domain.Contracts
{
    public interface ICharacter : IModel
    {
        string FullUrl { get; set; }
        string Style { get; set; }
        string MainImageUrl { get; set; }
        string ThumbnailUrl { get; set; }
        string Description { get; set; }
        string ColorTheme { get; set; }
        string DisplayName { get; set; }
    }
}
