using System.Reflection;
using UnityEngine;
using ZSerializer;

public partial class testing2 : GlobalObject
{
    private static testing2 _instance;
    public static testing2 Instance => _instance ??= Get<testing2>();
    
    public static void Save() => ZSerialize.SaveGlobal(Instance);
    public static void Load() => ZSerialize.LoadGlobal(Instance);
}
