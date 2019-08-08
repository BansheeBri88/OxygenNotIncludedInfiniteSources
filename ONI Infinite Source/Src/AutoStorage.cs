using KSerialization;
using UnityEngine;
using System.Collections.Generic;
using TUNING;
using STRINGS;

namespace BrisInfiniteSources
{
    //[SkipSaveFileSerialization]
    public class AutoStorage : KMonoBehaviour, ISim1000ms, IUserControlledCapacity
    {
        [Serialize]
        private float userMaxCapacity = float.PositiveInfinity;
        [Serialize]
        public string lockerName = string.Empty;
        private LoggerFS log;
        protected FilteredStorage filteredStorage;
        
        [MyCmpGet]
        private Storage storage;
        [MyCmpReq]
        private TreeFilterable treeFilterable;

        public void Sim1000ms(float dt)
        {
            if (this.AmountStored < this.UserMaxCapacity)
            {
                List<Tag> acceptedTags = this.treeFilterable.AcceptedTags;
                foreach (Tag tag in acceptedTags)
                {
                    GameObject myItem = GameObject.Instantiate(tag.Prefab());
                    myItem.SetActive(true);
                    if (myItem.HasTag(GameTags.Clothes)) { storage.Store(myItem, false, false, false, true); } else storage.Store(myItem, false, false, false, false);
                }
            }
            return;
        }


        protected override void OnSpawn()
        {
            Tag myTag;
            List<GameObject> myObjects = new List<GameObject>();
            foreach (Tag myTagCat in GameTags.UnitCategories)
            {
                myObjects = (Assets.GetPrefabsWithTag(myTagCat));
                foreach (GameObject myObject in myObjects)
                {
                    if (myTagCat == GameTags.Compostable) { myTag = myObject.tag; }
                    else myTag = myObject.name.ToString();
                    if (myTagCat == GameTags.Compostable) { if (myTag.ToString() != "Untagged") WorldInventory.Instance.Discover(myTag, GameTags.Seed); }
                    else WorldInventory.Instance.Discover(myTag, myTagCat);
                    
                }
            }


            this.filteredStorage.FilterChanged();
            if (this.lockerName.IsNullOrWhiteSpace())
                return;
            this.SetName(this.lockerName);

           
        }
        protected override void OnPrefabInit()
        {
            this.Initialize(false);
        }

        protected void Initialize(bool use_logic_meter)
        {
            base.OnPrefabInit();
            this.log = new LoggerFS(nameof(StorageLocker), 35);
            this.filteredStorage = new FilteredStorage((KMonoBehaviour)this, (Tag[])null, (Tag[])null, (IUserControlledCapacity)this, use_logic_meter, Db.Get().ChoreTypes.StorageFetch);
        }


        protected override void OnCleanUp()
        {
            this.filteredStorage.CleanUp();
        }

        private void OnCopySettings(object data)
        {
            GameObject gameObject = (GameObject)data;
            if ((UnityEngine.Object)gameObject == (UnityEngine.Object)null)
                return;
            AutoStorage component = gameObject.GetComponent<AutoStorage>();
            if ((UnityEngine.Object)component == (UnityEngine.Object)null)
                return;
            this.UserMaxCapacity = component.UserMaxCapacity;
        }

        public virtual float UserMaxCapacity
        {
            get
            {
                return Mathf.Min(this.userMaxCapacity, this.GetComponent<Storage>().capacityKg);
            }
            set
            {
                this.userMaxCapacity = value;
                this.filteredStorage.FilterChanged();
            }
        }

        public float AmountStored
        {
            get
            {
                return this.GetComponent<Storage>().MassStored();
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
                return this.GetComponent<Storage>().capacityKg;
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

        public void SetName(string name)
        {
            KSelectable component = this.GetComponent<KSelectable>();
            this.name = name;
            this.lockerName = name;
            if ((UnityEngine.Object)component != (UnityEngine.Object)null)
                component.SetName(name);
            this.gameObject.name = name;
            NameDisplayScreen.Instance.UpdateName(this.gameObject);
        }
    }
}