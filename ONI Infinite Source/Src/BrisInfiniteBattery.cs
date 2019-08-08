using Harmony;
using System;


namespace BrisInfiniteSources
{
    [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
    internal class BrisInfiniteBattery_GeneratedBuildings_LoadGeneratedBuildings
    {
       
        [HarmonyPatch(typeof(Db), "Initialize")]
        internal class BrisInfiniteBattery_Db_Initialize
        {
            private static void Prefix(Db __instance)
            {
                Debug.Log(" Bri's Infinite Battery Loaded ");
            }
        }
        [HarmonyPatch(typeof(Battery), "ConsumeEnergy", new Type[] { typeof(float), typeof(bool) })]
        internal class BrisInfiniteBattery_Battery_ConsumeEnergy
        {
            private static bool Prefix(Battery __instance)
            {
                if (__instance.gameObject.GetComponent<KPrefabID>().PrefabTag == BrisInfiniteBatteryConfig.ID)
                {
                    return false;
                }
                return true;
            }
        }
        [HarmonyPatch(typeof(Battery), "OnSpawn")]
        internal class BrisInfiniteBattery_Battery_OnSpawn
        {
            private static void Prefix(Battery __instance)
            {
                Debug.Log("Bri's InfiniteBattery On Spawn");
                if (__instance.gameObject.GetComponent<KPrefabID>().PrefabTag == BrisInfiniteBatteryConfig.ID)
                {
                    AccessTools.Field(typeof(Battery), "joulesAvailable").SetValue(__instance, 40000f);
                }
            }
        }
    }
}