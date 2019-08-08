using STRINGS;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace BrisInfiniteSources
{
    public class ConveyorDuplicatorConfig : IBuildingConfig
    {
        public static string Effect = "1 In, 2 Out.  Thats Maths!!!";
        private readonly ConduitPortInfo _secondaryPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(0, 0));
        public const string Id = "ConveyorDuplicator";
        public const string DisplayName = "Conveyor Duplicator";
        public const string Description = "Duplicated any materials put through it";

        public override BuildingDef CreateBuildingDef()
        {
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("ConveyorDuplicator", 3, 1, "utilities_conveyorbridge_kanim", TUNING.BUILDINGS.HITPOINTS.TIER3, TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0, TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0, MATERIALS.REFINED_METALS, 1600f, BuildLocationRule.Anywhere, TUNING.BUILDINGS.DECOR.NONE, TUNING.NOISE_POLLUTION.NONE, 0.2f);
            buildingDef.InputConduitType = ConduitType.Solid;
            buildingDef.OutputConduitType = ConduitType.Solid;
            buildingDef.Floodable = false;
            buildingDef.Entombable = false;
            buildingDef.ViewMode = OverlayModes.SolidConveyor.ID;
            
            buildingDef.RequiresPowerInput = false;
            buildingDef.EnergyConsumptionWhenActive = 0f;
            buildingDef.ExhaustKilowattsWhenActive = 0.0f;
            buildingDef.SelfHeatKilowattsWhenActive = 0f;
            buildingDef.AudioCategory = "Metal";
            buildingDef.AudioSize = "small";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.PermittedRotations = PermittedRotations.R360;
            buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
            buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
            GeneratedBuildings.RegisterWithOverlay(OverlayScreen.SolidConveyorIDs, "ConveyorDuplicator");
            return buildingDef;
        }

        private void AttachPort(GameObject go)
        {
            go.AddComponent<ConduitSecondaryOutput>().portInfo = this._secondaryPort;
        }

        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
            this.AttachPort(go);
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
        }

        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            this.AttachPort(go);
            go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.ConveyorBuild.Id;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            BuildingTemplates.DoPostConfigure(go);
            Prioritizable.AddRef(go);
            go.AddOrGet<EnergyConsumer>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            List<Tag> tagList = new List<Tag>();
            
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.BAGABLE_CREATURES);
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.NOT_EDIBLE_SOLIDS);
            tagList.AddRange((IEnumerable<Tag>)STORAGEFILTERS.FOOD);
            Storage storage = go.AddOrGet<Storage>();
            storage.capacityKg = 10000.0f;
            storage.showInUI = true;
            storage.showDescriptor = true;
            storage.storageFilters = tagList;
            storage.allowItemRemoval = false;
            storage.onlyTransferFromLowerPriority = false;
            go.AddOrGet<Automatable>();
            go.AddOrGet<ConveyorDuplicator>().SecondaryPort = this._secondaryPort;
        }
    }
}
