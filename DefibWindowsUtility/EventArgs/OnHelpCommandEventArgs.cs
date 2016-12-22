namespace DefibWindowsUtility.EventArgs
{
    public class OnHelpCommandEventArgs : System.EventArgs
    {
        public string Invoked;

        public OnHelpCommandEventArgs(string invoked)
        {
            this.Invoked = invoked;
        }
    }
}