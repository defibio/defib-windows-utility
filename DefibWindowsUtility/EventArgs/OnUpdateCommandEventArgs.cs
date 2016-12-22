namespace DefibWindowsUtility.EventArgs
{
    public class OnUpdateCommandEventArgs : System.EventArgs
    {
        public string Invoked;
        public string Alias;
        public string Key;

        public OnUpdateCommandEventArgs(string invoked, string alias, string key)
        {
            this.Invoked = invoked;
            this.Alias = alias;
            this.Key = key;
        }
    }
}