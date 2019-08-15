using System;
using UnityEngine;
using System.Runtime.Serialization;
using STRINGS;
using KSerialization;

namespace BrisInfiniteSources
{
	[SerializationConfig(MemberSerialization.OptIn)]
    public class InfiniteSource : KMonoBehaviour, ISaveLoadable, ISingleSliderControl
    {
        [MyCmpGet]
        internal Storage storage;
        private static StatusItem filterStatusItem = null;
        public const int MinAllowedTemperature = 1;
		public const int MaxAllowedTemperature = 7500;
        [MyCmpGet]
        protected Operational operational;
        [SerializeField]
		public ConduitType Type;
        [Serialize]
		public float Flow = 10000f;
        [Serialize]
		public float Temp = 300f;
        [Serialize]
		public Tag FilteredTag;
        private Filterable filterable = null;
		private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;
		private int outputCell = -1;
        public SimHashes FilteredElement { get; private set; } = SimHashes.Void;
        private ISingleSliderControl mySlider;
            

        private bool inUpdate = false;
        protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			filterable = GetComponent<Filterable>();
            accumulator = Game.Instance.accumulators.Add("Source", this);
			InitializeStatusItems();
		}
        protected override void OnSpawn()
		{
            base.OnSpawn();
            operational.SetActive(operational.IsOperational, false);
            var building = GetComponent<Building>();
			outputCell = building.GetUtilityOutputCell();
            Conduit.GetFlowManager(Type).AddConduitUpdater(ConduitUpdate);
            mySlider = (ISingleSliderControl)this;
            
            OnFilterChanged(ElementLoader.FindElementByHash(FilteredElement).tag);
			filterable.onFilterChanged += new Action<Tag>(OnFilterChanged);
            
            GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, filterStatusItem, this);
		}

		protected override void OnCleanUp()
		{
			Conduit.GetFlowManager(Type).RemoveConduitUpdater(ConduitUpdate);
			Game.Instance.accumulators.Remove(accumulator);
			base.OnCleanUp();
		}

        private bool IsValidFilter
        {
            get
            {
                return (FilteredTag != null) && (FilteredElement != SimHashes.Void)
                    && (FilteredElement != SimHashes.Vacuum);
            }

        }

		private bool IsOperational
		{
			get
			{ Debug.Log("Is Operational: " + (IsValidFilter && GetComponent<Operational>().IsOperational) + "  IsValidFilter: " + IsValidFilter + "2nd IsOperational: " + GetComponent<Operational>().IsOperational);
                return IsValidFilter && GetComponent<Operational>().IsOperational;
			}
		}

		public string SliderTitleKey
		{
			get
			{
				switch (Type)
				{
					case ConduitType.Gas:
						return "STRINGS.UI.UISIDESCREENS.GASSOURCE.TITLE";
					case ConduitType.Liquid:
						return "STRINGS.UI.UISIDESCREENS.LIQUIDSOURCE.TITLE";
                    case ConduitType.Solid:
                        return "STRINGS.UI.UISIDESCREENS.SOLIDSOURCE.TITLE";

                    default:
						throw new Exception("Invalid ConduitType provided to InfiniteSource: " + Type.ToString());
				}
			}
		}

		public string SliderUnits
		{
			get
			{
                return UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			}
		}

       

		private void OnFilterChanged(Tag tag)
		{
			FilteredTag = tag;
			Element element = ElementLoader.GetElement(FilteredTag);
			if (element != null)
			{
				FilteredElement = element.id;
			}
			GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, !IsValidFilter, null);
			Temp = Math.Max(Temp, element.lowTemp);
			Temp = Math.Min(Temp, element.highTemp);
			Temp = Math.Max(Temp, MinAllowedTemperature);
			Temp = Math.Min(Temp, MaxAllowedTemperature);
            mySlider.SetSliderValue(Temp, -1);
			if (DetailsScreen.Instance != null && !inUpdate)
			{
				inUpdate = true;
				try
				{
					DetailsScreen.Instance.Refresh(gameObject);
				}
				catch (Exception) { }
				inUpdate = false;
			}
		}

		[OnDeserialized]
		private void OnDeserialized()
		{
			if (ElementLoader.GetElement(FilteredTag) == null)
				return;
			filterable.SelectedTag = FilteredTag;
			OnFilterChanged(FilteredTag);
		}

		private void InitializeStatusItems()
		{
			if (filterStatusItem != null)
				return;
            filterStatusItem = new StatusItem("Filter", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.LiquidConduits.ID, true, 129022);
            filterStatusItem.resolveStringCallback = (str, data) =>
            {
                InfiniteSource infiniteSource = (InfiniteSource)data;
                if (infiniteSource.FilteredElement == SimHashes.Void)
                {
                    str = string.Format(STRINGS.BUILDINGS.PREFABS.GASFILTER.STATUS_ITEM, STRINGS.BUILDINGS.PREFABS.GASFILTER.ELEMENT_NOT_SPECIFIED);
                }
                else
                {
                    Element elementByHash = ElementLoader.FindElementByHash(infiniteSource.FilteredElement);
                    str = string.Format(STRINGS.BUILDINGS.PREFABS.GASFILTER.STATUS_ITEM, elementByHash.name);
                }
                return str;
            };
            filterStatusItem.conditionalOverlayCallback = new Func<HashedString, object, bool>(ShowInUtilityOverlay);
		}


		private bool ShowInUtilityOverlay(HashedString mode, object data)
		{
			bool flag = false;
			switch (Type)
			{
				case ConduitType.Gas:
					flag = mode == OverlayModes.GasConduits.ID;
					break;
                case ConduitType.Liquid:
                    flag = mode == OverlayModes.LiquidConduits.ID;
                    break;
                case ConduitType.Solid:
                    flag = mode == OverlayModes.SolidConveyor.ID;
                    break;
            }
			return flag;
		}

        private void ConduitUpdate(float dt)
        {
            operational.SetActive(operational.IsOperational, false);

            if (Type == ConduitType.Solid)
            {
                var sFlow = SolidConduit.GetFlowManager();
                if (sFlow == null || !sFlow.HasConduit(outputCell) || !IsOperational)
                {
                    
                    return;
                }
                storage.AddOre(FilteredElement, Flow / InfiniteSourceFlowControl.GramsPerKilogram, Temp, 0, 0, false, false);
                sFlow.GetContents(outputCell);


                return;
              
            }
            else
            {
                var flowManager = Conduit.GetFlowManager(Type);
                if (flowManager == null || !flowManager.HasConduit(outputCell) || !IsOperational)
                {
                    return;
                }
                var delta = flowManager.AddElement(outputCell, FilteredElement, Flow / InfiniteSourceFlowControl.GramsPerKilogram, Temp, 0, 0);
                Game.Instance.accumulators.Accumulate(accumulator, delta);
            }
		}
        int ISliderControl.SliderDecimalPlaces(int index)
        {
            return 1;
        }

        float ISliderControl.GetSliderMin(int index)
        {
            Element element = ElementLoader.GetElement(FilteredTag);
            if (element == null)
            {
                return 0.0f;
            }
            return Math.Max(element.lowTemp, MinAllowedTemperature);
        }

        float ISliderControl.GetSliderMax(int index)
        {
            Element element = ElementLoader.GetElement(FilteredTag);
            if (element == null)
            {
                return 100.0f;
            }
            return Math.Min(element.highTemp, MaxAllowedTemperature);
        }

        float ISliderControl.GetSliderValue(int index)
        {
            return Temp;
        }

        void ISliderControl.SetSliderValue(float percent, int index)
        {
            Temp = percent;
        }

        string ISliderControl.GetSliderTooltipKey(int index)
        {
            switch (Type)
            {
                case ConduitType.Gas:
                    return "STRINGS.UI.UISIDESCREENS.GASSOURCE.TOOLTIP";
                case ConduitType.Liquid:
                    return "STRINGS.UI.UISIDESCREENS.LIQUIDSOURCE.TOOLTIP";
                case ConduitType.Solid:
                    return "STRINGS.UI.UISIDESCREENS.SOLIDSOURCE.TOOLTIP";
                default:
                    throw new Exception("Invalid ConduitType provided to InfiniteSource: " + Type.ToString());
            }
        }

        string ISliderControl.GetSliderTooltip()
        {
            switch (Type)
            {
                case ConduitType.Gas:
                    return "STRINGS.UI.UISIDESCREENS.GASSOURCE.TOOLTIP";
                case ConduitType.Liquid:
                    return "STRINGS.UI.UISIDESCREENS.LIQUIDSOURCE.TOOLTIP";
                case ConduitType.Solid:
                    return "STRINGS.UI.UISIDESCREENS.SOLIDSOURCE.TOOLTIP";
                default:
                    throw new Exception("Invalid ConduitType provided to InfiniteSource: " + Type.ToString());
            }
        }
    }
}
