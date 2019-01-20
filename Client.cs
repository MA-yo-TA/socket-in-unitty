using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Net;

namespace Script.Client
{
    public class Client : MonoBehaviour
    {
        #region private members
        private TcpClient socketConnection;
        private Thread clientReceiveThread;
        #endregion
        // Use this for initialization
        void Start()
        {
            ConnectToTcpServer();

        }
        // Update is called once per frame
        // Maybe unnecessary
        void Update()
        {

        }
        /// <summary>
        /// Setup socket connection.
        /// </summary>
        private void ConnectToTcpServer()
        {
            try
            {
                clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                clientReceiveThread.IsBackground = true;
                clientReceiveThread.Start();
                Debug.Log("YES");
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception " + e);
            }
        }
        /// <summary>
        /// Runs in background clientReceiveThread; Listens for incomming data.
        /// </summary>
        private void ListenForData()
        {
          //xxx.xxx.xx.x is ipAddress xxxx is a port num
            IPEndPoint iPEP = new IPEndPoint(IPAddress.Parse("xxx.xxx.xx.x"), xxxx);
            try
            {

                socketConnection = new TcpClient();
                socketConnection.Connect(iPEP);
                if (socketConnection.Connected == false)
                {
                    return;
                }
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    // Get a stream object for reading
                    using (NetworkStream stream = socketConnection.GetStream())
                    {
                        int length;
                        // Read incomming stream into byte arrary.
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incommingData = new byte[length];
                            Array.Copy(bytes, 0, incommingData, 0, length);
                            // Convert byte array to string message.
                            string serverMessage = Encoding.UTF8.GetString(incommingData);
                            ClientTask.Instance.SetReceivedStr(serverMessage);
                            Debug.Log("server message received as: " + serverMessage);
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }
        /// <summary>
        /// Send message to server using socket connection.
        /// </summary>
        private void SendMessage()
        {
            if (socketConnection == null)
            {
                return;
            }
            try
            {
                // Get a stream object for writing.
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    string clientMessage = "PLEASE\n";
                    // Convert string message to byte array.
                    byte[] clientMessageAsByteArray = Encoding.UTF8.GetBytes(clientMessage);
                    // Write byte array to socketConnection stream.
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    Debug.Log("Client sent his message" + clientMessage + " - should be received by server");
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }

        public void PushToSend()
        {
            SendMessage();
        }
    }
}
