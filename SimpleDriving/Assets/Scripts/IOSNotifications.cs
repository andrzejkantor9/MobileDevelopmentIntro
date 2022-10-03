using System;

using UnityEngine;

#if UNITY_IOS
using Unity.Notifications.iOS;
#endif

namespace SimpleDriving
{
    public class IOSNotifications : MonoBehaviour
    {
#if UNITY_IOS
        #region Config
        //[Header("CONFIG")]
        #endregion

        #region Cache
        //[Header("CACHE")]
    	//[Space(8f)]
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
        public void ScheduleNotification(int minutes)
        {
            iOSNotification notification = new iOSNotification
            {
                Title = "Energy Recharged!",
                Subtitle = "Your energy has been recharged",
                Body = "Your energy has recharged, come back to play again!",
                ShowInForeground = true,
                ForegroundPresentationOption = (PresentationOption.Alert || PresentationOption.Sound),
                CategoryIdentifier = "categor_a,"
                ThreadIdentifier = "thread1",
                Trigger = new iOSNotificationTimeIntervalTrigger
                {
                    TimeInterval = new System.TimeSpan(0, minutes, 0),
                    Repeats = false
                }
            };

            iOSNotificationCenter.ScheduleNotification(notification);
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
