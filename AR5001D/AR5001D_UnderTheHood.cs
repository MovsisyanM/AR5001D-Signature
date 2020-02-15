/*
 Made by Movsisyan.
 Find me on GitHub.
 Contact me at movsisyan@protonmail.com for future endeavors.
 Գտիր ինձ ԳիթՀաբ-ում:
 Գրիր ինձ movsisyan@protonmail.com հասցեյով հետագա առաջարկների համար:
 2019
*/

using AR5001D;
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace AR5001D
{
    /// <summary>
    /// This class provides the means to interact with the AR5001D reciever via .Net
    /// </summary>
    public sealed partial class AR5001D : IDisposable
    {
        // 1.0.0.0
        #region Variables and Properties

        // 1.0.0.0
        /// <summary>
        /// This object is responsible for interpreting incoming responsums
        /// </summary>
        private Interpretator interpretator;

        // 1.0.0.0
        /// <summary>
        /// This is the COM port used to communicate with the device
        /// </summary>
        private SerialPort port;

        // 1.0.0.0
        /// <summary>
        /// This object is responsible for handling responsums.
        /// </summary>
        private Resolver resolver;
        
        private bool isLive;        // 1.0.0.0
        private bool isListening;   // 1.0.0.0

        // private ScreamerList<> commandBuffer TODO

        // shud be internal
        /// <summary>
        /// Notifies about the received responsum.
        /// </summary>
        public event Action ResponsumReceived;

        private int baudRate = 115200;

        #endregion Variables and Properties


        #region Constructors

        /// <summary>
        /// Creates an instance of AR5001D
        /// </summary>
        /// <param name="ComPort">COM port name, defaults to COM5</param>
        public AR5001D(string ComPort = "COM5")
        {
            if (ComPort[0] == 'C' && ComPort[1] == 'O' && ComPort[2] == 'M')
            {
                port = new SerialPort(ComPort, 115200, Parity.None, 8, StopBits.One);
            }
            else
            {
                throw new ArgumentException("Please enter a valid COM port. Example: COM5");
            }

        }
        
        public AR5001D(string ComPort = "COM5", int BaudRate = 115200)
        {
            if (ComPort[0] == 'C' && ComPort[1] == 'O' && ComPort[2] == 'M')
            {
                baudRate = BaudRate;
                port = new SerialPort(ComPort, BaudRate, Parity.None, 8, StopBits.One);
            }
            else
            {
                throw new ArgumentException("Please enter a valid COM port. Example: COM5");
            }

        }

        // TODO: Automatic port detection

        #endregion Constructors


        #region Methods

        // 1.0.0.1
        /// 
        /// <summary>
        /// Opens the port.
        /// </summary>
        /// 
        /// <exception cref="UnauthorizedAccessException">
        /// Most likely caused by different software using the said port
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// The port is null.
        /// </exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <see cref="SerialPort.Open()"/>
        /// <seealso cref="SerialPort.IsOpen"/>
        private void open()
        {
            if (port.IsOpen) return;    // Checks to see if the port is open.
            port.Open();                // Opens the port.
        }

        // 1.0.0.1
        /// <summary>
        /// Clear the buffers
        /// </summary>
        /// 
        /// <exception cref = "NullReferenceException" >
        /// The port is null.
        /// </exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <see cref="Stream.Flush()"/>
        /// <seealso cref="SerialPort.BaseStream"/>
        public void flush()
        {
            port.BaseStream.Flush();
            port.Close();
            port.Dispose();
            port = new SerialPort("COM1", baudRate, Parity.None, 8, StopBits.One);
            port.Open();
        }

        // 1.0.0.1
        /// <summary>
        /// Safely closes the port
        /// </summary>
        /// 
        /// <exception cref="NullReferenceException">
        /// The port is null.
        /// </exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <see cref="SerialPort.Close()"/>
        /// <seealso cref="SerialPort.IsOpen"/>
        private void close()
        {
            if (port.IsOpen) port.Close();
        }

        // 1.0.0.0
        /// <summary>
        /// Checks the connection between the PC and the AR5001D
        /// </summary>
        /// <returns>True if connection confirmed</returns>
        public bool Check()
        {
            byte[] ping = new byte[2] { 13, 10 };
            bool worked = false;
            ScreamerList<byte[]>.ListChangedHandler callbck = () =>
            {
                worked = true;
            };
            GetInterpretator().Buffer.ItemAdded += callbck;
            for (int i = 0; i < 20; i++)
            {
                Write("A");
                if (worked)
                {
                    GetInterpretator().Buffer.ItemAdded -= callbck;
                    return true;
                }
                Thread.Sleep(5);
            }
            return false;
        }

        // 1.0.0.0
        /// <summary>
        /// Generates the bytes from command string and structures it for deployment
        /// </summary>
        /// <param name="command">Command in string format</param>
        /// <returns>Byte array used to contact the AR5001D</returns>
        internal byte[] Command(string command)
        {
            // Encode command into ascii bytes
            byte[] buffer = command.ToASCII();
            // Append 0d to indicate end of command
            byte[] output = new byte[buffer.Length + 1];
            int k = 0;
            foreach (byte bite in buffer)
            {
                output[k] = bite;
                k++;
            }
            output[output.Length - 1] = 13;


            return output;
        }

        // 1.0.0.0
        /// <summary>
        /// Writes the given string to the port in a structured manner.
        /// </summary>
        /// <param name="Message">Input string that is to the port</param>
        public void Write(string Message)
        {
            port.Write(Command(Message), 0, Command(Message).Length);
        }

        // 1.0.0.0
        /// <summary>
        /// Provides a singleton Interpretator instance.
        /// </summary>
        /// <returns>Singleton Interpretator instance</returns>
        public Interpretator GetInterpretator()
        {
            if (interpretator == null)
            {
                interpretator = new Interpretator(this);
                interpretator.Buffer.ItemAdded += responsumReceived;
            }

            return interpretator;
        }

        /// <summary>
        /// Relays the news to the Responsumreceived event
        /// </summary>
        private void responsumReceived()
        {
            ResponsumReceived?.Invoke();
        }
        
        // 1.0.0.0
        /// <summary>
        /// Provides a singleton Resolver instance.
        /// </summary>
        /// <returns>Singleton Interpretator instance</returns>
        internal Resolver GetResolver()
        {
            if (resolver == null) resolver = new Resolver();
            return resolver;
        }

        #endregion Method

        // 1.0.0.0
        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls 

        /// <summary>
        /// Destructs the device, freeing managed resources.
        /// </summary>
        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    port.Dispose();
                }

                interpretator = null;
                port = null;
                resolver = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// Destructs the device, freeing managed resources.
        /// </summary>
        ~AR5001D()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        /// <summary>
        /// Destructs the device, freeing managed resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}