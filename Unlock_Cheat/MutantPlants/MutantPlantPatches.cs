using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Diagnostics;
using KMod;

namespace Unlock_Cheat.MutantPlants
{
    internal class MutantPlantPatches
    {

        public static global::Action MaxAction { get {

                

              
                global::Action action;
                if (!Enum.TryParse<global::Action>("NumActions", out action))
                {
                    Array values = Enum.GetValues(typeof(global::Action));
                    if (values.Length > 0)
                    {
                        action = (global::Action)values.GetValue(values.Length - 1);
                    }
                    else
                    {
                        action = global::Action.NumActions;
                    }
                }
                return  action;
                }

            
        }
        private static void OnRefreshUserMenu(MutantPlant mutant)
        {
            KPrefabID kprefabID;
            if (mutant != null && mutant.TryGetComponent<KPrefabID>(out kprefabID))
            {
                if ((mutant.IsOriginal && !kprefabID.HasTag(GameTags.PlantBranch)) || kprefabID.HasTag(GameTags.Seed) || kprefabID.HasTag(GameTags.CropSeed) || kprefabID.HasTag(GameTags.Plant))
                {
                    KIconButtonMenu.ButtonInfo button = new KIconButtonMenu.ButtonInfo("action_select_research", Languages.UI.USERMENUACTIONS.MUTATOR.NAME, new System.Action(mutant.Mutator), MaxAction, null, null, null, Languages.UI.USERMENUACTIONS.MUTATOR.TOOLTIP, true);
                    Game.Instance.userMenu.AddButton(mutant.gameObject, button, 1f);
                }
                if (!mutant.IsOriginal && !mutant.IsIdentified)
                {
                    KIconButtonMenu.ButtonInfo button2 = new KIconButtonMenu.ButtonInfo("action_select_research", Languages.UI.USERMENUACTIONS.IDENTIFY_MUTATION.NAME, new System.Action(mutant.IdentifyMutation), MaxAction, null, null, null, Languages.UI.USERMENUACTIONS.IDENTIFY_MUTATION.TOOLTIP, true);
                    Game.Instance.userMenu.AddButton(mutant.gameObject, button2, 1f);
                }
            }
        }

        private static readonly EventSystem.IntraObjectHandler<MutantPlant> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<MutantPlant>(delegate (MutantPlant component, object data)
        {
            MutantPlantPatches.OnRefreshUserMenu(component);
        });


        [HarmonyPatch(typeof(MutantPlant), "OnSpawn")]
        public static class MutantPlant_OnSpawn
        {
            // Token: 0x06000025 RID: 37 RVA: 0x00002CC9 File Offset: 0x00000EC9
            public static void Postfix(MutantPlant __instance)
            {
                __instance.Subscribe<MutantPlant>(493375141, MutantPlantPatches.OnRefreshUserMenuDelegate);
            }
        }

        // Token: 0x0200000C RID: 12
        [HarmonyPatch(typeof(MutantPlant), "OnCleanUp")]
        public static class MutantPlant_OnCleanUp
        {
            // Token: 0x06000026 RID: 38 RVA: 0x00002CDC File Offset: 0x00000EDC
            public static void Prefix(MutantPlant __instance)
            {
                __instance.Unsubscribe<MutantPlant>(493375141, MutantPlantPatches.OnRefreshUserMenuDelegate, false);
            }
        }
    }
}
