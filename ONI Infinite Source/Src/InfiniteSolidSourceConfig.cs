using TUNING;
using UnityEngine;
using System.Collections.Generic;

using KSerialization;
namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class InfiniteSolidSourceConfig : IBuildingConfig
	{
		public const string Id = "BriSolidSource";
		public const string DisplayName = "Bri's Infinite Solid Source";
		public const string Description = "Materializes solid matter from the void.";
		public const string Effect = "Where is all this matter coming from?";
		public const string TemperatureSliderTitle = "Matter ouput temperature";
		public const string TemperatureSliderTooltip = "Matter output temperature";

		public override BuildingDef CreateBuildingDef()
		{
			var buildingDef = BuildingTemplates.CreateBuildingDef(
				id: Id,
				width: 1,
				height: 2,
				anim: "conveyorin_kanim",
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
			buildingDef.OutputConduitType = ConduitType.Solid;
			buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
			buildingDef.AudioCategory = "HollowMetal";
			buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
            buildingDef.PermittedRotations = PermittedRotations.R360;
			return buildingDef;
            
		}

		public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
		{
            go.AddOrGet<Filterable>().filterElementState = Filterable.ElementState.Solid;
            
            go.AddOrGet<InfiniteSourceFlowControl>();
			go.AddOrGet<InfiniteSource>().Type = ConduitType.Solid;

            SoundEventVolumeCache.instance.AddVolume("conveyorin_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.allowItemRemoval = false;
            storage.showDescriptor = true;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            List<Tag> tagList = new List<Tag>();
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.NOT_EDIBLE_SOLIDS);
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.FOOD);
            storage.storageFilters = tagList;
            CopyBuildingSettings copyBuildingSettings = go.AddOrGet<CopyBuildingSettings>();
            copyBuildingSettings.copyGroupTag = GameTags.StorageLocker;
            go.AddOrGet<SolidConduitDispenser>();
        }

        
        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_1);
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGet<Operational>();
            go.AddOrGetDef<StorageController.Def>();

            Object.DestroyImmediate(go.GetComponent<RequireInputs>());
            Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
            Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
            BuildingTemplates.DoPostConfigure(go);
        }
    }
}


