﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using UIKit;
using UserNotifications;

namespace list.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            RequestPushPermissionAsync();
            //Manage internally the deletetion for token
            //var pushNotifications = App.PushNotifications;
            //pushNotifications.UnRegistration = UnRegisteredForRemoteNotifications;

            return base.FinishedLaunching(app, options);
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            try
            {
                //var pushNotifications = App.PushNotifications;
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    byte[] result = new byte[deviceToken.Length];
                    Marshal.Copy(deviceToken.Bytes, result, 0, (int)deviceToken.Length);
                    var token = BitConverter.ToString(result).Replace("-", "");
                     System.Diagnostics.Debug.WriteLine("Token extracted " + token);
                     System.Diagnostics.Debug.WriteLine("Token Description " + deviceToken.Description);
                    //pushNotifications.RegistrationCompletedAction(token);
                }
                else
                {
                    var token = deviceToken.Description.Trim('<', '>').Replace(" ", string.Empty).ToUpperInvariant();
                     System.Diagnostics.Debug.WriteLine("Token " + token);
                    //pushNotifications.RegistrationCompletedAction(token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            ProcessNotification(userInfo, false);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine($"Failed to register for remote notifications: {error.Description}");
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // This will be called if the app is in the background/not running and if in the foreground.
            // However, it will not display a notification visually if the app is in the foreground.

            PresentNotification(userInfo);

            completionHandler(UIBackgroundFetchResult.NoData);
        }

        public bool RequestPushPermissionAsync()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Sound | UNAuthorizationOptions.Sound,
                                                                        (granted, error) =>
                                                                        {
                                                                            if (granted)
                                                                            {
                                                                                InvokeOnMainThread(UIApplication.SharedApplication.RegisterForRemoteNotifications);

                                                                            }
                                                                        });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                        UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                        new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }

            return true;
        }

        public void UnRegisteredForRemoteNotifications(string args)
        {
            UIApplication.SharedApplication.UnregisterForRemoteNotifications();
            RequestPushPermissionAsync();
        }

        void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
        {
            // Check to see if the dictionary has the aps key.  This is the notification payload you would have sent
            if (null != options && options.ContainsKey(new NSString("aps")))
            {
                //Get the aps dictionary
                NSDictionary aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;

                string alert = string.Empty;

                //Extract the alert text
                // NOTE: If you're using the simple alert by just specifying
                // "  aps:{alert:"alert msg here"}  ", this will work fine.
                // But if you're using a complex alert with Localization keys, etc.,
                // your "alert" object from the aps dictionary will be another NSDictionary.
                // Basically the JSON gets dumped right into a NSDictionary,
                // so keep that in mind.
                if (aps.ContainsKey(new NSString("alert")))
                    alert = (aps[new NSString("alert")] as NSString).ToString();

                //If this came from the ReceivedRemoteNotification while the app was running,
                // we of course need to manually process things like the sound, badge, and alert.
                if (!fromFinishedLaunching)
                {
                    //Manually show an alert
                    if (!string.IsNullOrEmpty(alert))
                    {
                        UIAlertView avAlert = new UIAlertView("Notification", alert, null, "OK", null);
                        avAlert.Show();

                        //Receive the message on xamarin forms
                        /*var pushNotifications = App.PushNotifications;
                        var notificacion = new Notificacion
                        {
                            Mensaje = alert,
                            Titulo = "Cancelapp"
                        };

                        pushNotifications.NotificationIncomingAction(notificacion);
                        */
                    }
                }
            }
        }

        void PresentNotification(NSDictionary dict)
        {
            // Extract some data from the notifiation and display it using an alert view.
            NSDictionary aps = dict.ObjectForKey(new NSString("aps")) as NSDictionary;

            var msg = string.Empty;
            if (aps.ContainsKey(new NSString("alert")))
            {
                msg = (aps[new NSString("alert")] as NSString).ToString();
            }

            if (string.IsNullOrEmpty(msg))
            {
                msg = "(unable to parse)";
            }

            //Receive the message on xamarin forms
            /*var pushNotifications = App.PushNotifications;
            var notificacion = new Notificacion
            {
                Mensaje = msg,
                Titulo = "Paquetito"
            };

            pushNotifications.NotificationIncomingAction(notificacion);
            */

        }

    }
}

