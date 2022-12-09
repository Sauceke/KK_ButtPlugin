﻿using HarmonyLib;
using LoveMachine.Core;
using System;

namespace LoveMachine.IO
{
    internal static class Hooks
    {
        public static void InstallHooks()
        {
            var twosome = Type.GetType("FH_SetUp, Assembly-CSharp");
            var startH = new HarmonyMethod(AccessTools.Method(typeof(Hooks), nameof(StartH)));
            var endH = new HarmonyMethod(AccessTools.Method(typeof(Hooks), nameof(EndH)));
            var harmony = new Harmony(typeof(Hooks).FullName);
            harmony.Patch(AccessTools.Method(twosome, "Awake"), postfix: startH);
            harmony.Patch(AccessTools.Method(twosome, "Unload"), prefix: endH);
        }

        public static void StartH() =>
            CoreConfig.ManagerObject.GetComponent<InsultOrderGame>().StartH();

        public static void EndH() =>
            CoreConfig.ManagerObject.GetComponent<InsultOrderGame>().EndH();
    }
}