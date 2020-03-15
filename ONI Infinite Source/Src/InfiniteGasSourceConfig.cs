using TUNING;
using UnityEngine;
using KSerialization;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class InfiniteGasSourceConfig : IBuildingConfig
	{
		public const string Id = "BriGasSource";
		public const string DisplayName = "Bri's Infinite Gas Source";
		public const string Description = "Materializes gas from the void.";
		public const string Effect = "Where is all the gas coming from?";
		public const string TemperatureSliderTitle = "Gas ouput temperature";
		public const string TemperatureSliderTooltip = "Gas output temperature";

		public override BuildingDef CreateBuildingDef()
		{
			var buildingDef = BuildingTemplates.CreateBuildingDef(
				id: Id,
				width: 1,
				height: 2,
				anim: "minigaspump_kanim",
				hitpoints: BUILDINGS.HITPOINTS.TIER2,
				construction_time: BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0,
				construction_mass: BUILDINGS.CONSTRUCTION_MASS_KG.TIER0,
				construction_materials: MATERIALS.ALL_METALS,
				melting_point: BUILDINGS.MELTING_POINT_KELVIN.TIER0,
				build_location_rule: BuildLocationRule.Conduit,
                decor: BUILDINGS.DECOR.PENALTY.TIER1,
				noise: NOISE_POLLUTION.NOISY.TIER0,
				0.2f
				);
			buildingDef.OutputConduitType = ConduitType.Gas;
			buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.ViewMode = OverlayModes.GasConduits.ID;
			buildingDef.AudioCategory = "HollowMetal";
            
			buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
            buildingDef.PermittedRotations = PermittedRotations.R360;
			buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
			return buildingDef;
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
			go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Gas;
			go.AddOrGet<InfiniteSourceFlowControl>();
			go.AddOrGet<InfiniteSource>().Type = ConduitType.Gas;
		}



        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGet<Operational>();
            go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Gas;
            go.AddOrGet<InfiniteSourceFlowControl>();
            go.AddOrGet<InfiniteSource>().Type = ConduitType.Gas;
            Object.DestroyImmediate(go.GetComponent<RequireInputs>());
            Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
            Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
            BuildingTemplates.DoPostConfigure(go);
        }
    }
}
