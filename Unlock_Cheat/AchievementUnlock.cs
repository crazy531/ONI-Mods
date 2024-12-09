using Database;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AchievementUnlock
{
    internal class Achievement_Unlock
    {

        [HarmonyPatch(typeof(Game))]
        [HarmonyPatch("Load")]
        public class Game_Load_Patch
        {
            // Token: 0x06000007 RID: 7
            public static void Postfix(Game __instance)
            {

                if (__instance.debugWasUsed)
                {
                    __instance.debugWasUsed = false;
                }

            }
        }

        [HarmonyPatch(typeof(ColonyAchievementTracker))]
        [HarmonyPatch("UnlockPlatformAchievement")]
        public class ColonyAchievementTracker_UnlockPlatformAchievement_Patch
        {
            // Token: 0x06000007 RID: 7
            public static bool Prefix(string achievement_id)
            {
                //  __result.sandboxEnabled=true;
                //return __result;
                bool result = DebugHandler.InstantBuildMode || SaveGame.Instance.sandboxEnabled || Game.Instance.debugWasUsed;
                if (!result)
                {
                    return true;

                }
                ColonyAchievement colony_achievement = Db.Get().ColonyAchievements.Get(achievement_id);
                bool flag = colony_achievement != null;
                if (flag)
                {
                    bool flag2 = !string.IsNullOrEmpty(colony_achievement.platformAchievementId);
                    if (flag2)
                    {
                        bool flag3 = SteamAchievementService.Instance;
                        if (flag3)
                        {
                            SteamAchievementService.Instance.Unlock(colony_achievement.platformAchievementId);
                        }
                        else
                        {
                            global::Debug.LogWarningFormat("Steam achievement [{0}] was achieved, but achievement service was null", new object[]
                            {
                                    colony_achievement.platformAchievementId
                            });
                        }
                    }
                }
                return false;

            }
        }


        [HarmonyPatch(typeof(RetiredColonyInfoScreen))]
        [HarmonyPatch("OnShow")]
        public class RetiredColonyInfoScreen_OnShow_Patch
        {
            // Token: 0x06000007 RID: 7
            public static void Postfix(RetiredColonyInfoScreen __instance)
            {
                //  __result.sandboxEnabled=true;
                //return __result;

                bool flag6 = SaveGame.Instance != null;
                if (flag6)
                {
                    GameObject disabledPlatformUnlocks = Traverse.Create(__instance).Field("disabledPlatformUnlocks").GetValue<GameObject>();

                    if (DebugHandler.InstantBuildMode || SaveGame.Instance.sandboxEnabled || Game.Instance.debugWasUsed)

                    {
                        disabledPlatformUnlocks.GetComponent<HierarchyReferences>().GetReference("disabled").gameObject.SetActive(false);
                        disabledPlatformUnlocks.GetComponent<HierarchyReferences>().GetReference("enabled").gameObject.SetActive(true);
                    }

                }

            }
        }


    }
}
