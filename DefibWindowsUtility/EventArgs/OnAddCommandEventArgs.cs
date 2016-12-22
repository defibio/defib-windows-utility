namespace DefibWindowsUtility.EventArgs
{
    public class OnAddCommandEventArgs : System.EventArgs
    {
        public string Invoked;
        public string Alias;
        public string Key;

        public OnAddCommandEventArgs(string invoked, string alias, string key)
        {
            this.Invoked = invoked;
            this.Alias = alias;
            this.Key = key;
        }
    }
}