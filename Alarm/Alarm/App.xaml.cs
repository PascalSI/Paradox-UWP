using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Alarm
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;
            Debug.WriteLine("app launched");

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(ctrlAllarme), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }



        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
        public static string formatMessage(byte[] newbytes)
        {
            int checksum = 0;
            for (int i = 16; i < newbytes.Length - 2; i++) { checksum += newbytes[i]; }
            while (checksum > 255) { checksum = checksum - (checksum / 256) * 256; }
            newbytes[newbytes.Length - 1] = Convert.ToByte(checksum);
            string newByteString = "";
            foreach (byte item in newbytes) { newByteString += String.Format("{0:X2}", item); }
            return newByteString;
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string createLoginString (string password)
        {
            //login string format:
            // \xaa + \x[pass length] + \x0003[08=no encription;09=encrypt]f0000aeeeeeeeeeeeeeeee + \x[password chars] + padding \xee to 32bytes

            string LoginString = "";
            byte[] loginArray = new byte[31];
            loginArray[0] = Convert.ToByte(0xaa);
            loginArray[1] = Convert.ToByte(password.Length);
            byte[] fixedPart = StringToByteArray("000308f0000aeeeeeeeeeeeeeeee");
            int i = 0;
            foreach (byte item in fixedPart)
            {
                loginArray[i + 2] = fixedPart[i];
                i++;
            }
            foreach (char letter in password)
            {
                loginArray[i+2] = Convert.ToByte(letter);
                i++;
            }
            while ( i < 29)
            {
                loginArray[i+2] = StringToByteArray("ee")[0];
                i++;
            }
            foreach (byte item in loginArray)
            {
                LoginString += String.Format("{0:X2}", item);
            }
            return LoginString;
        }

        public static string retrieveZoneLabel (int label, byte[] response)
        {
            int start = 0;
            int stop = 0;
            if (label == 1) { start = 20; stop = 36; } else if (label == 2) { start = 36; stop = 52; }
            string sensorLbl = "";
            for (int i = start; i < stop; i++)
            {
                sensorLbl += Convert.ToChar(response[i]);
            }
            return sensorLbl;
        }
        
    }
}
