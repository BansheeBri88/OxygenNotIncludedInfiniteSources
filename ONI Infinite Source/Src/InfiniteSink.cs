using UnityEngine;

namespace BrisInfiniteSources
{
	public class InfiniteSink : KMonoBehaviour
	{
		[SerializeField]
		public ConduitType Type;
        [MyCmpGet]
        protected Operational operational;

        private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;
		private int inputCell;

		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			accumulator = Game.Instance.accumulators.Add("Sink", (KMonoBehaviour)this);
		}

		protected override void OnSpawn()
		{
             

            base.OnSpawn();
            operational.SetActive(operational.IsOperational, false);
            var building = GetComponent<Building>();
			inputCell = building.GetUtilityInputCell();

			Conduit.GetFlowManager(Type).AddConduitUpdater(ConduitUpdate);
		}

		protected override void OnCleanUp()
		{
			Conduit.GetFlowManager(Type).RemoveConduitUpdater(ConduitUpdate);
			Game.Instance.accumulators.Remove(accumulator);
			base.OnCleanUp();
		}

        private bool IsOperational
        {
            get
            {
                return GetComponent<Operational>().IsOperational;
            }
        }

		private void ConduitUpdate(float dt)
		{
            if (Type == ConduitType.Solid)
             {
                var sFlow = SolidConduit.GetFlowManager();
                if (sFlow == null || !sFlow.HasConduit(inputCell) || !IsOperational)
                { operational.SetActive(false, false); ;  return; }
                if (sFlow.IsConduitEmpty(inputCell)) {  operational.SetActive(false,false); return; }
                operational.SetActive(true,false);
                var pickupable = sFlow.RemovePickupable(inputCell);
                pickupable.DeleteObject();
            }
            else
            {
                
                var flowManager = Conduit.GetFlowManager(Type);
                
                if (flowManager == null || !flowManager.HasConduit(inputCell) || !IsOperational || flowManager.IsConduitEmpty(inputCell))
                {
                    operational.SetActive(false, false);
                    return;
                }
                operational.SetActive(true, false);
                var contents = flowManager.GetContents(inputCell);
                flowManager.RemoveElement(inputCell, contents.mass);
                Game.Instance.accumulators.Accumulate(accumulator, contents.mass);
            }
		}

	}
}
