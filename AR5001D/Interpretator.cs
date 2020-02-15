/*
 Made by Movsisyan.
 Find me on GitHub.
 Contact me at movsisyan@protonmail.com for future endeavors.
 Գտիր ինձ ԳիթՀաբ-ում:
 Գրիր ինձ movsisyan@protonmail.com հասցեյով հետագա առաջարկների համար:
 2019
*/
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;

namespace AR5001D
{
    public sealed partial class AR5001D
    {
        // 1.0.0.0
        /// <summary>
        /// Interpreting engine that reads responsums from the AR5001D
        /// </summary>
        public sealed class Interpretator
        {
            // 1.0.0.0
            #region Variables and Properties 

            /// <summary>
            /// This holds a ref to the parent device
            /// </summary>
            private AR5001D device; // 1.0.0.0

            /// <summary>
            /// This list provides notifications and stores the incoming responsums as byte arrays.
            /// </summary>
            public ScreamerList<byte[]> Buffer { get; set; } = new ScreamerList<byte[]>(); // 1.0.0.0

            #endregion Variables and Properties

            // 1.0.0.0
            #region Constructors

            /// <summary>
            /// Creates an instance of Interpretator.
            /// </summary>
            internal Interpretator() // 1.0.0.0
            {
                device = null;
            }

            /// <summary>
            /// Creates an instance of Interpretator.
            /// </summary>
            internal Interpretator(AR5001D Device) // 1.0.0.0
                : this()
            {
                device = Device;
                device.port.DataReceived += dataReceivedHandler;
            }


            #endregion Constructors

            // 1.0.0.0
            #region Methods

            // 1.0.0.0
            /// <summary>
            /// Handles the received data
            /// </summary>
            /// <param name="sender">SerialPort object</param>
            /// <param name="e">Provides data for the SerialPort.DataReceived event</param>
            private void dataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
            {
                Buffer.Add(Interpret(device));
                if (device.port.BytesToRead > 0)
                {
                    dataReceivedHandler(sender, e);
                }
            }

            // 1.0.0.0
            /// <summary>
            /// Listens for a single byte, returns null if the buffer is empty
            /// </summary>
            /// <returns>Null if empty</returns>
            private byte? Listen(AR5001D Device)
            {
                // Check for data before reading.
                if (Device.port.BytesToRead > 0)
                {
                    // Reads a single byte.
                    return (byte)Device.port.ReadByte();
                }
                else
                {
                    return null;
                }
            }

            // 1.0.0.0
            /// <summary>
            /// Interprets a command 
            /// </summary>
            /// <param name="Device">The device on which interpretation should be done.</param>
            /// <returns>Responsum as a byte array</returns>
            internal byte[] Interpret(AR5001D Device)
            {
                List<byte> cmdBuffer = new List<byte>();    // Holds the incoming bytes.
                while (Device.IsListening)                  // Checks for permissions to listen.
                {
                    byte? bite = Listen(Device);            // Gets a single byte.
                    if (!bite.HasValue) { }                 // Checks for null values.
                    else { cmdBuffer.Add(bite.Value); }     // Stores the received byte.
                    if (cmdBuffer.Count > 1)
                    {   // Checks to see if the buffer holds a full responsum.
                        if (cmdBuffer[cmdBuffer.Count - 2] == 13 && cmdBuffer[cmdBuffer.Count - 1] == 10)
                        {
                            return cmdBuffer.ToArray();     // Returns the full responsum.
                        }
                    }
                }
                return null; // Returns null if the device does not have permission to listen.
            }

            // 1.0.0.0
            ///
            /// <summary>
            /// Starts appending incoming responsums to the buffer in real-time.
            /// </summary>
            /// 
            /// <param name="Device">The device on which the interpretation should be initiated</param>
            /// 
            public void Start(AR5001D Device)
            {
                device.isListening = true;      // Gives permission to listen for bytes.
                device.isLive = true;           // Gives permission to interpret responsums.
            }

            // 1.0.0.0
            /// <summary>
            /// Stops appending incoming responsums to the buffer
            /// </summary>
            /// <param name="Device">The device which should stop recieving responsums</param>
            public void Stop(AR5001D Device)
            {
                Device.isListening = false;     // Removes permission to listen for bytes.
                Device.isLive = false;          // Removes permission to interpret for responsums.
            }

            // 1.0.0.0
            /// <summary>
            /// Listens for a single byte, returns null if the buffer is empty
            /// </summary>
            /// <returns>Null if empty</returns>
            private byte? Listen()
            {
                // Checks for null devices
                if (device == null)
                {
                    throw new ApplicationException("The AR5001D.Interpretator.Listen() did not find the reciever");
                }
                // Check for data before reading.
                if (device.port.BytesToRead > 0)
                {
                    // Reads a single byte.
                    return (byte)device.port.ReadByte();
                }
                else
                {
                    return null;
                }
            }

            // 1.0.0.0
            /// <summary>
            /// Interprets a command 
            /// </summary>
            /// <returns>Responsum as a byte array</returns>
            internal byte[] Interpret()
            {
                // Checks for null devices
                if (device == null)
                {
                    throw new ApplicationException("The AR5001D.Interpretator.Interpret() did not find the reciever");
                }
                List<byte> cmdBuffer = new List<byte>();    // Holds the incoming bytes.
                while (device.IsListening)                  // Checks for permissions to listen.
                {
                    byte? bite = Listen(device);            // Gets a single byte.
                    if (!bite.HasValue) { }                 // Checks for null values.
                    else { cmdBuffer.Add(bite.Value); }     // Stores the received byte.
                    if (cmdBuffer.Count > 1)
                    {   // Checks to see if the buffer holds a full responsum.
                        if (cmdBuffer[cmdBuffer.Count - 2] == 13 && cmdBuffer[cmdBuffer.Count - 1] == 10)
                        {
                            return cmdBuffer.ToArray();     // Returns the full responsum.
                        }
                    }
                }
                return null; // Returns null if the device does not have permission to listen.
            }

            // 1.0.0.0
            ///
            /// <summary>
            /// Starts appending incoming commands to the buffer in real-time.
            /// </summary>
            /// 
            public void Start()
            {
                // Checks for null devices
                if (device == null)
                {
                    throw new ApplicationException("The AR5001D.Interpretator.Start() did not find the reciever");
                }
                Start(device);
            }


            /// 
            /// <summary>
            /// Stops the real-time interpretation.
            /// </summary>
            /// 
            public void Stop()
            {
                // Checks for null devices
                if (device == null)
                {
                    throw new ApplicationException("The AR5001D.Interpretator.Stop() did not find the reciever");
                }
                Stop(device);
            }

            #endregion Methods
        }
    }
}