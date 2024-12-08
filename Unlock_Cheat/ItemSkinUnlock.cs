using Database;
using HarmonyLib;
using Klei.CustomSettings;
using KMod;
using Newtonsoft.Json;
using System.Text;
using UnityEngine;


namespace ItemSkinUnlock
{


    public class ItemSkinUnlock
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
