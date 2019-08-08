using Klei.AI;
using KSerialization;
using System;
using UnityEngine;

namespace BrisInfiniteSources
{
    [SerializationConfig(MemberSerialization.OptIn)]
    internal class BrisInfiniteGenerator : Generator
    {
        [SerializeField]
        public int powerDistributionOrder;
        [MyCmpGet]
        internal Storage storage;
        private float capacity;
        private StatusItem currentStatusItem;
        private Guid statusItemID;
        private AttributeInstance generatorOutputAttribute;
        protected override void OnSpawn()
        {
            operational.SetActive(operational.IsOperational, false);
            //Components.Generators.Add(this);
            capacity = Generator.CalculateCapacity(building.Def, (Element)null);
            PowerCell = this.building.GetPowerOutputCell();
            Game.Instance.energySim.AddGenerator(this);
            storage.AddLiquid(SimHashes.Water, 10000, 293, 0, 0, false, false);
            base.OnSpawn();
        }
        public int PowerCell { get; private set; }

        protected override void OnCleanUp()
        {
            Game.Instance.energySim.RemoveGenerator(this);
            Game.Instance.circuitManager.Disconnect(this);
            Components.Generators.Remove(this);

            base.OnCleanUp();
        }
        public override bool IsEmpty
            {
                get
                {
                    return false;
                }
            }

        public override void EnergySim200ms(float dt)
            {
                base.EnergySim200ms(dt);
                this.ApplyDeltaJoules(this.WattageRating * dt, false);
            }
        public float AmountStored
        {
            get
            {
                return this.storage.MassStored();
            }
        }

    }
}