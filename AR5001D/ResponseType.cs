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
    public enum ResponsumType
    {
        Unrecognized,   // 0x3f
        Ok,             // 0x20 for no params, <command><value> with params
        PowerOn,        // AR5001D Start!!!
        PowerOff,       // AR5001D Shut Down!!!<0x20>
        WakeUpId,       // ZInn
        
        // TODO
    }
}