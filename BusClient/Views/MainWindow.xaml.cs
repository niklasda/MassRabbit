using System;
using System.Diagnostics;
using System.Windows;
using BusMessages.Interfaces;
using BusMessages.Requests;
using MassTransit;

namespace BusClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, RoutedEventArgs e)
        {
            // send a message

            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(x => x.Host(new Uri("rabbitmq://localhost"), h =>
            {
                h.Username("guest");
                h.Password("guest");
            }));

            busControl.Start();

            try
            {
                var serviceAddress = new Uri("rabbitmq://localhost/request_service");
                var simpleClient = busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
                var complexClient = busControl.CreateRequestClient<IComplexRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
                
                ISimpleResponse simpleResponse = await simpleClient.Request(new SimpleRequest("1"));
                ISimpleResponse complexResponse = await complexClient.Request(new ComplexRequest("2"));

                Debug.WriteLine("Customer Name1: {0}", simpleResponse.CustomerName);
                Debug.WriteLine("Customer Name2: {0}", complexResponse.CustomerName);
                textBlock.Text = simpleResponse.CustomerName + " - " + complexResponse.CustomerName;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception {0}", ex);
            }
            finally
            {
                Debug.WriteLine("Stopping it");
                busControl.Stop();
            }
        }
    }
}
