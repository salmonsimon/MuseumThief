using System;
using ZSerializer;

[Serializable, SerializeGlobalData(GlobalDataType.Globally)]
public partial class Settings
{
    public float musicVolume = 1;
    public float SFXVolume = 1;

    public void ResetAudioSettingsToDefault()
    {
        musicVolume = 1;
        SFXVolume = 1;
    }
}
