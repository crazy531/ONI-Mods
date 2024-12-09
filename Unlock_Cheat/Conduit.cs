using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit
{
    internal class Conduit
    {


        [HarmonyPatch(typeof(ConduitFlow))]
        [HarmonyPatch("FreezeConduitContents")]  //过冷损坏
        public class ConduitFlow_FreezeConduitContents_Patch
        {
            // Token: 0x06000007 RID: 7
            public static bool Prefix(ConduitFlow __instance)
            {

              return false;

            }
        }


        [HarmonyPatch(typeof(ConduitFlow))]
        [HarmonyPatch("MeltConduitContents")]  //过热损坏
        public class ConduitFlow_MeltConduitContents_Patch
        {
            // Token: 0x06000007 RID: 7
            public static bool Prefix(ConduitFlow __instance)
            {

                return false;

            }
        }



    }
}
