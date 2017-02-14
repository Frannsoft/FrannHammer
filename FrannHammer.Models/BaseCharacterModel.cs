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
                var hashCode = Style?.GetHashCode() ?? 0;
                hashCode = (hashCode*397) ^ (MainImageUrl?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (ThumbnailUrl?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Description?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (ColorTheme?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode*397) ^ (DisplayName?.GetHashCode() ?? 0);
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

        public string FullUrl { get; set; }
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Id { get; set; }
    }
}