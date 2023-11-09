using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Gms.Common;
using Android.Widget;
using Firebase.Messaging;
using Android.Content;
using Android.Nfc;
using Android.Util;
using AndroidX.Core.App;
using AndroidX.Media.App;
using Android;
using AndroidX.Core.Content.PM;
using static Xamarin.Essentials.Platform;
using System.Runtime.Remoting.Contexts;
using System.Collections.Generic;
using Java.Util;

namespace list.Droid
{
    [Activity(Label = "list", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly int NOTIFICATION_ID = 100;
        internal static readonly string CHANNEL_ID = "notification_channel";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            CreateNotificationChannel();
            CheckForGoogleServices();

            FirebaseMessaging.Instance.SubscribeToTopic("com.humberto.linkedlist.list");

            //Manage internally the deletetion for token
            //var pushNotifications = App.PushNotifications;
            //pushNotifications.UnRegistration = UnRegisteredForRemoteNotifications;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void UnRegisteredForRemoteNotifications()
        {
            FirebaseMessaging.Instance.UnsubscribeFromTopic("com.humberto.linkedlist.list");
            //FirebaseInstanceId.Instance.DeleteInstanceId(); //Deprecated
            FirebaseMessaging.Instance.DeleteToken();
        }

        public bool CheckForGoogleServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device does not support Google Play Services", ToastLength.Long);
                }
                return false;
            }
            return true;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }
            else
            {
                var channel = new NotificationChannel(CHANNEL_ID, "FCM Notifications", NotificationImportance.Default)
                {
                    Description = "Firebase Cloud Messages appear in this channel"
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            const int requestLocationId = 0;

            string[] notiPermission =
            {
                Manifest.Permission.PostNotifications
            };

            if (Build.VERSION.SdkInt < BuildVersionCodes.Tiramisu) return;

            if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
            {
                this.RequestPermissions(notiPermission, requestLocationId);
            }
        }

    }
}
