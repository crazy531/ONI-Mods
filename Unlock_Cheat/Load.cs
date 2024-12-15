using HarmonyLib;
using KMod;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace Unlock_Cheat
{
    public class Load : UserMod2
    {
        // Token: 0x0600002A RID: 42 RVA: 0x0000239F File Offset: 0x0000059F
        public override void OnLoad(Harmony harmony)
        {


            Type[] types = this.assembly.GetTypes();
            PUtil.InitLibrary(false);
            new POptions().RegisterOptions(this, typeof(Options));
#if DEBUG
            ModUtil.RegisterForTranslation(typeof(Languages));
#else
            Localization.RegisterForTranslation(typeof(Languages));
#endif

            if (SingletonOptions<Options>.Instance.Achievement)
            {
                foreach (Type type in types.Where(n => n.Namespace == "Unlock_Cheat.AchievementUnlock")) {
                   // Debug.Log("测试Achievement：" + type.FullName);
                    harmony.CreateClassProcessor(type).Patch();

                }
      

            }


            if (SingletonOptions<Options>.Instance.Skin)
            {
                foreach (Type type in types.Where(n => n.Namespace == "Unlock_Cheat.ItemSkinUnlock"))
                {
                    //Debug.Log("ItemSkinUnlock：" + type.FullName);
                    harmony.CreateClassProcessor(type).Patch();

                }

            }
            if (SingletonOptions<Options>.Instance.Conduit)
            {

                foreach (Type type in types.Where(n => n.Namespace == "Unlock_Cheat.Conduit_mod"))
                {
                   // Debug.Log("Conduit："+ type.FullName);
                    harmony.CreateClassProcessor(type).Patch();

                }

            }

            if (SingletonOptions<Options>.Instance.MutantPlant)
            {

                foreach (Type type in types.Where(n => n.Namespace == "Unlock_Cheat.MutantPlants"))
                {
                    // Debug.Log("测试Achievement：" + type.FullName);
                    harmony.CreateClassProcessor(type).Patch();

                }

            }


            foreach (MethodBase method in harmony.GetPatchedMethods())
            {


                Debug.LogFormat("[Unlock_Cheat] 修补了：{0}.{1}", method.DeclaringType.FullName,method.Name);


            }
         




            // base.OnLoad(harmony);

        }
    }
}
