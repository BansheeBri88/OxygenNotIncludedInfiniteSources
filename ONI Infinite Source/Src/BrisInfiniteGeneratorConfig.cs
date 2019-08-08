using KSerialization;
using UnityEngine;
using TUNING;
using System.Collections.Generic;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class BrisInfiniteGeneratorConfig : IBuildingConfig
    {
        public const string ID = "BrisInfiniteGenerator";
        public const string DisplayName = "Bri's Infinite Generator";
        public const string Description = "1.21 JiggaWatts!!!!!";
        public const string Effect = "Doc Brown would be Proud!!!!";
       
        public override BuildingDef CreateBuildingDef()
        {


            string id = "BrisInfiniteGenerator";
            int width = 1;
            int height = 2;
            string anim = "batterysm_kanim";
            int hitpoints = TUNING.BUILDINGS.HITPOINTS.TIER4;
            float construction_time = TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;
            float[] tieR4 = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
            string[] rawMinerals = MATERIALS.RAW_MINERALS;
            float melting_point = 800f;
            BuildLocationRule build_location_rule = BuildLocationRule.Conduit;
            EffectorValues nONE = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, construction_time, tieR4, rawMinerals, melting_point, build_location_rule, BUILDINGS.DECOR.PENALTY.TIER1, nONE, 0.2f);
            buildingDef.GeneratorWattageRating = 1210000000f;
            buildingDef.GeneratorBaseCapacity = 20000f;
            buildingDef.ExhaustKilowattsWhenActive = 0f;
            buildingDef.SelfHeatKilowattsWhenActive = 0f;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.ViewMode = OverlayModes.Power.ID;
            buildingDef.Overheatable = false;
            buildingDef.RequiresPowerInput = false;
            buildingDef.RequiresPowerOutput = true;
            buildingDef.Entombable = false;
            buildingDef.PowerOutputOffset = new CellOffset(0, 0);
            SoundEventVolumeCache.instance.AddVolume("batterysm_kanim", "Battery_sm_rattle", TUNING.NOISE_POLLUTION.NOISY.TIER2);
            
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
            energyGenerator.formula = EnergyGenerator.CreateSimpleFormula(SimHashes.Water.CreateTag(), 1f, 10f, SimHashes.Water, 1f, true, new CellOffset(0, 0), 0f);
            energyGenerator.meterOffset = Meter.Offset.Behind;
            energyGenerator.powerDistributionOrder = 15;
            go.AddOrGet<LoopingSounds>();
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 10f;

            storage.showInUI = true;
            storage.allowItemRemoval = false;
            storage.showDescriptor = true;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            Tinkerable.MakePowerTinkerable(go);
            go.AddOrGet<BrisInfiniteGenerator>();
            
            
        }
        public override void DoPostConfigureComplete(GameObject go)
        {
            
            GeneratedBuildings.RegisterLogicPorts(go, LogicOperationalController.INPUT_PORTS_0_0);
            go.AddOrGet<LogicOperationalController>();
            go.AddOrGetDef<PoweredActiveController.Def>();
            BuildingTemplates.DoPostConfigure(go);
        }

    }
}