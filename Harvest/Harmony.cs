using System;
using System.Xml.Linq;
using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using UnityEngine;



namespace crazyxyr_mod
{
    internal class HarmonyPatches : UserMod2
    {
        // Token: 0x0600002A RID: 42 RVA: 0x0000239F File Offset: 0x0000059F
        public override void OnLoad(Harmony harmony)
        {
            PUtil.InitLibrary(false);
            new POptions().RegisterOptions(this, typeof(options));
            base.OnLoad(harmony);

        }

        // Token: 0x0200000A RID: 10
        public class Patches
        {
            [HarmonyPatch(typeof(NoseconeHarvestConfig))]
            [HarmonyPatch("DoPostConfigureComplete")]
            public class harvestModule
            {
                // Token: 0x06000041 RID: 65 RVA: 0x00002744 File Offset: 0x00000944
                public static void Postfix(GameObject go)
                {
                    bool NoseconeHarvest = SingletonOptions<options>.Instance.NoseconeHarvest;

                    ResourceHarvestModule.Def harvestModule = go.AddOrGetDef<ResourceHarvestModule.Def>();
                    Storage storage = go.AddOrGet<Storage>();
                    storage.capacityKg = 5000f;
                    ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
                    manualDeliveryKG.SetStorage(storage);
                    manualDeliveryKG.MinimumMass = storage.capacityKg;
                    manualDeliveryKG.capacity = storage.capacityKg;
                    manualDeliveryKG.refillMass = storage.capacityKg;
                }
            }

            [HarmonyPatch(typeof(ResourceHarvestModule.StatesInstance))]
            [HarmonyPatch("HarvestFromPOI")]
            public class ResourceHarvestModule_fix
            {
                public static void Postfix(float dt , ResourceHarvestModule.StatesInstance __instance)
                {
                        if (!__instance.CheckIfCanHarvest())
                    {
                        return;
                    }
                   

                }
            }

        }
    }
}