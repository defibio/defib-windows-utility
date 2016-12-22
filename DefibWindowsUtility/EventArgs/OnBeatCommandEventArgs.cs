namespace DefibWindowsUtility.EventArgs
{
    public class OnBeatCommandEventArgs : System.EventArgs
    {
        public string Invoked;
        public string Alias;

        public OnBeatCommandEventArgs(string invoked, string alias)
        {
            this.Invoked = invoked;
            this.Alias = alias;
        }
    }
}