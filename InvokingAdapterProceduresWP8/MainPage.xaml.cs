/**
* Copyright 2015 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
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

            WLProcedureInvocationData invocationData = new WLProcedureInvocationData("RSSReader", "getFeed"); 
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
