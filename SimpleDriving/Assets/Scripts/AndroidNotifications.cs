using System;

using UnityEngine;

#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

namespace SimpleDriving
{
    public class AndroidNotifications : MonoBehaviour
    {
#if UNITY_ANDROID
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]

        private const string CHANNEL_ID = "notification_channel";
        #endregion

        #region States
        #endregion

        #region Events & Statics
        #endregion

        #region Data
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////

        #region EngineMethods & Contructors
        #endregion

        #region PublicMethods
        public void ScheduleNotification(DateTime dateTime)
        {
            AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel
            {
                Id = CHANNEL_ID,
                Name = "Notofication Channel",
                Description = "put description there",
                Importance = Importance.Default
            };

            AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

            AndroidNotification notification = new AndroidNotification
            {
                Title = "Energy Recharged!",
                Text = "Your energy has recharged, come back to play again!",
                SmallIcon = "default",
                LargeIcon = "default",
                FireTime = dateTime
            };

            AndroidNotificationCenter.SendNotification(notification, CHANNEL_ID);
        }
        #endregion

        #region Interfaces & Inheritance
        #endregion

        #region Events & Statics
        #endregion

        #region PrivateMethods
        #endregion
#endif
    }
}
