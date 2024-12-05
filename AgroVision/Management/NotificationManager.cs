using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroVision
{
    public class NotificationManager
    {

        public static event EventHandler<NewNotificationEventArgs> NewNotification;

        //Sucesso
        public static void Success(string title, string description)
        {
            HandleNewNotification(title, description, 0);
        }

        //Informação
        public static void Info(string title, string description)
        {
            HandleNewNotification(title, description, 1);
        }

        //Aviso
        public static void Warn(string title, string description)
        {
            HandleNewNotification(title, description, 2);
        }

        //Erro
        public static void Error(string title, string description)
        {
            HandleNewNotification(title, description, 3);
        }

        //Limpar
        public static void Clear()
        {
            HandleNewNotification("", "", -1);
        }

        private static void HandleNewNotification(string title, string description, int type)
        {
            NewNotification?.Invoke(null, new NewNotificationEventArgs() { Title = title, Description = description, Type = type });
        }

    }
}
