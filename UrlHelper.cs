namespace SearchEngineScraper
{
    public static class UrlHelper
    {
        public static string NormalizeUrl(string inputUrl)
        {
            if (string.IsNullOrWhiteSpace(inputUrl))
                return string.Empty;

            if (!inputUrl.StartsWith("http://") && !inputUrl.StartsWith("https://"))
                inputUrl = "http://" + inputUrl;

            if (Uri.TryCreate(inputUrl, UriKind.Absolute, out var uri))
            {
                string host = uri.Host;

                if (host.StartsWith("www."))
                    host = host.Substring(4);

                return host;
            }

            return string.Empty;
        }
    }
}
