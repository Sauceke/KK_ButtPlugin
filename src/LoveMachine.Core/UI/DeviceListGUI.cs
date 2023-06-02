﻿using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using UnityEngine;

namespace LoveMachine.Core
{
    internal class DeviceListGUI
    {
        private static List<Device> cachedDeviceList = new List<Device>();
        
        private static float testPosition;
        
        public static void DeviceListDrawer(ConfigEntryBase entry)
        {
            var serverController = Globals.ManagerObject.GetComponent<ButtplugWsClient>();
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            {
                GUILayout.BeginHorizontal();
                {
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Connect", GUILayout.Width(150)))
                    {
                        serverController.Connect();
                    }
                    GUI.enabled = serverController.IsConnected;
                    if (GUILayout.Button("Scan", GUILayout.Width(150)))
                    {
                        serverController.StartScan();
                    }
                    GUI.enabled = true;
                    GUILayout.FlexibleSpace();
                }
                GUILayout.EndHorizontal();
                GUIUtil.SingleSpace();
                // imgui doesn't expect the layout to change outside of layout events
                if (Event.current.type == EventType.Layout)
                {
                    cachedDeviceList = serverController.Devices;
                }
                foreach (var device in cachedDeviceList)
                {
                    DrawDevicePanel(device);
                }
                if (DeviceListConfig.ShowOfflineDevices.Value)
                {
                    DrawOfflineDeviceList(cachedDeviceList);
                }
            }
            GUILayout.EndVertical();
        }

        private static void DrawDevicePanel(Device device)
        {
            GUILayout.BeginVertical(GetDevicePanelStyle());
            {
                device.Draw();
                GUILayout.BeginHorizontal();
                {
                    GUIUtil.LabelWithTooltip("Test", "Test this device");
                    GUILayout.HorizontalSlider(testPosition, 0f, 1f);
                    GUIUtil.SingleSpace();
                    if (GUILayout.Button("Test", GUILayout.ExpandWidth(false)))
                    {
                        TestDevice(device);
                    }
                }
                GUILayout.EndHorizontal();
                GUIUtil.SingleSpace();
            }
            GUILayout.EndVertical();
            GUIUtil.SingleSpace();
        }

        private static void DrawOfflineDeviceList(List<Device> onlineDevices)
        {
            var settings = DeviceManager.DeviceSettings;
            foreach (var setting in settings)
            {
                if (onlineDevices.Any(device => device.Matches(setting)))
                {
                    continue;
                }
                GUILayout.BeginVertical(GetOfflineDevicePanelStyle());
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label($"{setting.DeviceName} (Offline)");
                        GUILayout.FlexibleSpace();
                    }
                    GUILayout.EndHorizontal();
                    GUIUtil.SingleSpace();
                    setting.Draw();
                }
                GUILayout.EndVertical();
                GUIUtil.SingleSpace();
            }
            DeviceManager.DeviceSettings = settings;
        }

        private static void TestDevice(Device device) => Array.ForEach(
            Globals.ManagerObject.GetComponents<ClassicButtplugController>(),
            controller => controller.Test(device, pos => testPosition = pos));

        private static GUIStyle GetDevicePanelStyle() => new GUIStyle
        {
            margin = new RectOffset { left = 20, right = 20, top = 5, bottom = 5 },
            normal = new GUIStyleState { background = GetTexture(new Color(0f, 1f, 0.5f, 0.2f)) }
        };

        private static GUIStyle GetOfflineDevicePanelStyle() => new GUIStyle
        {
            margin = new RectOffset { left = 20, right = 20, top = 5, bottom = 5 },
            normal = new GUIStyleState { background = GetTexture(new Color(1f, 0f, 0.2f, 0.2f)) }
        };

        private static Texture2D GetTexture(Color color)
        {
            var texture = new Texture2D(1, 1);
            texture.SetPixels(new[] { color });
            texture.Apply();
            return texture;
        }
    }
}