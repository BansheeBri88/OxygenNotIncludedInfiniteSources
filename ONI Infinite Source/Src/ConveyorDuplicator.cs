
using KSerialization;
using System.Collections.Generic;
using UnityEngine;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    public class ConveyorDuplicator : KMonoBehaviour, ISecondaryOutput
    {
        private int _inputCell = -1;
        private int _outputCell = -1;
        private int _filteredCell = -1;
        [SerializeField]
        public ConduitPortInfo SecondaryPort;
        [MyCmpReq]
        private Operational operational;
        [MyCmpGet]
        internal Storage storage;

        protected override void OnSpawn()
        {
            base.OnSpawn();
            Building component = this.GetComponent<Building>();
            this._inputCell = component.GetUtilityInputCell();
            this._outputCell = component.GetUtilityOutputCell();
            this._filteredCell = Grid.OffsetCell(Grid.PosToCell(this.transform.GetPosition()), component.GetRotatedOffset(this.SecondaryPort.offset));
            Game.Instance.solidConduitSystem.AddToNetworks(this._filteredCell, (object)new FlowUtilityNetwork.NetworkItem(this.SecondaryPort.conduitType, Endpoint.Source, this._filteredCell, this.gameObject), true);
            SolidConduit.GetFlowManager().AddConduitUpdater(new System.Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
        }

        protected override void OnCleanUp()
        {
            SolidConduit.GetFlowManager().RemoveConduitUpdater(new System.Action<float>(this.ConduitUpdate));
            base.OnCleanUp();
        }

        private void ConduitUpdate(float dt)
        {

            SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
            if (!flowManager.HasConduit(this._inputCell) || !flowManager.HasConduit(this._outputCell) || !flowManager.IsConduitFull(this._inputCell) || (!flowManager.IsConduitEmpty(this._outputCell)))
                return;


            Pickupable pickupable = flowManager.RemovePickupable(_inputCell);
            flowManager.AddPickupable(this._outputCell, pickupable);
            if (!(bool)((UnityEngine.Object)pickupable))
                return;
            if (flowManager.HasConduit(this._filteredCell) && flowManager.IsConduitEmpty(this._filteredCell))
            {
                Pickupable pickupable2 = EntityPrefabs.Instantiate(pickupable);
                flowManager.AddPickupable(this._filteredCell,pickupable2);
            }
            this.operational.SetActive(false, false);
        }

        public ConduitType GetSecondaryConduitType()
        {
            return this.SecondaryPort.conduitType;
        }

        public CellOffset GetSecondaryConduitOffset()
        {
            return this.SecondaryPort.offset;
        }
    }
}
