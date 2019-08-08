using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrisInfiniteSources
{
    public class BrisArtLight : GameStateMachine<BrisArtLight, BrisArtLight.Instance>
    {
        public GameStateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.State Off;
        public GameStateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.State On;

        public override void InitializeStates(out StateMachine.BaseState defaultState)
        {
            defaultState = (StateMachine.BaseState)this.Off;
            this.Off.PlayAnim("misc").EventTransition(GameHashes.OperationalChanged, this.On, (StateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.Transition.ConditionCallback)(smi => smi.GetComponent<Operational>().IsOperational));
            this.On.Enter("SetActive", (StateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.State.Callback)(smi => smi.GetComponent<Operational>().SetActive(true, false))).PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.Off, (StateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.Transition.ConditionCallback)(smi => !smi.GetComponent<Operational>().IsOperational)).ToggleStatusItem(Db.Get().BuildingStatusItems.EmittingLight, (Func<BrisArtLight.Instance, object>)null);
        }

        public class Instance : GameStateMachine<BrisArtLight, BrisArtLight.Instance, IStateMachineTarget, object>.GameInstance
        {
            public Instance(IStateMachineTarget master)
              : base(master)
            {
            }
        }
    }
}
