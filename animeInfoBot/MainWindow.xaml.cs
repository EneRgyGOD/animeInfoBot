using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot;

namespace animeInfoBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TelegramUser> Users;
        TelegramBotClient bot;
        Commands commands = new Commands();

        public MainWindow()
        {
            InitializeComponent();
           
            Users = new ObservableCollection<TelegramUser>();
            usersList.ItemsSource = Users;
            string text = "";

            string token = "1594638411:AAHKeSvo20d6Zc8llydOVZQ8Tr8jznl7p-M";
            bot = new TelegramBotClient(token);

            bot.OnMessage += delegate (object sender, Telegram.Bot.Args.MessageEventArgs e)
            {
                string msg = $"{DateTime.Now}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

                File.AppendAllText("data.log", $"{msg}/n");

                this.Dispatcher.Invoke(() =>
                {
                    var person = new TelegramUser(e.Message.Chat.FirstName, e.Message.Chat.Id);
                    if (!Users.Contains(person)) Users.Add(person);
                    Users[Users.IndexOf(person)].AddMessage($"{person.Nick}: {e.Message.Text}");
                    text = commands.CommandChooser(e.Message.Text);
                    bot.SendTextMessageAsync(e.Message.Chat.Id, text);
                    
                    if (concreteUserChat.Items.Count != 0)
                    {
                        concreteUserChat.SelectedIndex = concreteUserChat.Items.Count - 1;
                        concreteUserChat.ScrollIntoView(concreteUserChat.SelectedItem);
                    }
                });   
            };
                bot.StartReceiving();

                btnSendMsg.Click += delegate { SendMsg(text); };
                txtBxSendMsg.KeyDown += (s, e) => { if (e.Key == Key.Return) { SendMsg(text); } };
        }

        public void SendMsg(string text) 
        {
            var concreteUser = Users[Users.IndexOf(usersList.SelectedItem as TelegramUser)];
            string responseMsg = $"AnimeBot: {txtBxSendMsg.Text}";
            concreteUser.Messages.Add(responseMsg);

            bot.SendTextMessageAsync(concreteUser.Id, txtBxSendMsg.Text);
            string logText = $"{DateTime.Now}: >> {concreteUser.Id} {concreteUser.Nick} {responseMsg}\n";
            File.AppendAllText("data.log", logText);

            txtBxSendMsg.Text = String.Empty;
            concreteUserChat.SelectedIndex = concreteUserChat.Items.Count;
            concreteUserChat.ScrollIntoView(concreteUserChat.SelectedItem);
        }
    }
}
