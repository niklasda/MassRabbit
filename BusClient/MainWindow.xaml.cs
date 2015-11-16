using System;
using System.Diagnostics;
using System.Windows;
using BusMessages;
using MassTransit;

namespace BusClient
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
                var client = busControl.CreateRequestClient<ISimpleRequest, ISimpleResponse>(serviceAddress, TimeSpan.FromSeconds(10));
                
                ISimpleResponse response = await client.Request(new SimpleRequest("Niklas"));

                Debug.WriteLine("Customer Name: {0}", response.CustomerName);
                textBlock.Text = response.CustomerName;
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
