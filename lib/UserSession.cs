using System;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using Woof.SystemEx;

namespace InvisWork.UserProfile
{
    public static class UserSession
    {
        [Serializable]
        public static class CurrentUser
        {
            internal static string UserName { get; set; }
            internal static string DomainName { get; set; }
            internal static string DisplayName { get; set; }
            internal static string EmailAddress { get; set; }
            internal static string UserImagePath { get; set; }
            internal static DateTime? AuthenticationTime { get; set; }
            internal static TimeSpan? SessionLength { get; set; }
            internal static bool SessionOnline { get; set; }
        }
        public static void Terminate(bool CloseApp = true)
        {
            if (CurrentUser.AuthenticationTime != null) CurrentUser.SessionLength = DateTime.Now.Subtract((DateTime)CurrentUser.AuthenticationTime);
            //MessageBox.Show(               
            //    $"Username: {CurrentUser.UserName}\n" +
            //    $"Domain name: {CurrentUser.DomainName}\n" +
            //    $"Display name: {CurrentUser.DisplayName}\n" +
            //    $"Email Address: {CurrentUser.EmailAddress}\n" +
            //    $"Acc Image: {CurrentUser.UserImagePath}\n" +
            //    $"Session start time: {CurrentUser.AuthenticationTime}\n" +
            //    $"Session duration: {CurrentUser.SessionLength.Days} Days | {CurrentUser.SessionLength.Hours} Hours | {CurrentUser.SessionLength.Minutes} Min | {CurrentUser.SessionLength.Seconds} Sec",
            //    $"*UserSession Terminated*");
            if (CloseApp) Application.Current.Shutdown();
            CurrentUser.UserName = string.Empty;
            CurrentUser.DomainName = string.Empty;
            CurrentUser.DisplayName = "Sign In";
            CurrentUser.EmailAddress = string.Empty;
            CurrentUser.UserImagePath = "";
            CurrentUser.AuthenticationTime = null;
            CurrentUser.SessionLength = null;
            CurrentUser.SessionOnline = false;
        }
        public static bool AuthenticateUser()
        {
            AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                CurrentUser.UserName = Environment.UserName;
                CurrentUser.DomainName = Environment.UserDomainName;
                CurrentUser.DisplayName = System.DirectoryServices.AccountManagement.UserPrincipal.Current.DisplayName;
                CurrentUser.EmailAddress = System.DirectoryServices.AccountManagement.UserPrincipal.Current.EmailAddress;
                CurrentUser.UserImagePath = SysInfo.GetUserPicturePath();
                CurrentUser.AuthenticationTime = DateTime.Now;
                CurrentUser.SessionOnline = true;
                return true;
            }
            else return false;
        }
    }
}


