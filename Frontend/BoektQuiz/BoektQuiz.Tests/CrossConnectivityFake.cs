using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BoektQuiz.Tests
{
    public class CrossConnectivityFake : IConnectivity
    {
        public bool ConnectionValue { get; set; }
        public bool IsConnected => ConnectionValue;

        public IEnumerable<ConnectionType> ConnectionTypes => new[] { ConnectionType.WiFi };

        public IEnumerable<ulong> Bandwidths => new[] { (ulong)4000 };

        public event ConnectivityChangedEventHandler ConnectivityChanged;
        public event ConnectivityTypeChangedEventHandler ConnectivityTypeChanged;

        public void Dispose()
        {

        }

        public Task<bool> IsReachable(string host, int msTimeout = 5000)
        {
            return Task.FromResult(ConnectionValue);
        }

        public Task<bool> IsRemoteReachable(string host, int port = 80, int msTimeout = 5000)
        {
            return Task.FromResult(ConnectionValue);
        }
    }
}
