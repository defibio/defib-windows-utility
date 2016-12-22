using System;
using DefibWindowsUtility.EventArgs;

namespace DefibWindowsUtility
{
    public delegate void HelpCommandHandler(object sender, OnHelpCommandEventArgs arguments);
    public delegate void AddCommandHandler(object sender, OnAddCommandEventArgs arguments);
    public delegate void DeleteCommandHandler(object sender, OnDeleteCommandEventArgs arguments);
    public delegate void UpdateCommandHandler(object sender, OnUpdateCommandEventArgs arguments);
    public delegate void BeatCommandHandler(object sender, OnBeatCommandEventArgs arguments);

    public class Delegator
    {
        public event HelpCommandHandler OnHelpCommand;
        public event AddCommandHandler OnAddCommand;
        public event DeleteCommandHandler OnDeleteCommand;
        public event UpdateCommandHandler OnUpdateCommand;
        public event BeatCommandHandler OnBeatCommand; 

        public virtual void HelpCommand(OnHelpCommandEventArgs arguments)
        {
            OnHelpCommand(this, arguments);
        }

        public virtual void AddCommand(OnAddCommandEventArgs arguments)
        {
            OnAddCommand(this, arguments);
        }

        public virtual void DeleteCommand(OnDeleteCommandEventArgs arguments)
        {
            OnDeleteCommand(this, arguments);
        }

        public virtual void UpdateCommand(OnUpdateCommandEventArgs arguments)
        {
            OnUpdateCommand(this, arguments);
        }

        public virtual void BeatCommand(OnBeatCommandEventArgs arguments)
        {
            OnBeatCommand(this, arguments);
        }

        public void Delegate(string command, string[] arguments)
        {
            string Timestamp = ((int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();

            switch (command)
            {
                case "help":
                    HelpCommand(new OnHelpCommandEventArgs(Timestamp));
                    break;
                case "add":
                    AddCommand(new OnAddCommandEventArgs(Timestamp, arguments[0], arguments[1]));
                    break;
                case "delete":
                    DeleteCommand(new OnDeleteCommandEventArgs(Timestamp, arguments[0]));
                    break;
                case "update":
                    UpdateCommand(new OnUpdateCommandEventArgs(Timestamp, arguments[0], arguments[1]));
                    break;
                case "beat":
                    BeatCommand(new OnBeatCommandEventArgs(Timestamp, arguments[0]));
                    break;
                default:
                    HelpCommand(new OnHelpCommandEventArgs(Timestamp));
                    break;
            }
        }
    }
}
