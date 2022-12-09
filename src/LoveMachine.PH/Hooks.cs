﻿using BepInEx.Bootstrap;
using HarmonyLib;
using LoveMachine.Core;

namespace LoveMachine.PH
{
    internal static class Hooks
    {
        public static void InstallHooks() => Harmony.CreateAndPatchAll(typeof(HSceneTriggers));

        private static class HSceneTriggers
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(H_Scene), nameof(H_Scene.Awake))]
            public static void StartH(H_Scene __instance) =>
                CoreConfig.ManagerObject.GetComponent<PlayHomeGame>().OnStartH(__instance);

            [HarmonyPrefix]
            [HarmonyPatch(typeof(H_Scene), nameof(H_Scene.Exit))]
            public static void EndH() =>
                CoreConfig.ManagerObject.GetComponent<PlayHomeGame>().EndH();
        }
    }
}