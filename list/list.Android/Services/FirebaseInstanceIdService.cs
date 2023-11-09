using System;
using Android.App;
using Firebase.Iid;
using Firebase.Messaging;

namespace Paquetito.Droid
{
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    //[IntentFilter(new[] { "om.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseInstanceIdService //: Firebase.Iid.FirebaseInstanceIdService
    {
        public /*override*/ void OnTokenRefresh()
        {
            var refreshToken = "";// FirebaseInstanceId.Instance.Token;
            Console.WriteLine($"Token received: {refreshToken}");
            SendRegistrationToServer(refreshToken);
        }

        private void SendRegistrationToServer(string refreshToken)
        {
            try
            {
                /*var pushNotifications = App.PushNotifications;
                pushNotifications.RegistrationCompletedAction(refreshToken);*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
