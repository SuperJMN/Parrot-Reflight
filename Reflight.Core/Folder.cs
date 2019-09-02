namespace Reflight.Core
{
    public class Folder
    {
        public Folder(string token, string path)
        {
            Token = token;
            Path = path;
        }

        public string Token { get; }
        public string Path { get; }
    }
}