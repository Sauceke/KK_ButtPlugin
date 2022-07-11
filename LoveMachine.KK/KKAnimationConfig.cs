﻿using BepInEx;
using BepInEx.Configuration;

namespace LoveMachine.KK
{
    public static class KKAnimationConfig
    {
        public static ConfigEntry<bool> ReduceAnimationSpeeds { get; private set; }
        public static ConfigEntry<bool> SuppressAnimationBlending { get; private set; }

        public static void Initialize(BaseUnityPlugin plugin)
        {
            string animationSettingsTitle = "Animation Settings";
            ReduceAnimationSpeeds = plugin.Config.Bind(
                section: animationSettingsTitle,
                key: "Reduce animation speeds",
                defaultValue: true,
                "Whether to slow down animations to a speed your stroker can handle");
            SuppressAnimationBlending = plugin.Config.Bind(
                section: animationSettingsTitle,
                key: "Simplify animations",
                defaultValue: true,
                "Some animations are too complex and cannot be tracked precisely.\n" +
                "This setting will make such animations simpler for better immersion.");
        }
    }
}
