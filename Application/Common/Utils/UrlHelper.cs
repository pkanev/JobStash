using JobStash.Application.Common.Exceptions;
using JobStash.Application.Common.Interfaces;

namespace JobStash.Application.Common.Utils;

public class UrlHelper : IUrlHelper
{
    public Uri GetUri(string uri)
    {
        try
        {
            return new Uri(uri);
        }
        catch (FormatException)
        {
            throw new InvalidUrlException($"Invalid url: \"{uri}\"");
        }
    }
}
