﻿using BepInEx;
using LoveMachine.Core;

namespace LoveMachine.AGH
{
    [BepInPlugin(CoreConfig.GUID, CoreConfig.PluginName, CoreConfig.Version)]
    internal class AGHLoveMachine : BaseUnityPlugin
    {
        private void Start()
        {
            this.Initialize<HoukagoRinkanChuudokuGame>(
                logger: Logger,
                girlMappingHeader: null,
                girlMappingOptions: null);
            Hooks.InstallHooks();
        }
    }
}
