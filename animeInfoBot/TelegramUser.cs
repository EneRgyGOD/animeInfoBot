using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace animeInfoBot
{
    public class TelegramUser : INotifyPropertyChanged, IEquatable<TelegramUser>
    {
        public TelegramUser(string Nickname, long ChatId)
        {
            this.nick = Nickname;
            this.id = ChatId;
            Messages = new ObservableCollection<string>();
        }

        private string nick;

        public string Nick
        {
            get { return this.nick; }
            set
            {
                this.nick = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Nick)));
            }
        }

        private long id;

        public long Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.id)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Equals(TelegramUser other) => other.id == this.id;

        public ObservableCollection<string> Messages { get; set; }

        public void AddMessage(string Text) => Messages.Add(Text);
    }
}