using System;
using System.ComponentModel;
using System.Timers;

namespace HelloWorld.Ui.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _message;
        private Timer _timer;

        public MainViewModel()
        {
            Message = "Hello, World!";
            StartTimer();
        }
        
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        private void StartTimer()
        {
            _timer = new Timer(2000); // set interval to 2 seconds
            _timer.Elapsed += UpdateMessage;
            _timer.Start();
        }

        private async void UpdateMessage(object sender, ElapsedEventArgs e)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    Message = Guid.NewGuid().ToString();
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}