using System.Collections.Generic;
using System.Linq;
using HarmonyLib;

namespace crazyxyr.SelectLastCarePackage.Patches
{
    [HarmonyPatch(typeof(ImmigrantScreen), "OnProceed")]
    public static class ImmigrantScreenOnProceedPatch //按下打印按钮
    {
        public static bool Prefix(List<ITelepadDeliverable> ___selectedDeliverables)
        {
            var context = SaveGame.Instance.GetComponent<ImmigrantScreenContext>();
            var selectedDeliverable = ___selectedDeliverables.First();
          //  Debug.Log("上次结果" + context+"\n"+ selectedDeliverable    );

            if (selectedDeliverable is CarePackageContainer.CarePackageInstanceData CarePackageContainer)
            {
                context.LastSelectedCarePackageInfo = CarePackageContainer.info;
             //   Debug.Log("保存上次结果" + CarePackageContainer.info);



            }
            context.Skip = false;
            return true;
        }
    }
}