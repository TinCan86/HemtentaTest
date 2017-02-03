namespace HemtentaTdd2017.music
{
    public class SoundMaker : ISoundMaker
    {
        ISong _songPlaying;

        public string NowPlaying
        {
            get
            {
                //Gör en if sats istället. TODO
                return _songPlaying == null ? "" : _songPlaying.Title;
            }
        }

        public void Play(ISong song)
        {
            _songPlaying = song;
        }

        public void Stop()
        {
            _songPlaying = null;
        }
    }
}
