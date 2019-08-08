using System;
using System.Collections.Generic;
using Harmony;
using UnityEngine;
using UnityEngine.UI;

namespace BrisInfiniteSources
{
	public class InfiniteSourceSinkPatches
	{

		[HarmonyPatch(typeof(GeneratedBuildings))]
		[HarmonyPatch("LoadGeneratedBuildings")]
		public class GeneratedBuildings_LoadGeneratedBuildings_Patch
		{
			private static void Prefix()
			{
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSinkConfig.Id.ToUpperInvariant()}.NAME", InfiniteLiquidSinkConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSinkConfig.Id.ToUpperInvariant()}.DESC", InfiniteLiquidSinkConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSinkConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteLiquidSinkConfig.Effect);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSourceConfig.Id.ToUpperInvariant()}.NAME", InfiniteLiquidSourceConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSourceConfig.Id.ToUpperInvariant()}.DESC", InfiniteLiquidSourceConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteLiquidSourceConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteLiquidSourceConfig.Effect);

				Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteLiquidSourceConfig.Id.ToUpperInvariant()}.TITLE", InfiniteLiquidSourceConfig.TemperatureSliderTitle);
				Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteLiquidSourceConfig.Id.ToUpperInvariant()}.TOOLTIP", InfiniteLiquidSourceConfig.TemperatureSliderTooltip);

				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSinkConfig.Id.ToUpperInvariant()}.NAME", InfiniteGasSinkConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSinkConfig.Id.ToUpperInvariant()}.DESC", InfiniteGasSinkConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSinkConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteGasSinkConfig.Effect);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSourceConfig.Id.ToUpperInvariant()}.NAME", InfiniteGasSourceConfig.DisplayName);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSourceConfig.Id.ToUpperInvariant()}.DESC", InfiniteGasSourceConfig.Description);
				Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteGasSourceConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteGasSourceConfig.Effect);

				Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteGasSourceConfig.Id.ToUpperInvariant()}.TITLE", InfiniteGasSourceConfig.TemperatureSliderTitle);
				Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteGasSourceConfig.Id.ToUpperInvariant()}.TOOLTIP", InfiniteGasSourceConfig.TemperatureSliderTooltip);

                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSinkConfig.Id.ToUpperInvariant()}.NAME", InfiniteSolidSinkConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSinkConfig.Id.ToUpperInvariant()}.DESC", InfiniteSolidSinkConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSinkConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteSolidSinkConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSourceConfig.Id.ToUpperInvariant()}.NAME", InfiniteSolidSourceConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSourceConfig.Id.ToUpperInvariant()}.DESC", InfiniteSolidSourceConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{InfiniteSolidSourceConfig.Id.ToUpperInvariant()}.EFFECT", InfiniteSolidSourceConfig.Effect);
                Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteSolidSourceConfig.Id.ToUpperInvariant()}.TITLE", InfiniteSolidSourceConfig.TemperatureSliderTitle);
                Strings.Add($"STRINGS.UI.UISIDESCREENS.{InfiniteSolidSourceConfig.Id.ToUpperInvariant()}.TOOLTIP", InfiniteSolidSourceConfig.TemperatureSliderTooltip);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ConveyorDuplicatorConfig.Id.ToUpperInvariant()}.NAME", ConveyorDuplicatorConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ConveyorDuplicatorConfig.Id.ToUpperInvariant()}.DESC", ConveyorDuplicatorConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{ConveyorDuplicatorConfig.Id.ToUpperInvariant()}.EFFECT", ConveyorDuplicatorConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoFridgeConfig.ID.ToUpperInvariant()}.NAME", AutoFridgeConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoFridgeConfig.ID.ToUpperInvariant()}.DESC", AutoFridgeConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoFridgeConfig.ID.ToUpperInvariant()}.EFFECT", AutoFridgeConfig.Effect);

                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoStorageConfig.ID.ToUpperInvariant()}.NAME", AutoStorageConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoStorageConfig.ID.ToUpperInvariant()}.DESC", AutoStorageConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{AutoStorageConfig.ID.ToUpperInvariant()}.EFFECT", AutoStorageConfig.Effect);

                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteBatteryConfig.ID.ToUpperInvariant()}.NAME", BrisInfiniteBatteryConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteBatteryConfig.ID.ToUpperInvariant()}.DESC", BrisInfiniteBatteryConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteBatteryConfig.ID.ToUpperInvariant()}.EFFECT", BrisInfiniteBatteryConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteGeneratorConfig.ID.ToUpperInvariant()}.NAME", BrisInfiniteGeneratorConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteGeneratorConfig.ID.ToUpperInvariant()}.DESC", BrisInfiniteGeneratorConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisInfiniteGeneratorConfig.ID.ToUpperInvariant()}.EFFECT", BrisInfiniteGeneratorConfig.Effect);

                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtConfig.ID.ToUpperInvariant()}.NAME", BrisArtConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtConfig.ID.ToUpperInvariant()}.DESC", BrisArtConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtConfig.ID.ToUpperInvariant()}.EFFECT", BrisArtConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisLightConfig.ID.ToUpperInvariant()}.NAME", BrisLightConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisLightConfig.ID.ToUpperInvariant()}.DESC", BrisLightConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisLightConfig.ID.ToUpperInvariant()}.EFFECT", BrisLightConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtLightConfig.ID.ToUpperInvariant()}.NAME", BrisArtLightConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtLightConfig.ID.ToUpperInvariant()}.DESC", BrisArtLightConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtLightConfig.ID.ToUpperInvariant()}.EFFECT", BrisArtLightConfig.Effect);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtNightLightConfig.ID.ToUpperInvariant()}.NAME", BrisArtNightLightConfig.DisplayName);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtNightLightConfig.ID.ToUpperInvariant()}.DESC", BrisArtNightLightConfig.Description);
                Strings.Add($"STRINGS.BUILDINGS.PREFABS.{BrisArtNightLightConfig.ID.ToUpperInvariant()}.EFFECT", BrisArtNightLightConfig.Effect);


                Strings.Add($"STRINGS.UI.UISIDESCREENS.INFINITESOURCE.FLOW.TITLE", InfiniteSourceFlowControl.FlowTitle);
				Strings.Add($"STRINGS.UI.UISIDESCREENS.INFINITESOURCE.FLOW.TOOLTIP", InfiniteSourceFlowControl.FlowTooltip);

				ModUtil.AddBuildingToPlanScreen("Plumbing", InfiniteLiquidSinkConfig.Id);
				ModUtil.AddBuildingToPlanScreen("Plumbing", InfiniteLiquidSourceConfig.Id);
				ModUtil.AddBuildingToPlanScreen("HVAC", InfiniteGasSinkConfig.Id);
				ModUtil.AddBuildingToPlanScreen("HVAC", InfiniteGasSourceConfig.Id);
                ModUtil.AddBuildingToPlanScreen("Conveyance", InfiniteSolidSinkConfig.Id);
                ModUtil.AddBuildingToPlanScreen("Conveyance", InfiniteSolidSourceConfig.Id);
                ModUtil.AddBuildingToPlanScreen("Conveyance", ConveyorDuplicatorConfig.Id);
                ModUtil.AddBuildingToPlanScreen("Food", AutoFridgeConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Base", AutoStorageConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Power", BrisInfiniteBatteryConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Power", BrisInfiniteGeneratorConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Furniture", BrisArtConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Furniture", BrisLightConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Furniture", BrisArtLightConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Furniture", BrisArtNightLightConfig.ID);



            }
        }






        [HarmonyPatch(typeof(FilterSideScreen), nameof(FilterSideScreen.IsValidForTarget))]
        public class FilterSideScreen_IsValidForTarget
        {
            private static bool Prefix(GameObject target, FilterSideScreen __instance, ref bool __result)
            {
                if (target.GetComponent<InfiniteSourceFlowControl>() != null)
                {
                    __result = !__instance.isLogicFilter;
                    return false;
                }
                return true;
            }
        }

        [HarmonyPatch(typeof(Db))]
		[HarmonyPatch("Initialize")]
		public class Db_Initialize_Patch
		{
			private static void Prefix()
			{
				var liquid = new List<string>(Database.Techs.TECH_GROUPING["LiquidPiping"]) { InfiniteLiquidSinkConfig.Id, InfiniteLiquidSourceConfig.Id };
				Database.Techs.TECH_GROUPING["LiquidPiping"] = liquid.ToArray();

				var gas = new List<string>(Database.Techs.TECH_GROUPING["GasPiping"]) { InfiniteGasSinkConfig.Id, InfiniteGasSourceConfig.Id };
				Database.Techs.TECH_GROUPING["GasPiping"] = gas.ToArray();

                var solid = new List<string>(Database.Techs.TECH_GROUPING["SolidTransport"]) { InfiniteSolidSinkConfig.Id, InfiniteSolidSourceConfig.Id };
                Database.Techs.TECH_GROUPING["SolidTransport"] = solid.ToArray();
            }
            private static int GetVisualizerCell(Building building, CellOffset offset)
            {
                CellOffset rotatedOffset = building.GetRotatedOffset(offset);
                return Grid.OffsetCell(building.GetCell(), rotatedOffset);
            }

            private static void DrawUtilityIcon(
              ref Dictionary<GameObject, Image> icons,
              int cell,
              Sprite icon,
              ref GameObject visualizerObj,
              Color tint)
            {
                Vector3 posCcc = Grid.CellToPosCCC(cell, Grid.SceneLayer.Building);
                if (!visualizerObj.gameObject.activeInHierarchy)
                    visualizerObj.gameObject.SetActive(true);
                visualizerObj.GetComponent<Image>().enabled = true;
                icons[visualizerObj].raycastTarget = false;
                icons[visualizerObj].sprite = icon;
                visualizerObj.transform.GetChild(0).gameObject.GetComponent<Image>().color = tint;
                visualizerObj.transform.SetPosition(posCcc);
                if (!((UnityEngine.Object)visualizerObj.GetComponent<SizePulse>() == (UnityEngine.Object)null))
                    return;
                visualizerObj.transform.localScale = Vector3.one * 1.5f;
            }

            [HarmonyPatch(typeof(BuildingCellVisualizer))]
            [HarmonyPatch("DrawIcons")]
            public static class BuildingCellVisualizer_DrawIcons_Patch
            {
                public static void Postfix(BuildingCellVisualizer __instance, HashedString mode)
                {
                    Traverse traverse1 = Traverse.Create((object)__instance);
                    Building building = traverse1.Field("building").GetValue<Building>();
                    ISecondaryOutput component = building.Def.BuildingComplete.GetComponent<ISecondaryOutput>();
                    if (component == null || component.GetSecondaryConduitType() != ConduitType.Solid)
                        return;
                    BuildingCellVisualizerResources visualizerResources = traverse1.Field("resources").GetValue<BuildingCellVisualizerResources>();
                    Traverse traverse2 = traverse1.Field("icons");
                    Dictionary<GameObject, Image> icons = traverse2.GetValue<Dictionary<GameObject, Image>>();
                    Traverse traverse3 = traverse1.Field("secondaryOutputVisualizer");
                    if (icons == null)
                        return;
                    GameObject visualizerObj = traverse3.GetValue<GameObject>();
                    if ((UnityEngine.Object)visualizerObj == (UnityEngine.Object)null)
                    {
                        GameObject key = Util.KInstantiate(Assets.UIPrefabs.ResourceVisualizer, GameScreenManager.Instance.worldSpaceCanvas, (string)null);
                        key.transform.SetAsFirstSibling();
                        icons.Add(key, key.transform.GetChild(0).GetComponent<Image>());
                        traverse3.SetValue((object)key);
                    }
                    else
                    {
                        if (mode != OverlayModes.SolidConveyor.ID || !(bool)((UnityEngine.Object)building) || !(bool)((UnityEngine.Object)visualizerResources))
                            return;
                        CellOffset secondaryConduitOffset = component.GetSecondaryConduitOffset();
                        InfiniteSourceSinkPatches.Db_Initialize_Patch.DrawUtilityIcon(ref icons, InfiniteSourceSinkPatches.Db_Initialize_Patch.GetVisualizerCell(building, secondaryConduitOffset), visualizerResources.liquidOutputIcon, ref visualizerObj, (Color)BuildingCellVisualizer.secondOutputColour);
                        traverse3.SetValue((object)visualizerObj);
                        traverse2.SetValue((object)icons);
                    }
                }
            }
        
    

        }

		[HarmonyPatch(typeof(KSerialization.Manager))]
		[HarmonyPatch("GetType")]
		[HarmonyPatch(new[] { typeof(string) })]
		public static class InfiniteSourceSinkSerializationPatch
		{
			public static void Postfix(string type_name, ref Type __result)
			{
				if (type_name == "BrisInfiniteSources.InfiniteSink")
				{
					__result = typeof(InfiniteSink);
				}
				else if (type_name == "BrisInfiniteSources.InfiniteSource")
				{
					__result = typeof(InfiniteSource);
				}
			}
		}
	}

}
