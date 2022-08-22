using System.Reflection;
using UnityEngine;
using ZSerializer;

public partial class testing : GlobalObject
{
    private static testing _instance;
    public static testing Instance => _instance ??= Get<testing>();
    
    public static void Save() => ZSerialize.SaveGlobal(Instance);
    public static void Load() => ZSerialize.LoadGlobal(Instance);
}
