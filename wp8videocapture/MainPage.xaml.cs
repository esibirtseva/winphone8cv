using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.IO.IsolatedStorage;
using Microsoft.Devices;
using wp8videocapture.Resources;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace wp8videocapture
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Viewfinder for capturing video.
        private VideoBrush videoRecorderBrush;
        // Source and device for capturing video.
        private CaptureSource captureSource;
        private VideoCaptureDevice videoCaptureDevice;        

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Initialize the video recorder.
            InitializeVideoRecorder();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            // Dispose of camera and media objects.
            DisposeVideoPlayer();
            DisposeVideoRecorder();

            base.OnNavigatedFrom(e);
        }

        public void setText(string str) {
            txtDebug.Text = str;
        }
        public void InitializeVideoRecorder()
        {
            if (captureSource == null)
            {
                // Create the VideoRecorder objects.
                captureSource = new CaptureSource();
                videoCaptureDevice = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
                captureSource.VideoCaptureDevice = videoCaptureDevice;
                // Add eventhandlers for captureSource.
                captureSource.CaptureFailed += new EventHandler<ExceptionRoutedEventArgs>(OnCaptureFailed);

                // Initialize the camera if it exists on the phone.
                if (videoCaptureDevice != null)
                {
                    // Create the VideoBrush for the viewfinder.
                    videoRecorderBrush = new VideoBrush();
                    videoRecorderBrush.SetSource(captureSource);
                    
                    // Display the viewfinder image on the rectangle.
                    viewfinderRectangle.Fill = videoRecorderBrush;

                    // Wiring the CaptureImageCompleted event handler
                    captureSource.CaptureImageCompleted += (s, e) =>
                    {
                        // Do something with the camera snapshot
                        // e.Result is a WriteableBitmap
                        Process(e.Result);
                    };


                    // Start video capture and display it on the viewfinder.
                    captureSource.Start();

                    var dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 33); // 30 fps
                    dispatcherTimer.Tick += (s, e) =>
                    {
                        // Process camera snapshot if started
                        if (captureSource.State == CaptureState.Started)
                        {
                            // CaptureImageAsync fires the CaptureImageCompleted event
                            captureSource.CaptureImageAsync();
                        }
                    };
                    // Start the timer
                    dispatcherTimer.Start();

                    // Set the button state and the message.
                }
            }
        }
        private void Process(WriteableBitmap img) {
            // this is the Pixels raw data! use it wisely
            int[] arr = img.Pixels;
        }

        private void DisposeVideoPlayer()
        {
            if (VideoPlayer != null)
            {
                // Stop the VideoPlayer MediaElement.
                VideoPlayer.Stop();

                // Remove playback objects.
                VideoPlayer.Source = null;

                // Remove the event handler.
                VideoPlayer.MediaEnded -= VideoPlayerMediaEnded;
            }
        }

        private void DisposeVideoRecorder()
        {
            if (captureSource != null)
            {
                // Stop captureSource if it is running.
                if (captureSource.VideoCaptureDevice != null
                    && captureSource.State == CaptureState.Started)
                {
                    captureSource.Stop();
                }

                // Remove the event handler for captureSource.
                captureSource.CaptureFailed -= OnCaptureFailed;

                // Remove the video recording objects.
                captureSource = null;
                videoCaptureDevice = null;
                videoRecorderBrush = null;
            }
        }

        // If recording fails, display an error message.
        private void OnCaptureFailed(object sender, ExceptionRoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(delegate()
            {
                txtDebug.Text = "ERROR: " + e.ErrorException.Message.ToString();
            });
        }

        // Display the viewfinder when playback ends.
        public void VideoPlayerMediaEnded(object sender, RoutedEventArgs e)
        {
            // Remove the playback objects.
            DisposeVideoPlayer();

        }
    }
}