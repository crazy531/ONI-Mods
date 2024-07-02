using Database;
using HarmonyLib;
using KMod;


namespace ItemSkinUnlock
{
    public class ItemSkinUnlock : UserMod2
    {
        // Token: 0x0600002A RID: 42 RVA: 0x0000239F File Offset: 0x0000059F
        public override void OnLoad(Harmony harmony)
        {
       
            base.OnLoad(harmony);

        }
    }

    public class Patches
    {
        [HarmonyPatch(typeof(PermitItems))]
        [HarmonyPatch("GetOwnedCount")]
        public class PermitItems_GetOwnedCount
        {
            // Token: 0x06000041 RID: 65 RVA: 0x00002744 File Offset: 0x00000944
            public static void Postfix(PermitResource permit,ref int __result)
            {   

                __result += 1;
            }
        }


        [HarmonyPatch(typeof(PermitResource))]
        [HarmonyPatch("IsOwnableOnServer")]
        public class PermitResource_IsOwnableOnServer
        {
            // Token: 0x06000041 RID: 65 RVA: 0x00002744 File Offset: 0x00000944
            public static bool Prefix( ref bool __result)
            {
                __result = true;

                return false;
            }
        }
    }
 
}
