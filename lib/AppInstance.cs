using InvisWork;
using System.Windows;

namespace InvisWork.InstanceManager
{
    public static class Instance
    {
        public static Main Main { get; set; }

        public static void Create<T>() where T : Window, new()
        {
            if (typeof(T) == typeof(Main))
            {
                if (Main != null) Main.Close();
                Main = new T() as Main;
                Main.Show();
                return;
            }
        }
    }
}


