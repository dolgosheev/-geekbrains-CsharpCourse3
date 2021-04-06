using System.Collections.Generic;
using System.Windows;

namespace Lesson1
{
    public partial class MainWindow
    {
        public Dictionary<string,string> Receivers { get; }

        private string _address;
        private string _name;


        public MainWindow()
        {
            Receivers = Database.Dictionary;
            InitializeComponent();
            DataContext = this;

        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            RtbMessage.Document.Blocks.Clear();
        }

        private void cbReceivers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedUserItem = e.AddedItems[0] as KeyValuePair<string, string>? ?? default;
            _address = selectedUserItem.Value;
            _name = selectedUserItem.Key;
            LReceiver.Content = _address;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            var receiveMail = _address;
            if (string.IsNullOrWhiteSpace(receiveMail))
            {
                MessageBox.Show("Have no Address", "Error");
                return;
            }

            var receiveName = _name;
            if (string.IsNullOrWhiteSpace(receiveName))
            {
                MessageBox.Show("Have no Name", "Error");
                return;
            }

            var receiveSubject = TbSubject.Text;
            if (string.IsNullOrWhiteSpace(receiveSubject))
            {
                MessageBox.Show("Have no Subject", "Error");
                return;
            }

            RtbMessage.SelectAll();
            var receiveMessage = RtbMessage.Selection.Text;
            if (string.IsNullOrWhiteSpace(receiveMessage))
            {
                MessageBox.Show("Have no Message", "Error");
                return;
            }

            var preparedEvent = new MailSender(receiveMail, receiveName, receiveSubject, receiveMessage);

            var state = preparedEvent.Send();

            if (state)
                MessageBox.Show("Success!", "[Message Sended]");
            else
                MessageBox.Show("Fail!", "[Message Not sended]");

        }
    }
}
