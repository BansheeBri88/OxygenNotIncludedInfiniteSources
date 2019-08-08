using KSerialization;
using UnityEngine;
using TUNING;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class BrisArtNightLightConfig : IBuildingConfig
    {
        public const string ID = "BrisArtNightLight";
        public const string DisplayName = "Bri's Night Light";
        public const string Description = "Because Everyone Loves Salt Lamps!";
        public const string Effect = "I can see it from all the way over there!";

        public override BuildingDef CreateBuildingDef()
        {
            int width = 1;
            int height = 2;
            string anim = "lava_lamp_kanim";
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
            buildingDef.PermittedRotations = PermittedRotations.R360;
            return buildingDef;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
            go.AddOrGet<BrisArt>();
        }
        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
            lightShapePreview.lux = 0;
            lightShapePreview.radius = 8f;
            lightShapePreview.shape = LightShape.Circle;
            lightShapePreview.offset = new CellOffset((int)def.BuildingComplete.GetComponent<Light2D>().Offset.x, (int)def.BuildingComplete.GetComponent<Light2D>().Offset.y);
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            Light2D light2D = go.AddOrGet<Light2D>();
            light2D.Lux = 1;
            light2D.overlayColour = LIGHT2D.PLASMALAMP_OVERLAYCOLOR;
            light2D.Color = LIGHT2D.PLASMALAMP_COLOR;
            
            light2D.Range = 8f;
            light2D.Angle = 0.0f;
            light2D.Direction = LIGHT2D.PLASMALAMP_DIRECTION;
            light2D.Offset = LIGHT2D.PLASMALAMP_OFFSET;
            light2D.shape = LightShape.Circle;
            light2D.drawOverlay = true;
            go.AddOrGetDef<LightController.Def>();
        }
    }
}
