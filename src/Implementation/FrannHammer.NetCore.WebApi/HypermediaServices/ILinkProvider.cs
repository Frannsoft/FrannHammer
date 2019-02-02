using FrannHammer.NetCore.WebApi.Models;
using System;

namespace FrannHammer.NetCore.WebApi.HypermediaServices
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
