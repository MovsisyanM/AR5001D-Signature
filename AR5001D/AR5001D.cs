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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AR5001D
{
    public sealed partial class AR5001D
    {
        // 1.0.0.0
        #region Settings

        // 1.0.0.0
        /// <summary>
        /// Listen for bytes
        /// </summary>
        public bool IsListening
        {
            get { return isListening; }
        }

        // 1.0.0.0
        /// <summary>
        /// Listen for responsums
        /// </summary>
        public bool IsLive
        {
            get { return isLive; }
        }

        #endregion Settings

        /// <todo>
        /// Unrecognized
        /// Recognized
        /// AudioGainUpdated
        /// PoweredOn
        /// PoweredOff
        /// WakeUpIDUpdated
        /// StepUpdated
        /// ReceiveModeUpdated
        /// IFBandwidthUpdated
        /// AutoModeUpdated
        /// DestinationUpdated
        /// AutoNotchUpdated
        /// NoiseReductionUpdated
        /// NoiseBlankerUpdated
        /// VoiseInversionDescramblerUpdated
        /// IFShiftUpdated
        /// CWPitchUpdated
        /// AutomaticGainControlUpdated
        /// AutomaticFrequencyControlUpdated
        /// ToneSquelchUpdated
        /// DigitalCodeSquelchUpdated
        /// DTMFUpdated
        /// DeemphasisUpdated
        /// LevelSquelchUpdated
        /// VoiceSquelchUpdated
        /// RFAmplifierUpdated
        /// AntennaSelectionUpdated
        /// SignalLevelUpdated
        /// AutoSLevelUpdated
        /// ControlRelayUpdated
        /// ControlRelayStatusReportUpdated
        /// ManualRFGainUpdated
        /// RFFilterBandwidthADConverterUpdated
        /// RFBandPassFilterUpdated
        /// VFOUpdated
        /// RFUpdated
        /// SearchModeUpdated
        /// SearchDataReceived
        /// SearchDataOutputChanged
        /// SearchBankUpdated
        /// PassListUpdated
        /// NormalSearchSettingsUpdated
        /// FFTDatareceived
        /// FFTReportingUpdated (LC)
        /// FFTSettingsUpdated
        /// MemoryDataUpdated
        /// 
        /// 
        /// 
        /// </todo>
        #region Events

        /// <summary>
        /// Fired when the device does not recognize a command.
        /// </summary>
        public event Action Unrecognized;

        /// <summary>
        /// Fired when the device confirms the validity of a command.
        /// </summary>
        public event Action Recognized;

        /// <summary>
        /// Fired when the AudioGain value updates.
        /// </summary>
        public event Action<byte> AudioGainUpdated;

        /// <summary>
        /// Fires when a startup responsum has been received.
        /// </summary>
        public event Action PoweredOn;

        /// <summary>
        /// Fires when the AR5001D powers off.
        /// </summary>
        public event Action PoweredOff;

        /// <summary>
        /// Fires when WakeUpID updates (0~99).
        /// </summary>
        public event Action<byte> WakeUpIDUpdated;

        /// <summary>
        /// Fires when Sleep timer updates (0 ~ 99 minutes, 0 = off).
        /// </summary>
        public event Action<TimeSpan> SleepTimerUpdated;

        /// <summary>
        /// Fires when the Sleep timer display updates (off = false, on = true).
        /// </summary>
        public event Action<bool> SleepTimerDisplayUpdated;

        /// <summary>
        /// Fires when step size updates (in Hz, 0 ~ 999,999).
        /// </summary>
        public event Action<int> StepUpdated;

        /// <summary>
        /// Fires when the receive mode updates.
        /// </summary>
        public event Action<ReceiveMode> ReceiveModeUpdated;

        /// <summary>
        /// Fires when the IF bandwidth updates.
        /// </summary>
        public event Action<IFBandwidth> IFBandwidthUpdated;

        /// <summary>
        /// Fires when auto mode updates (false = off, true = on).
        /// </summary>
        public event Action<bool> AutoModeUpdated;

        /// <summary>
        /// Fires when Destination updates.
        /// </summary>
        public event Action<Destination> DestinationUpdated;

        /// <summary>
        /// Fires when Auto Notch updates.
        /// </summary>
        public event Action<AutoNotchState> AutoNotchUpdated;

        /// <summary>
        /// Fires when Noise reduction updates (false = disabled, true = enabled).
        /// </summary>
        public event Action<bool> NoiseReductionUpdated;

        /// <summary>
        /// Fires when Noise blanker updates (false = disabled, true = enabled).
        /// </summary>
        public event Action<bool> NoiseBlankerUpdated;

        /// <summary>
        /// Fires when the Voice Inversion Descrambler updates (0 = Off, 2000 ~ 7000 in Hz).
        /// </summary>
        public event Action<short> VoiseInversionDescramblerUpdated;

        /// <summary>
        /// Fires when IF Shift updates (0 = Off, -1200 ~ 1200 in Hz).
        /// </summary>
        public event Action<short> IFShiftUpdated;

        /// <summary>
        /// Fires when CW Pitch updates (300 ~ 900 in Hz).
        /// </summary>
        public event Action<short> CWPitchUpdated;                       

        /// <summary>
        /// Fires when Automatic Gain Control updates.
        /// </summary>
        public event Action<AutoGainControlState> AutomaticGainControlUpdated;          

        /// <summary>
        /// Fires when Automatic Frequency Control updates (false = disabled, true = enabled).
        /// </summary>
        public event Action<bool> AutoFrequencyControlUpdated;     

        /// <summary>
        /// Fires when Tone Squelch updates.
        /// </summary>
        public event Action<ToneSquelchState> ToneSquelchUpdated;              
             

        /// <summary>
        /// Fires when Digital Code Squelch updates.
        /// </summary>
        /// <remarks>
        /// (0 = off, 
        /// 017 023 025 026 031 032 036 043 047 050
        /// 051 053 054 065 071 072 073 074 114 115
        /// 116 122 125 131 132 134 143 145 152 155
        /// 156 162 165 172 174 205 212 223 225 226
        /// 243 244 245 246 251 252 255 261 263 265
        /// 266 271 274 306 311 315 325 331 332 343
        /// 346 351 356 364 365 371 411 412 413 423
        /// 431 432 445 446 452 454 455 462 464 465
        /// 466 503 506 516 523 526 532 546 565 606
        /// 612 624 627 631 632 654 662 664 703 712
        /// 723 731 732 734 743 754).
        /// </remarks>
        public event Action<short> DigitalCodeSquelchUpdated;            

        /// <summary>
        /// Fires when DTMF updates (false = off, true = on).
        /// </summary>
        public event Action<bool> DTMFUpdated;                          

        /// <summary>
        /// Fires when De-emphasis updates.
        /// </summary>
        public event Action<DeemphasisState> DeemphasisUpdated;

        /// <summary>
        /// Fires when Level Squelch updates.
        /// </summary>
        /// 
        /// <remarks>
        /// First param: Squelch Level above 25 MHz
        /// <para>
        /// Second param: Squelch Level below 25 MHz
        /// </para>
        /// </remarks>
        public event Action<byte, byte> LevelSquelchUpdated;

        /// <summary>
        /// Fires when Voise squelch updates.
        /// </summary>
        public event Action VoiceSquelchUpdated;

        /// <summary>
        /// Fires when Voise squelch delay updates.
        /// </summary>
        public event Action VoiceSquelchDelayUpdated;

        /// <summary>
        /// Fires when Voise squelch level updates.
        /// </summary>
        public event Action VoiceSquelchLevelUpdated;

        /// <summary>
        /// Fires when RF Amplifier updates.
        /// </summary>
        public event Action RFAmplifierUpdated;

        /// <summary>
        /// Fires when Antenna selection updates.
        /// </summary>
        public event Action AntennaSelectionUpdated;

        /// <summary>
        /// Fires when Signal level updates.
        /// </summary>
        public event Action SignalLevelUpdated;                   

        /// <summary>
        /// Fires when Auto Signal Level updates.
        /// </summary>
        public event Action AutoSLevelUpdated;                    

        /// <summary>
        /// Fires when Control Relay updates.
        /// </summary>
        public event Action ControlRelayUpdated;                  

        /// <summary>
        /// Fires when Control Relay Status Report updates
        /// </summary>
        public event Action ControlRelayStatusReportUpdated;      

        /// <summary>
        /// Fires when Manual RF Gain updates.
        /// </summary>
        public event Action ManualRFGainUpdated;

        /// <summary>
        /// Fires when the RF Filter Bandwidth for A/D Converter updates.
        /// </summary>
        public event Action RFFilterBandwidthADConverterUpdated;

        /// <summary>
        /// Fires when RF Band Pass Filter updates.
        /// </summary>
        public event Action RFBandPassFilterUpdated;

        /// <summary>
        /// Fires when VFO updates.
        /// </summary>
        public event Action VFOUpdated;

        /// <summary>
        /// Fires when RF updates.
        /// </summary>
        public event Action RFUpdated;

        /// <summary>
        /// Fires when Search mode updates.
        /// </summary>
        public event Action SearchModeUpdated;                    

        /// <summary>
        /// Fires whenever search data appears.
        /// </summary>
        public event Action SearchDataReceived;                   

        /// <summary>
        /// Fires when Search data output updates.
        /// </summary>
        public event Action SearchDataOutputUpdated;              

        /// <summary>
        /// Fires when a search bank updates.
        /// </summary>
        public event Action SearchBankUpdated;                    

        /// <summary>
        /// Fires when a frequency pass list updates.
        /// </summary>
        public event Action PassListUpdated;                      

        /// <summary>
        /// Fires when normal search settings update.
        /// </summary>
        public event Action NormalSearchSettingsUpdated;          

        /// <summary>
        /// Fires when FFT data appears.
        /// </summary>
        public event Action FFTDataReceived;                      

        /// <summary>
        /// Fires when FFT Reporting updates.
        /// </summary>
        public event Action FFTReportingUpdated;                  

        /// <summary>
        /// Fires when FFT Settings update.
        /// </summary>
        public event Action FFTSettingsUpdated;

        /// <summary>
        /// Fires when Memory data updates
        /// </summary>
        public event Action MemoryDataUpdated;

        /// <summary>
        /// Fires when the Up command has been sent to the device.
        /// </summary>
        public event Action UpFired;

        /// <summary>
        /// Fires when the Down command has been sent to the device
        /// </summary>
        public event Action DownFired;





        #endregion Events

        /// <todo>
        /// 
        /// </todo>
        #region Commands

        // 1.0.0.0
        /// <summary>
        /// Turns the receiver on.
        /// </summary>
        public void TurnOn()
        {
            open();
            GetInterpretator().Start();
            Write("ZP");
        }
        
        // 1.0.0.0
        /// <summary>
        /// Turns the receiver off.
        /// </summary>
        public void TurnOff()
        {
            if (port == null)
            {
                return;
            }
            Write("QP");
        }

        // 1.0.0.0
        /// <summary>
        /// Frequency/memory channel up
        /// </summary>
        public void Up()
        {
            Write("ZK");
            UpFired?.Invoke();
        }

        // 1.0.0.0
        /// <summary>
        /// Frequency/memory channel down
        /// </summary>
        public void Down()
        {
            Write("ZJ");
            DownFired?.Invoke();
        }




        #endregion Commands

        

        #region Properties



        /// <summary>
        /// Gets or sets the sleep timer, 0 for off.
        /// </summary>
        public byte SleepTimer
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                if (value > 99)
                {
                    throw new ArgumentOutOfRangeException("Minutes", "Sleep timer accepts input 0-99");
                }
                
                Write("SP" + value.ToString().PadLeft(2, '0'));
            }
        }

        

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        public byte AudioGain
        {
            get
            {
                byte[] responsum = new byte[8];
                string cmd = "AG";
                open();
                port.BaseStream.Flush();
                Write(cmd);
                port.Read(responsum, 0, port.BytesToRead);
                port.BaseStream.Flush();
                // If it starts with AG and ends with the responsum signature
                if (cmd[0] == responsum[0].ToASCII() &&
                    cmd[1] == responsum[1].ToASCII() &&
                    (responsum[responsum.Length - 2] == 13 || responsum[responsum.Length - 1] == 13))
                {
                    string num = "" + responsum[2].ToASCII() + responsum[3].ToASCII() + responsum[4].ToASCII();
                    byte afGain;
                    byte.TryParse(num, out afGain);
                    return afGain;
                }
                else { return AudioGain; }
                
            }
            set
            {
                string cmd = "AG";
                open();
                Write(cmd + value.ToString().PadLeft(3, '0'));
                byte[] responsum = new byte[port.BytesToRead];
                port.Read(responsum, 0, port.BytesToRead);
                if (responsum[0] != 32)
                {
                    AudioGain = value;
                }
            }
        }



        /// <summary>
        /// Gets or sets the step size
        /// </summary>
        public float Step
        {
            get
            {
                open();
                flush();
                Write("ST");
                Thread.Sleep(20);
                StringBuilder parsed = new StringBuilder();
                float num;
                byte[] responsum = new byte[port.BytesToRead];
                port.Read(responsum, 0, responsum.Length);
                flush();
                foreach (byte bite in responsum.Cut(2, responsum.Length - 3))
                {
                    parsed.Append(bite.ToASCII());
                }
                float.TryParse(parsed.ToString(), out num);
                return num;
            }
            set
            {
                open();
                flush();
                StringBuilder strBldr = new StringBuilder();
                if (Math.Abs(value) == value)
                {

                }
                Thread.Sleep(20);
                byte[] responsum = new byte[port.BytesToRead];
                port.Read(responsum, 0, responsum.Length);
                flush();
            }
        }



        #endregion Properties
    }

}
