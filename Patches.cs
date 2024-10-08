﻿using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using EFT.UI.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Headbob
{
    public class HeadBonPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GameSettingsTab).GetMethod("Show");
        }
        [PatchPrefix]
        private static void PatchPrefix(ref ReadOnlyCollection<float> ___readOnlyCollection_1)
        {
            float[] headBobRange = new float[]
            {
            0.0f,
            0.1f,
            0.2f,
            0.3f,
            0.4f,
            0.5f,
            0.6f,
            0.7f,
            0.8f,
            0.9f,
            1f
            };
            ___readOnlyCollection_1 = Array.AsReadOnly(headBobRange);
        }
    }


    //to find this method for a new client version, look for something like this: this.HeadBobbing = base.method_2<float>("Settings/Game/HeadBobbing", 0.2f, new Func<float, float>(GClass971.Class1513.class1513_0.method_1));
    public class HeadBobClampPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(GClass960.Class1578).GetMethod("method_1", BindingFlags.Public | BindingFlags.Instance);
        }

        [PatchPrefix]
        private static bool Prefix(ref float __result, float x)
        {
            __result = Mathf.Clamp(x, 0.0f, 1f);
            return false;
        }
    }

}
