using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.Xpo;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Solusi;
using DevExpress.Xpo.DB;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Kasus
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).

    public class DataKasus : SPCBRPsoriasisBaseObject, IStateMachineProvider
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).

        private string KodeKasus;
        private string NamaPenyakit;
        private string Aikode;
        private DataPasien DataPasien;
        private DateTime TanggalKasus;
        private DateTime TanggalValidasiKasus;
        private Boolean Aktif;

        private DataKasusStatus StatusKasus;

        /* Temporary Variable to View the LOV */


        private XPCollection<DataPasien> QueryAvailableDataPasien;

        public DataKasus(Session session)
            : base(session)
        {
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string kodekasus
        {
            get
            {
                return KodeKasus;
            }
            set
            {
                SetPropertyValue("KodeKasus", ref KodeKasus, value);
            }
        }

        [Size(200)]
        public string namapenyakit
        {
            get
            {
                return NamaPenyakit;
            }
            set
            {
                SetPropertyValue("NamaPenyakit", ref NamaPenyakit, value);
            }
        }

        [ImmediatePostData]
        [Custom("DataSourceProperty", "AvailableDataPasien")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DataPasien datapasien
        {
            get
            {
                return DataPasien;
            }
            set
            {
                SetPropertyValue("DataPasien", ref DataPasien, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime tanggalkasus
        {
            get
            {
                return TanggalKasus;
            }
            set
            {
                SetPropertyValue("TanggalKasus", ref TanggalKasus, value);
            }
        }

        public DateTime tanggalvalidasikasus
        {
            get
            {
                return TanggalValidasiKasus;
            }
            set
            {
                SetPropertyValue("TanggalValidasiKasus", ref TanggalValidasiKasus, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Boolean aktif
        {
            get
            {
                return Aktif;
            }
            set
            {
                SetPropertyValue("Aktif", ref Aktif, value);
            }
        }

        public DataKasusStatus statuskasus
        {
            get
            {
                return StatusKasus;
            }
            set
            {
                SetPropertyValue("StatusKasus", ref StatusKasus, value);
            }
        }

        public void ProsesValidasi()
        {
            this.aktif = true;
            this.TanggalValidasiKasus = DateTime.Today;
        }

        protected override void OnSaving()
        {
            if (this.statuskasus.ToString() == DataKasusStatus.Validasi.ToString())
            {
                this.ProsesValidasi();
            }

            base.OnSaving();
        }


        [Association("DataKasus-DataKasusDetail")]
        public XPCollection<DataKasusDetail> DataKasusDetail
        {
            get
            {
                return GetCollection<DataKasusDetail>("DataKasusDetail");
            }
        }

        [Association("DataKasus-DataSolusi")]
        public XPCollection<DataSolusi> DataSolusi
        {
            get
            {
                return GetCollection<DataSolusi>("DataSolusi");
            }
        }

        public void SPCBRPsoriasisInitializeDataPasien()
        {
            // Get Collection of ROWS (Multi Records), by NAF
            XPCollection<DataPasien> queryDataPasien = new XPCollection<DataPasien>(Session);

            CriteriaOperator criteria1;

            criteria1 = BinaryOperator.Parse("aktif like '"+true+"'");
            queryDataPasien.Filter = criteria1;
            this.QueryAvailableDataPasien = queryDataPasien;

        }

        public void SPCBRPsoriasisInitializeAikode()
        {
            XPCollection<DataKasus> queryaikode = new XPCollection<DataKasus>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodekasus", SortingDirection.Ascending));
            queryaikode.Sorting = sort;

            foreach (DataKasus theRow in queryaikode)
            {
                this.Aikode = (Int32.Parse(theRow.kodekasus.Substring(1)) + 1).ToString();
            }
            if (this.Aikode != null)
            {
                if (this.Aikode.Length == 1)
                {
                    this.KodeKasus = "K000" + this.Aikode;
                }
                else if (this.Aikode.Length == 2)
                {
                    this.KodeKasus = "K00" + this.Aikode;
                }
                else if (this.Aikode.Length == 3)
                {
                    this.KodeKasus = "K0" + this.Aikode;
                }
                else
                {
                    this.KodeKasus = "K" + this.Aikode;
                }
            }
            else
            {
                this.KodeKasus = "K0001";
            }

        }

        public string kodekasusauto()
        {
            XPCollection<DataKasus> queryaikode = new XPCollection<DataKasus>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodekasus", SortingDirection.Ascending));
            queryaikode.Sorting = sort;

            foreach (DataKasus theRow in queryaikode)
            {
                this.Aikode = (Int32.Parse(theRow.kodekasus.Substring(1)) + 1).ToString();
            }
            if (this.Aikode != null)
            {
                if (this.Aikode.Length == 1)
                {
                    this.KodeKasus = "K000" + this.Aikode;
                }
                else if (this.Aikode.Length == 2)
                {
                    this.KodeKasus = "K00" + this.Aikode;
                }
                else if (this.Aikode.Length == 3)
                {
                    this.KodeKasus = "K0" + this.Aikode;
                }
                else
                {
                    this.KodeKasus = "K" + this.Aikode;
                }
            }
            else
            {
                this.KodeKasus = "K0001";
            }

            return this.KodeKasus;
        }


        [Browsable(false)]
        public XPCollection<DataPasien> AvailableDataPasien
        {
            get
            {
                if (this.QueryAvailableDataPasien == null)
                {
                    this.QueryAvailableDataPasien = new XPCollection<DataPasien>(Session);
                    SPCBRPsoriasisInitializeDataPasien();
                }
                else
                {
                    SPCBRPsoriasisInitializeDataPasien();
                }
                return this.QueryAvailableDataPasien;
            }
        }






        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).

            this.SPCBRPsoriasisInitializeDataPasien();
            this.SPCBRPsoriasisInitializeAikode();


        }


        #region IStateMachineProvider Members

        public IList<IStateMachine> GetStateMachines()
        {
            List<IStateMachine> result = new List<IStateMachine>();
            result.Add(new TSDataKasus(XPObjectSpace.FindObjectSpaceByObject(this)));
            return result;
        }

        #endregion

        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        //    this.PersistentProperty = "Paid";
        //}
    }
}
