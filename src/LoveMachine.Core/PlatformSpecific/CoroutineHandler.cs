﻿using System;
using System.Collections;
using BepInEx.Logging;
using LoveMachine.Core.Util;
using UnityEngine;

namespace LoveMachine.Core.PlatformSpecific
{
    public class CoroutineHandler : MonoBehaviour
    {
        protected static ManualLogSource Logger => Globals.Logger;
        
        protected Coroutine HandleCoroutine(IEnumerator coroutine,
            bool suppressExceptions = false) =>
            StartCoroutine(CoroutineUtil.HandleExceptions(coroutine, suppressExceptions, Logger));
    }
}