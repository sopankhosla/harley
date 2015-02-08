﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Kinect;
using Kinect.Toolbox;
using KinectMeasurementsLib;

namespace Harley
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The kinect sensor object
        /// </summary>
        private KinectSensor kinectSensor;

        /// <summary>
        /// The speech object
        /// </summary>
        private Speech speech;

        /// <summary>
        /// Readonly array of word list to recognize
        /// </summary>
        private readonly string[] grammar = { "star", "shape", "facial" };

        public MainWindow()
        {
            /*InitializeComponent();

            InitializeKinect();

            this.speech.Speak("Welcome to Harley");
            this.speech.Speak("What would you like to do?");
            this.speech.Start();*/

            //this.SwitchToStarActivityWindow();
            //this.SwitchToGestureActivityWindow();
            this.SwitchToFaceRecognitionActivityWindow();

            // Initialize speech recognition here
        }

        /// <summary>
        /// Called at the start when the window is loaded
        /// </summary>
        private void InitializeKinect()
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.kinectSensor = potentialSensor;
                    break;
                }
            }

            if (null != this.kinectSensor)
            {
                this.kinectSensor.Start();
            }

            if (null == this.kinectSensor)
            {
                // Connection is failed
                return;
            }

            this.speech = new Speech(this.kinectSensor, grammar);
            this.speech.Start();
        }

        /// <summary>
        /// Switches the window to Star Activity window
        /// </summary>
        private void SwitchToStarActivityWindow()
        {
            Window starActivityWindow = new StarActivityWindow();
            App.Current.MainWindow = starActivityWindow;
            this.Close();
            starActivityWindow.Show();
        }

        /// <summary>
        /// Switches the window to Face Recognition activity window
        /// </summary>
        private void SwitchToFaceRecognitionActivityWindow()
        {
            Window faceRecogWindow = new FaceRecognitionActivityWindow();
            App.Current.MainWindow = faceRecogWindow;
            this.Close();
            faceRecogWindow.Show();
        }

        /// <summary>
        /// Switches the window to Gesture activity window
        /// </summary>
        private void SwitchToGestureActivityWindow()
        {
            Window gestureWindow = new GestureActivityWindow();
            App.Current.MainWindow = gestureWindow;
            this.Close();
            gestureWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Clean();
        }

        private void Clean()
        {
            // All cleanup work goes here
            // this includes uninitializing Sensors
        }
    }

}
