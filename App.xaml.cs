using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using InvisWork.InstanceManager;
using InvisWork.UserProfile;
using InvisWork.win;

namespace InvisWork
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            new _mediator();
            Welcome(true);
        }

        private void Welcome(bool devmode=false)
        {
            if(devmode)
            {
                UserSession.Terminate(false); 
                Instance.Create<Main>();
                return;
            }
            UserSession.AuthenticateUser();
            switch (MessageBox.Show($"Detected Windows Login ({UserSession.CurrentUser.UserName})\n\nUse these credentials?", $"Hello {UserSession.CurrentUser.DisplayName}", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes: Instance.Create<Main>(); break;
                case MessageBoxResult.No: UserSession.Terminate(false); Instance.Create<Main>(); break;
                default: Instance.Create<Main>(); break;
            }

        }
    }
}


