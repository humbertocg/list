using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using AndroidX.Core.App;
using AndroidX.Core.Content.PM;
using Firebase.Messaging;
using Newtonsoft.Json;

namespace list.Droid.Services
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingService : Firebase.Messaging.FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);
            Console.WriteLine($"Token received: {token}");
            SendRegistrationToServer(token);
        }

        /// <summary>
        /// formato esperado {"data":{"message":{"Notification":{"Title":"titulo", "Body":"cuerpo"}}}}
        /// </summary>
        /// <param name="message">notificacion</param>
        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);
            var nativeNotification = message.GetNotification();
            //if (App.PushNotifications == null)
            {
            //    App.PushNotifications = new PushNotifications();
            }
            //var pushNotifications = App.PushNotifications;
            if (message.Data.ContainsKey(nameof(Message).ToLower()))
            {

                try
                {
                    var notificacionString = message.Data[nameof(Message).ToLower()];
                    var notificacionObject = JsonConvert.DeserializeObject<Message>(notificacionString);
                    Log.Debug(TAG, $"{notificacionObject.Notification.Title}: {notificacionObject.Notification.Message}");
                    var notificacion = new list.Models.Notifications.Notification
                    {
                        Message = notificacionObject.Notification.Message,
                        Title = notificacionObject.Notification.Title
                    };
                    //pushNotifications.NotificationIncomingAction(notificacion);
                    SendNotification(new list.Models.Notifications.Notification
                    {
                        Title = notificacionObject.Notification.Title,
                        Message = notificacionObject.Notification.Message
                    });
                }
                catch (Exception ex)
                {
                    Log.Debug(TAG, "Error push: " + ex.Message);
                }
            }
        }

        void SendNotification(list.Models.Notifications.Notification notification)
        {

            try
            {
                var intent = new Android.Content.Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                PendingIntent pendingIntent;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.S)
                {
                    pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable);
                }
                else
                {
                    pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);
                }
                var notificationBuilder = new AndroidX.Core.App.NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                                            .SetSmallIcon(Resource.Mipmap.icon)
                                                            .SetContentTitle(notification.Title)
                                                            .SetContentText(notification.Message)
                                                            .SetAutoCancel(true)
                                                            .SetContentIntent(pendingIntent);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    notificationBuilder = notificationBuilder.SetLocusId(new AndroidX.Core.Content.LocusIdCompat("11"))
                                        /*.SetBubbleMetadata(
                                            new AndroidX.Core.App.NotificationCompat.BubbleMetadata.Builder()
                                                    .SetDesiredHeight(600)
                                                    .SetIntent(pendingIntent)
                                                    .SetAutoExpandBubble(true)
                                                    .SetSuppressNotification(true)
                                                    .SetIcon(AndroidX.Core.Graphics.Drawable.IconCompat.CreateFromIcon(this, Android.Graphics.Drawables.Icon.CreateWithResource( this, Resource.Mipmap.icon)))
                                                    .Build()
                                            )*/
                                        .SetShortcutInfo(
                                        new ShortcutInfoCompat.Builder(this, MainActivity.NOTIFICATION_ID.ToString())
                                                //.SetActivity(new ComponentName(this, MainActivity.class))
                                                .SetCategories(new List<string>() { Notification.CategoryMessage }/*new HashSet(Collections.SingletonList(Notification.CategoryMessage))*/)
                                                .SetIcon(AndroidX.Core.Graphics.Drawable.IconCompat.CreateFromIcon(this, Android.Graphics.Drawables.Icon.CreateWithResource(this, Resource.Mipmap.icon)))
                                                .SetPerson(new AndroidX.Core.App.Person.Builder()
                                                    .SetIcon(AndroidX.Core.Graphics.Drawable.IconCompat.CreateFromIcon(this, Android.Graphics.Drawables.Icon.CreateWithResource(this, Resource.Mipmap.icon)))
                                                    .SetName(notification.Title)
                                                    .Build()
                                                )
                                                .SetRank(0)
                                                .SetLongLived(true)
                                                .SetShortLabel(notification.Title)
                                                .SetIntent(intent)
                                                .Build());
                }
                var notificationManager = NotificationManagerCompat.From(this);
                notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
            }
            catch (Exception ex)
            {
                Log.Debug("TAG", "Error sending push: " + ex.Message);
            }
        }

        private void SendRegistrationToServer(string refreshToken)
        {
            try
            {
                //capture token
                //var pushNotifications = App.PushNotifications;
                //pushNotifications.RegistrationCompletedAction(refreshToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class Message
        {
            public list.Models.Notifications.Notification Notification { get; set; }
        }
    }
}