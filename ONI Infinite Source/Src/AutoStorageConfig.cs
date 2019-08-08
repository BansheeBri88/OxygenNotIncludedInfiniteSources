using KSerialization;
using UnityEngine;
using TUNING;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class AutoStorageConfig : IBuildingConfig
    {
        public const string ID = "AutoStorage";
        public const string DisplayName = "Bri's Always Stocked Storage Locker";
        public const string Description = "All the Items!.";
        public const string Effect = "Everytime you open it, theres more!";
       
        public override BuildingDef CreateBuildingDef()
        {
            int width = 1;
            int height = 2;
            string anim = "storagelocker_kanim";
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
            return buildingDef;
        }
        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.allowItemRemoval = true;
            storage.showDescriptor = true;
            storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            go.AddOrGet<AutoStorage>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }
    }
}