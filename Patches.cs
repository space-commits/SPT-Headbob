using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using EFT.UI.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HeadBobClass = GClass1040;
using HeadBobSibClass = GClass1040.Class1690;

namespace Headbob
{
    public class HeadBobRangePatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameSettingsTab).GetMethod("Show");
        }

        [PatchPostfix]
        private static void PatchPostfix(EFT.UI.NumberSlider ____headbobbing, HeadBobClass ___gclass1040_0)
        {
            SettingsTab.BindNumberSliderToSetting(____headbobbing, ___gclass1040_0.HeadBobbing, 0f, 1f, "F1");
        }
    }


    //to find this method for a new client version, look for something like this: this.HeadBobbing = base.method_2<float>("Settings/Game/HeadBobbing", 0.2f, new Func<float, float>(GClass971.Class1513.class1513_0.method_1));
    public class HeadBobClampPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(HeadBobSibClass).GetMethod("method_1", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool Prefix(ref float __result, float x)
        {
            __result = Mathf.Clamp(x, 0.0f, 1f);
            return false;
        }
    }

}
