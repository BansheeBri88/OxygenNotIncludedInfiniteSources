using KSerialization;
using UnityEngine;
using System.Collections.Generic;
using TUNING;
using STRINGS;

namespace BrisInfiniteSources
{
    //[SkipSaveFileSerialization]
        public class AutoFridge : KMonoBehaviour, IUserControlledCapacity, IEffectDescriptor, IGameObjectEffectDescriptor,  ISim1000ms
    {


        //private static readonly EventSystem.IntraObjectHandler<AutoFridge> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AutoFridge>((System.Action<AutoFridge, object>)((component, data) => component.OnOperationalChanged(data)));
        //private static readonly EventSystem.IntraObjectHandler<AutoFridge> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<AutoFridge>((System.Action<AutoFridge, object>)((component, data) => component.OnCopySettings(data)));
        //private static readonly EventSystem.IntraObjectHandler<AutoFridge> UpdateLogicCircuitCBDelegate = new EventSystem.IntraObjectHandler<AutoFridge>((System.Action<AutoFridge, object>)((component, data) => component.UpdateLogicCircuitCB(data)));




        [SerializeField]
        public float simulatedInternalTemperature = 274.26f;
        [SerializeField]
        public float simulatedInternalHeatCapacity = 4000f;
        [SerializeField]
        public float simulatedThermalConductivity = 10000f;
        [Serialize]
        private float userMaxCapacity = float.PositiveInfinity;
        [MyCmpGet]
        private Storage storage;
        [MyCmpGet]
        private Operational operational;
        [MyCmpGet]
        private LogicPorts ports;
        private FilteredStorage filteredStorage;
        private SimulatedTemperatureAdjuster temperatureAdjuster;


        [MyCmpReq]
        private TreeFilterable treeFilterable;
        [MyCmpGet]
        internal KBatchedAnimController controller;

        public void Sim1000ms(float dt)
        {
            
            if (!(this.IsOperational)) return;
            if  (this.AmountStored < this.UserMaxCapacity)
            {
                Debug.Log("UserMaxCapacity: " + this.UserMaxCapacity.ToString());
                Debug.Log("AmountStored: " + this.AmountStored.ToString());
                Debug.Log("IsFull: " + this.filteredStorage.IsFull().ToString());
                
                
                List<Tag> acceptedTags = this.treeFilterable.AcceptedTags;
                foreach (Tag tag in acceptedTags)
                {
                    GameObject myFood = Edible.Instantiate(tag.Prefab());
                    myFood.SetActive(true);
                    storage.Store(myFood, false, false, false, false);
                    storage.Drop(myFood);
                    storage.Store(myFood, false, false, false, true);
                    
                }
            }
            //this.filteredStorage.FilterChanged();
            this.UpdateLogicCircuit();
            return;
        }
        protected override void OnPrefabInit()
        {
            this.filteredStorage = new FilteredStorage((KMonoBehaviour)this, (Tag[])null, new Tag[1]
            {
      GameTags.Compostable
            }, (IUserControlledCapacity)this, true, Db.Get().ChoreTypes.FoodFetch);
        }
        private bool IsOperational
        {
            get
            {
                return GetComponent<Operational>().IsOperational;
            }
        }
        protected override void OnSpawn()
        {
            Tag myTag;
            foreach (EdiblesManager.FoodInfo foodTypes in FOOD.FOOD_TYPES_LIST)
            {
                Tag tag = foodTypes.Id.ToTag();
                if (foodTypes.CaloriesPerUnit > 0.0)
                    WorldInventory.Instance.Discover(tag, GameTags.Edible);
                if (foodTypes.CaloriesPerUnit == 0.0)
                    WorldInventory.Instance.Discover(tag, GameTags.CookingIngredient);
            }
            List<GameObject> myObjects = (Assets.GetPrefabsWithTag(GameTags.Medicine));
            foreach (GameObject myObject in myObjects)
            {
                myTag = myObject.PrefabID();
                if (myTag != "Untagged") WorldInventory.Instance.Discover(myTag, GameTags.Medicine); ;
            }

            this.operational.SetActive(this.operational.IsOperational, false);
            this.GetComponent<KAnimControllerBase>().Play((HashedString)"off", KAnim.PlayMode.Once, 1f, 0.0f);
            this.filteredStorage.FilterChanged();
            this.temperatureAdjuster = new SimulatedTemperatureAdjuster(this.simulatedInternalTemperature, this.simulatedInternalHeatCapacity, this.simulatedThermalConductivity, this.GetComponent<Storage>());
            this.UpdateLogicCircuit();



            //this.Subscribe<AutoFridge>(-592767678, AutoFridge.OnOperationalChangedDelegate);
            //this.Subscribe<AutoFridge>(-905833192, AutoFridge.OnCopySettingsDelegate);
            //this.Subscribe<AutoFridge>(-1697596308, AutoFridge.UpdateLogicCircuitCBDelegate);
            //this.Subscribe<AutoFridge>(-592767678, AutoFridge.UpdateLogicCircuitCBDelegate);
        }
        protected override void OnCleanUp()
        {
            this.filteredStorage.CleanUp();
            this.temperatureAdjuster.CleanUp();
        }

        private void OnOperationalChanged(object data)
        {
            this.operational.SetActive(this.operational.IsOperational, false);
        }

        public bool IsActive()
        {
            return this.operational.IsActive;
        }

        private void OnCopySettings(object data)
        {
            GameObject gameObject = (GameObject)data;
            if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
                return;
            AutoFridge component = gameObject.GetComponent<AutoFridge>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                return;
            this.UserMaxCapacity = component.UserMaxCapacity;
        }

        public List<Descriptor> GetDescriptors(BuildingDef def)
        {
            return this.GetDescriptors(def.BuildingComplete);
        }

        public List<Descriptor> GetDescriptors(GameObject go)
        {
            return SimulatedTemperatureAdjuster.GetDescriptors(this.simulatedInternalTemperature);
        }

        public float UserMaxCapacity
        {
            get
            {
                return Mathf.Min(this.userMaxCapacity, this.storage.capacityKg);
            }
            set
            {
                this.userMaxCapacity = value;
                this.filteredStorage.FilterChanged();
                this.UpdateLogicCircuit();
            }
        }

        public float AmountStored
        {
            get
            {
                return this.storage.MassStored();
            }
        }

        public float MinCapacity
        {
            get
            {
                return 0.0f;
            }
        }

        public float MaxCapacity
        {
            get
            {
                return this.storage.capacityKg;
            }
        }

        public bool WholeValues
        {
            get
            {
                return false;
            }
        }

        public LocString CapacityUnits
        {
            get
            {
                return GameUtil.GetCurrentMassUnit(false);
            }
        }

        private void UpdateLogicCircuitCB(object data)
        {
            this.UpdateLogicCircuit();
        }

        private void UpdateLogicCircuit()
        {
            bool flag = this.filteredStorage.IsFull();
            bool isOperational = this.operational.IsOperational;
            bool on = flag && isOperational;
            this.ports.SendSignal(FilteredStorage.FULL_PORT_ID, !on ? 0 : 1);
            this.filteredStorage.SetLogicMeter(on);
        }
    }
}
