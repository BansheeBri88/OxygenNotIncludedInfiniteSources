using System;
using Harmony;
using STRINGS;
using KSerialization;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class InfiniteSourceFlowControl : KMonoBehaviour, IIntSliderControl
	{
		public const string FlowTitle = "Flow rate";
		public const string FlowTooltip = "Flow rate";
		public const int GramsPerKilogram = 1000;
        private IIntSliderControl mySlider;


		string ISliderControl.SliderUnits
        {
            get { return UI.UNITSUFFIXES.MASS.GRAM + "/" + UI.UNITSUFFIXES.SECOND; }
        }



        float ISliderControl.GetSliderMax(int index)
		{
			var flowManager = Conduit.GetFlowManager(GetComponent<InfiniteSource>().Type);
			return Traverse.Create(flowManager).Field("MaxMass").GetValue<float>() * GramsPerKilogram;
		}

		float ISliderControl.GetSliderMin(int index)
		{
			return 0;
		}

        string ISliderControl.SliderTitleKey
        {
            get
            {
                return "STRINGS.UI.UISIDESCREENS.INFINITESOURCE.FLOW.TITLE";
            }
        }


        string ISliderControl.GetSliderTooltipKey(int index)
		{
			return "STRINGS.UI.UISIDESCREENS.INFINITESOURCE.FLOW.TOOLTIP";
		}

		float ISliderControl.GetSliderValue(int index)
		{
			return GetComponent<InfiniteSource>().Flow;
		}

		void ISliderControl.SetSliderValue(float percent, int index)
		{
			GetComponent<InfiniteSource>().Flow = percent;
		}

		int ISliderControl.SliderDecimalPlaces(int index)
		{
			return 0;
		}


        string ISliderControl.GetSliderTooltip()
        {
            return "STRINGS.UI.UISIDESCREENS.INFINITESOURCE.FLOW.TOOLTIP";
        }


    }
}
