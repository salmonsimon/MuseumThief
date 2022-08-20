[System.Serializable]
public sealed class StolenManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Collections.Generic.List<Stealable> carrying;
    public System.Collections.Generic.List<Stealable> stolen;
    public System.Int32 money;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public StolenManagerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         carrying = (System.Collections.Generic.List<Stealable>)typeof(StolenManager).GetField("carrying").GetValue(instance);
         stolen = (System.Collections.Generic.List<Stealable>)typeof(StolenManager).GetField("stolen").GetValue(instance);
         money = (System.Int32)typeof(StolenManager).GetField("money").GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(StolenManager).GetField("carrying").SetValue(component, carrying);
         typeof(StolenManager).GetField("stolen").SetValue(component, stolen);
         typeof(StolenManager).GetField("money").SetValue(component, money);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}