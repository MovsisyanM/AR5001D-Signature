/*
 Made by Movsisyan.
 Find me on GitHub.
 Contact me at movsisyan@protonmail.com for future endeavors.
 Գտիր ինձ ԳիթՀաբ-ում:
 Գրիր ինձ movsisyan@protonmail.com հասցեյով հետագա առաջարկների համար:
 2019
*/
namespace AR5001D
{
    public sealed partial class AR5001D
    {
        public enum ReceiveMode : byte
        {
            FrequencyModulation,
            FMStereo,
            AmplitudeModulation,
            SynchronousAM,
            UpperSideBand,
            LowerSideBand,
            ContinuousWave,
            IndependentSideBand,
            AnalogIQ,
            WFM100k = 21,
            WFM200k,
            FMStereo200k,
            NFM15k,
            SFM6k,
            WAM15k,
            AM6k,
            NAM3k,
            SAM6k,
            USB3k,
            LSB3k,
            CW500,
            CW200,
            ISB6k,
            AIQ15k
        }
    }
}