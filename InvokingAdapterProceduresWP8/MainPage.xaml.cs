/*
 *
    COPYRIGHT LICENSE: This information contains sample code provided in source code form. You may copy, modify, and distribute
    these sample programs in any form without payment to IBM® for the purposes of developing, using, marketing or distributing
    application programs conforming to the application programming interface for the operating platform for which the sample code is written.
    Notwithstanding anything to the contrary, IBM PROVIDES THE SAMPLE SOURCE CODE ON AN "AS IS" BASIS AND IBM DISCLAIMS ALL WARRANTIES,
    EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO, ANY IMPLIED WARRANTIES OR CONDITIONS OF MERCHANTABILITY, SATISFACTORY QUALITY,
    FITNESS FOR A PARTICULAR PURPOSE, TITLE, AND ANY WARRANTY OR CONDITION OF NON-INFRINGEMENT. IBM SHALL NOT BE LIABLE FOR ANY DIRECT,
    INDIRECT, INCIDENTAL, SPECIAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OR OPERATION OF THE SAMPLE SOURCE CODE.
    IBM HAS NO OBLIGATION TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS OR MODIFICATIONS TO THE SAMPLE SOURCE CODE.

 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using InvokingAdapterProceduresWP8.Resources;
using Newtonsoft.Json;
using IBM.Worklight;

namespace InvokingAdapterProceduresWP8
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            ConnectButton.Click += new RoutedEventHandler(ConnectButton_Click);
            InvokeButton.Click += new RoutedEventHandler(InvokeButton_Click);
            AboutBox.Text = "IBM MobileFirst Platform\nInvoking Adapter Procedure Sample";
        }

        void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            PanoramaControl.DefaultItem = Main;
            ReceivedTextBlock.Text = "Connecting ...\n";
            ConnectButton.Content = "Connecting ...";
            WLClient client = WLClient.getInstance();
            client.connect(new MyConnectResponseListener(this));
        }

        void InvokeButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectButton.Content = "Connect";
            ReceivedTextBlock.Text = "Invoking Procedure ...\n";
            PanoramaControl.DefaultItem = Console;

            WLProcedureInvocationData invocationData = new WLProcedureInvocationData("RSSReader", "getFeeds"); 
            invocationData.setParameters(new Object[]{});
            String myContextObject = "InvokingAdapterProceduresWP8";
            WLRequestOptions options = new WLRequestOptions(); 
            options.setInvocationContext(myContextObject);
            WLClient.getInstance().invokeProcedure(invocationData, new MyInvokeListener(this), options);
        }

        void ReceivedTextBlock_Tapped(object sender, RoutedEventArgs e)
        {
            ReceivedTextBlock.Text = "~ output comes here ~";
        }

        public void AddTextToReceivedTextBlock(String param)
        {
            ReceivedTextBlock.Text += param;
        } 
    }
}