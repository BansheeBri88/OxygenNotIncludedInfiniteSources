using KSerialization;
using UnityEngine;
using TUNING;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class AutoFridgeConfig : IBuildingConfig
    {
        private static readonly LogicPorts.Port OUTPUT_PORT = LogicPorts.Port.OutputPort(FilteredStorage.FULL_PORT_ID, new CellOffset(0, 1), "Is Auto Fridge Full", "Auto Fridge Full", "Auto Fridge Not Full", false, false);
        public const string ID = "AutoFridge";
        public const string DisplayName = "Bri's Always Stocked Fridge";
        public const string Description = "Yay Moar Foooodzzzz!.";
        public const string Effect = "Everytime you open it, theres more!";
       
        public override BuildingDef CreateBuildingDef()
        {
            int width = 1;
            int height = 2;
            string anim = "fridge_kanim";
            int hitpoints = TUNING.BUILDINGS.HITPOINTS.TIER4;
            float construction_time = TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;
            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
            string[] rawMinerals = MATERIALS.RAW_MINERALS;
            float melting_point = 800f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues nONE = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tieR4, rawMinerals, melting_point, build_location_rule, BUILDINGS.DECOR.BONUS.TIER5, nONE, 0.2f);
            buildingDef.Floodable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.Overheatable = false;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_open", TUNING.NOISE_POLLUTION.NOISY.TIER1);
            SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_close", TUNING.NOISE_POLLUTION.NOISY.TIER1);
            buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            //go.AddOrGet<Refrigerator>();
            go.AddOrGet<AutoFridge>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = STORAGEFILTERS.FOOD;
            storage.allowItemRemoval = true;
            storage.capacityKg = 100f;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            Prioritizable.AddRef(go);
            go.AddOrGet<TreeFilterable>();
            go.AddOrGet<StorageLocker>();
            go.AddComponent<Refrigerator>();
            
            go.AddOrGet<AutoFridge>();
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGetDef<StorageController.Def>();
        }
    }
}