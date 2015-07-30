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
using System.Text;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Threading.Tasks;
using Microsoft.Phone.Controls;
using IBM.Worklight;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InvokingAdapterProceduresWP8
{
    public class MyInvokeListener : WLResponseListener 
    { 
        InvokingAdapterProceduresWP8.MainPage myMainPage;
       
        public MyInvokeListener(InvokingAdapterProceduresWP8.MainPage page)
        {
            myMainPage = page;
        }

        public void onSuccess(WLResponse response)
        {
            WLProcedureInvocationResult invocationResponse = ((WLProcedureInvocationResult) response);
            JObject items;

            try
            {
                items = invocationResponse.getResponseJSON();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    myMainPage.AddTextToReceivedTextBlock("Response Success: " + items.ToString());
                });
            }
            catch (JsonReaderException e)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    myMainPage.AddTextToReceivedTextBlock("JSONException : " + e.Message);
                });
            }
        } 

        public void onFailure(WLFailResponse response) 
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                myMainPage.AddTextToReceivedTextBlock("Response failed: " + response.ToString());
            });
        }        
    } 
}
