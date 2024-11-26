using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crazyxyr.SelectLastCarePackage.Patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "Initialize")]
    public static class ImmigrantScreenInitializePatch
    {
        // Token: 0x06000009 RID: 9 RVA: 0x00002444 File Offset: 0x00000644
        public static void Postfix(ImmigrantScreen __instance)
        {

            ImmigrantScreenMethod.ShowButton(__instance);

            //else
            //{

            //    Debug.Log("结果为空");
            //}
        }




    }
}
