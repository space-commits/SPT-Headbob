using BepInEx;
using EFT.UI;
using EFT.UI.Settings;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Headbob.ExamplePatches
{
    [BepInPlugin("hauzman.bob", "Hauzman.Headbobbing", "1.0.0")]
    public class HeadBobRange : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameSettingsTab), "Show");
        }
        [PatchPostfix]
        private static void PatchPostfix(ref NumberSlider ____headbobbing, GClass1053 ___gclass1053_0)
        {
            SettingsTab.BindNumberSliderToSetting(____headbobbing, ___gclass1053_0.HeadBobbing, 0f, 1f, "F1");
        }
    }
    public class HeadBobPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GClass1053.Class1718).GetMethod("method_1", BindingFlags.Public | BindingFlags.Instance);
        }
        [PatchPrefix]
        private static bool Prefix(ref float __result, float x)
        {
            __result = Mathf.Clamp(x, 0.0f, 1f);
            return false;
        }
    }
}
