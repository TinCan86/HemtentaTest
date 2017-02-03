namespace HemtentaTdd2017.music
{
    public class Song : ISong
    {
        string title;

        public Song(string title)
        {
            this.title = title;
        }
        public string Title
        {
            get
            {
                return title;
            }
        }
    }
}
