public class JsonParse
{
    public class Links
    {
        public string self { get; set; }
        public string git { get; set; }
        public string html { get; set; }
    }

    public class Root
    {
        public string name { get; set; }
        public string path { get; set; }
        public string sha { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string git_url { get; set; }
        public string download_url { get; set; }
        public string type { get; set; }
        public Links _links { get; set; }
    }

    public class DirLinks
    {
        public string self { get; set; }
        public string git { get; set; }
        public string html { get; set; }
    }

    public class DirRoot
    {
        public string name { get; set; }
        public string path { get; set; }
        public string sha { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string git_url { get; set; }
        public string download_url { get; set; }
        public string type { get; set; }
        public DirLinks _links { get; set; }
    }
}
