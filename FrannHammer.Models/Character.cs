using System;

namespace FrannHammer.Models
{
    public class BaseCharacterModel
    {
        protected bool Equals(BaseCharacterModel other)
        {
            return string.Equals(Style, other.Style) && string.Equals(MainImageUrl, other.MainImageUrl) && string.Equals(ThumbnailUrl, other.ThumbnailUrl) && string.Equals(Description, other.Description) && string.Equals(ColorTheme, other.ColorTheme) && string.Equals(Name, other.Name) && string.Equals(DisplayName, other.DisplayName) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as BaseCharacterModel;
            return other != null && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Style != null ? Style.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (MainImageUrl != null ? MainImageUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ThumbnailUrl != null ? ThumbnailUrl.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (ColorTheme != null ? ColorTheme.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Id;
                return hashCode;
            }
        }

        public static bool operator ==(BaseCharacterModel left, BaseCharacterModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BaseCharacterModel left, BaseCharacterModel right)
        {
            return !Equals(left, right);
        }

        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Id { get; set; }
    }

    public class CharacterDto : BaseCharacterModel
    {
        //public string Style { get; set; }
        //public string MainImageUrl { get; set; }
        //public string ThumbnailUrl { get; set; }
        //public string Description { get; set; }
        //public string ColorTheme { get; set; }
        //public string Name { get; set; }
        //public string DisplayName { get; set; }
        //public int Id { get; set; }
    }

    public class Character : BaseCharacterModel// : BaseModel
    {
        //public string Style { get; set; }
        //public string MainImageUrl { get; set; }
        //public string ThumbnailUrl { get; set; }
        //public string Description { get; set; }
        //public string ColorTheme { get; set; }
        //public string Name { get; set; }
        //public string DisplayName { get; set; }
        //public int Id { get; set; }
        public DateTime LastModified { get; set; }

        //protected bool Equals(Character other)
        //{
        //    return string.Equals(Style, other.Style) && 
        //        string.Equals(MainImageUrl, other.MainImageUrl) && 
        //        string.Equals(ThumbnailUrl, other.ThumbnailUrl) && 
        //        string.Equals(Description, other.Description) && 
        //        string.Equals(ColorTheme, other.ColorTheme) &&
        //        string.Equals(DisplayName, other.DisplayName) &&
        //        string.Equals(Name, other.Name) && 
        //        Id == other.Id && 
        //        LastModified.Equals(other.LastModified);
        //}

        //public override async Task<HttpResponseMessage> Create(HttpClient client)
        //{
        //    var httpResponseMessage = await client.PostAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characters", this);
        //    return httpResponseMessage;
        //}

        //public override async Task<HttpResponseMessage> Update(HttpClient client)
        //{
        //    var httpResponseMessage = await client.PutAsJsonAsync($"{client.BaseAddress.AbsoluteUri}/characters/{Id}", this);
        //    return httpResponseMessage;
        //}

        //public override async Task<HttpResponseMessage> Delete(HttpClient client)
        //{
        //    return await client.DeleteAsync($"{client.BaseAddress.AbsoluteUri}/characters/{Id}");
        //}

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;
        //    if (obj.GetType() != GetType()) return false;
        //    return Equals((Character)obj);
        //}

        //[SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        var hashCode = Style?.GetHashCode() ?? 0;
        //        hashCode = (hashCode * 397) ^ (MainImageUrl?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (ThumbnailUrl?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (ColorTheme?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ (DisplayName?.GetHashCode() ?? 0);
        //        hashCode = (hashCode * 397) ^ Id;
        //        hashCode = (hashCode * 397) ^ LastModified.GetHashCode();
        //        return hashCode;
        //    }
        //}

        //public static bool operator ==(Character left, Character right)
        //{
        //    return Equals(left, right);
        //}

        //public static bool operator !=(Character left, Character right)
        //{
        //    return !Equals(left, right);
        //}

        //public static implicit operator CharacterDto(Character character)
        //{
        //    return new CharacterDto
        //    {
        //        ColorTheme = character.ColorTheme,
        //        Description = character.Description,
        //        DisplayName = character.DisplayName,
        //        Id = character.Id,
        //        MainImageUrl = character.MainImageUrl,
        //        Name = character.Name,
        //        Style = character.Style,
        //        ThumbnailUrl = character.ThumbnailUrl
        //    };
        //}
    }
}