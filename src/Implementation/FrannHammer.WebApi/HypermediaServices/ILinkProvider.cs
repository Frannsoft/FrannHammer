using System;
using FrannHammer.WebApi.Models;

namespace FrannHammer.WebApi.HypermediaServices
{
    public interface ILinkProvider
    {
        T CreateLink<T>(string href) where T : Link;
    }

    public class LinkProvider : ILinkProvider
    {
        public T CreateLink<T>(string href) where T : Link
        {
            return (T)Activator.CreateInstance(typeof(T), href);
        }
    }
}