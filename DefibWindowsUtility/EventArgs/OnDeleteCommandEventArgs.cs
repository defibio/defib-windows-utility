namespace DefibWindowsUtility.EventArgs
{
    public class OnDeleteCommandEventArgs : System.EventArgs
    {
        public string Invoked;
        public string Alias;

        public OnDeleteCommandEventArgs(string invoked, string alias)
        {
            this.Invoked = invoked;
            this.Alias = alias;
        }
    }
}