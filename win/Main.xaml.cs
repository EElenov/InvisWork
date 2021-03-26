using InvisWork.Core;
using InvisWork.UserProfile;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace InvisWork
{
    public partial class Main : Window, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Memebers
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion
        public Main()
        {
            InitializeComponent();
            DataContext = this;
            UserDir = new RelayCommand(o => SignIn());
            TerminateApp = new RelayCommand(o => UserSession.Terminate());
            Topmost = true; 
        }
        private Brush _formColor = new SolidColorBrush(Colors.White);
        public Brush FormColor
        {
            get => _formColor;
            set
            {
                _formColor = value;
                OnPropertyChanged();
            }
        }
        private Brush _forecolor = new SolidColorBrush(Colors.Black);
        public Brush ForeColor
        {
            get => _forecolor;
            set
            {
                _forecolor = value;
                OnPropertyChanged();
            }
        }
        private bool _showColorForm = false;
        public bool ShowColorForm
        {
            get => _showColorForm;
            set
            {
                _showColorForm = value;
                OnPropertyChanged();
            }
        }
        public string Img { get => UserSession.CurrentUser.UserImagePath; set => OnPropertyChanged(); }
        public string DisplayName { get => UserSession.CurrentUser.DisplayName; set => OnPropertyChanged(); }
        public ICommand UserDir { get; set; }
        public ICommand TerminateApp { get; set; }

        private void SignIn()
        {
            if (UserSession.CurrentUser.SessionOnline) Process.Start(Environment.GetEnvironmentVariable("WINDIR") + @"\explorer.exe", Path.Combine(@"\\sv-fs-01.corrstyle.local\MOS_tree\Shared Data\Users_Folders\XML Programming\", UserSession.CurrentUser.UserName));
            else
            {
                UserSession.AuthenticateUser();
                DisplayName = UserSession.CurrentUser.DisplayName;
            }
        }
        private void OpenBarSettings(object sender, MouseButtonEventArgs e)
        {
            ShowColorForm = true;
        }
        private void Move(object sender, MouseButtonEventArgs e)
        {
            if (WindowState != System.Windows.WindowState.Maximized) DragMove();
            else WindowState = System.Windows.WindowState.Normal;
        }
        private void UpdateColor(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            var col = ConvertNullColor(sender);
            var dark = (col.R + col.G + col.B) / 3;
            ForeColor = dark < 100 ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
            FormColor = col != Colors.Transparent ? new SolidColorBrush(col) : new SolidColorBrush(Color.FromArgb(1, 255, 255, 255));
            FormColor.Opacity = col.A>0 ? col.A : 1;
        }
        private Color ConvertNullColor(object sender) => (sender as ColorPicker).SelectedColor != null ? (Color)(sender as ColorPicker).SelectedColor : Color.FromArgb(255, 255, 255, 255);
    }
}


