using System;
using System.Diagnostics.CodeAnalysis;

namespace KuroganeHammer.Data.Api.Models
{
    public class Character
    {
        public string Style { get; set; }
        public string MainImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Description { get; set; }
        public string ColorTheme { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public DateTime LastModified { get; set; }

        protected bool Equals(Character other)
        {
            return string.Equals(Style, other.Style) && 
                string.Equals(MainImageUrl, other.MainImageUrl) && 
                string.Equals(ThumbnailUrl, other.ThumbnailUrl) && 
                string.Equals(Description, other.Description) && 
                string.Equals(ColorTheme, other.ColorTheme) && 
                string.Equals(Name, other.Name) && 
                Id == other.Id && 
                LastModified.Equals(other.LastModified);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Character)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Style?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (MainImageUrl?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ThumbnailUrl?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Description?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (ColorTheme?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ Id;
                hashCode = (hashCode * 397) ^ LastModified.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Character left, Character right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Character left, Character right)
        {
            return !Equals(left, right);
        }
    }
}