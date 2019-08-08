using KSerialization;
using UnityEngine;
using TUNING;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class BrisArtConfig : IBuildingConfig
    {
        public const string ID = "BrisArt";
        public const string DisplayName = "Bri's Super Art";
        public const string Description = "Because Everyone Loves Art!";
        public const string Effect = "I can see it from all the way over there!";

        public override BuildingDef CreateBuildingDef()
        {
            int width = 2;
            int height = 2;
            string anim = "painting_kanim";
            int hitpoints = TUNING.BUILDINGS.HITPOINTS.TIER4;
            float c_time = TUNING.BUILDINGS.CONSTRUCTION_TIME_SECONDS.TIER0;
            float[] c_mass = TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
            string[] c_mats = MATERIALS.RAW_MINERALS;
            float melting_point = BUILDINGS.MELTING_POINT_KELVIN.TIER4;
            BuildLocationRule b_loc = BuildLocationRule.Anywhere;
            EffectorValues myArt = new EffectorValues() { amount = 220, radius = 50 };
            EffectorValues noisy = NOISE_POLLUTION.NONE;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(ID, width, height, anim, hitpoints, c_time, c_mass, c_mats, melting_point, b_loc, myArt, noisy, 0.2f);
            buildingDef.Floodable = false;
            buildingDef.SceneLayer = Grid.SceneLayer.InteriorWall;
            buildingDef.Overheatable = false;
            buildingDef.AudioCategory = "Metal";
            buildingDef.BaseTimeUntilRepair = -1f;
            buildingDef.ViewMode = OverlayModes.Decor.ID;
            buildingDef.DefaultAnimState = "off";
            buildingDef.PermittedRotations = PermittedRotations.FlipH;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
            go.AddOrGet<BrisArt>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
        }
    }
}
