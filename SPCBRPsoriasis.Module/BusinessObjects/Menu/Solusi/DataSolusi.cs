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
using System.Diagnostics;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Gejala;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Kasus;
using DevExpress.Xpo.DB;


namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Solusi
{
    [DefaultClassOptions]

    public class DataSolusi : SPCBRPsoriasisBaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).

        private string KodeSolusi;
        private DataKasus DataKasus;
        private string Pengobatan;
        private string kosul;
        private XPCollection<DataKasus> QueryAvailableDataKasus; 



        public DataSolusi(Session session)
            : base(session)
        {
        }

        [Size(100)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public string kodesolusi
        {
            get
            {
                return KodeSolusi;
            }
            set
            {
                SetPropertyValue("KodeSolusi", ref KodeSolusi, value);
            }
        }

        [Association("DataKasus-DataSolusi"), ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public DataKasus datakasus
        {
            get
            {
                return DataKasus;
            }
            set
            {
                SetPropertyValue("DataKasus", ref DataKasus, value);
            }
        }
        

        [Size(1000)]
        public string pengobatan
        {
            get
            {
                return Pengobatan;
            }
            set
            {
                SetPropertyValue("Pengobatan", ref Pengobatan, value);
            }
        }


        public void SPCBRPsoriasisInitializeDataKasus()
        {
            // Get Collection of ROWS (Multi Records), by NAF
            XPCollection<DataKasus> queryDataKasus = new XPCollection<DataKasus>(Session);

            CriteriaOperator criteria1;

            criteria1 = BinaryOperator.Parse("aktif like '" + false + "'");
            queryDataKasus.Criteria = criteria1;
            this.QueryAvailableDataKasus = queryDataKasus;

        }

        public void SPCBRPsoriasisInitializeKodeSolusi()
        {
            XPCollection<DataSolusi> querykodesolusi = new XPCollection<DataSolusi>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodesolusi", SortingDirection.Ascending));
            querykodesolusi.Sorting = sort;
            foreach (DataSolusi theRow in querykodesolusi)
            {
                this.kosul = (Int32.Parse(theRow.kodesolusi.Substring(1)) + 1).ToString();
            }
            if (this.kosul != null)
            {
                if (this.kosul.Length == 1)
                {
                    this.KodeSolusi = "S000" + this.kosul;
                }
                else if (this.kosul.Length == 2)
                {
                    this.KodeSolusi = "S00" + this.kosul;
                }
                else if (this.kosul.Length == 3)
                {
                    this.KodeSolusi = "S0" + this.kosul;
                }
                else
                {
                    this.KodeSolusi = "S" + this.kosul;
                }
            }
            else
            {
                this.KodeSolusi = "S0001";
            }
        }

        public string kosulauto()
        {
            XPCollection<DataSolusi> querykodesolusi = new XPCollection<DataSolusi>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodesolusi", SortingDirection.Ascending));
            querykodesolusi.Sorting = sort;
            foreach (DataSolusi theRow in querykodesolusi)
            {
                this.kosul = (Int32.Parse(theRow.kodesolusi.Substring(1)) + 1).ToString();
            }
            if (this.kosul != null)
            {
                if (this.kosul.Length == 1)
                {
                    this.KodeSolusi = "S000" + this.kosul;
                }
                else if (this.kosul.Length == 2)
                {
                    this.KodeSolusi = "S00" + this.kosul;
                }
                else if (this.kosul.Length == 3)
                {
                    this.KodeSolusi = "S0" + this.kosul;
                }
                else
                {
                    this.KodeSolusi = "S" + this.kosul;
                }
            }
            else
            {
                this.KodeSolusi = "S0001";
            }

            return this.KodeSolusi;
        }

        [Browsable(false)]
        public XPCollection<DataKasus> AvailableDataKasus
        {
            get
            {
                if (this.QueryAvailableDataKasus == null)
                {
                    this.QueryAvailableDataKasus = new XPCollection<DataKasus>(Session);
                    SPCBRPsoriasisInitializeDataKasus();
                }
                else
                {
                    SPCBRPsoriasisInitializeDataKasus();
                }
                return this.QueryAvailableDataKasus;
            }
        }



        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
            this.SPCBRPsoriasisInitializeDataKasus();
            this.SPCBRPsoriasisInitializeKodeSolusi();

        }
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
