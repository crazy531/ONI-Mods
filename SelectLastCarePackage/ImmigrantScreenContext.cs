using HarmonyLib;
using KSerialization;
using System.Collections.Generic;

namespace undancer.SelectLastCarePackage
{
    public class ImmigrantScreenContext : KMonoBehaviour
    {
        public bool Skip { get; set; }
        [Serialize] public CarePackageInfo LastSelectedCarePackageInfo { get; set; }
    }

    public static class ImmigrantScreenMethod
    {


        public static void Reshuffle(ITelepadDeliverableContainer container)
        {
            Traverse.Create(container).Method("Reshuffle", new object[] { true }).GetValue();

            //MethodInfo method = container.GetType().GetMethod("Reshuffle", BindingFlags.Instance | BindingFlags.NonPublic);

            //            method?.Invoke(container, new object[] { true });
        }

        public static void ShowButton(ImmigrantScreen __instance)
        {

            List<ITelepadDeliverableContainer> deliverableContainerList = Traverse.Create(__instance).Field("containers").GetValue<List<ITelepadDeliverableContainer>>();
            if (deliverableContainerList != null)
            {
                deliverableContainerList.ForEach(c =>
                {
                    if (c is CharacterContainer characterContainer) characterContainer.SetReshufflingState(true);
                    else if (c is CarePackageContainer carePackageContainer)



                        carePackageContainer.SetReshufflingState(true);

                    Traverse.Create(c).Field("reshuffleButton").Field("onClick").SetValue(new System.Action(delegate
                    {

                        ImmigrantScreenMethod.Reshuffle(c);

                    }));
                }

            );

                //    Debug.Log("刷新按钮生成");
            }




        }



    }



}