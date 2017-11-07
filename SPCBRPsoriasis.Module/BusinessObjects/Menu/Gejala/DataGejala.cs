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
using System;
using DevExpress.Xpo.DB;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Gejala
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).

    public class DataGejala : SPCBRPsoriasisBaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).

        private string KodeGejala;
        private string NamaGejala;
        private double Bobot;
        private Boolean Aktif;

        private string Aikode;
        public DataGejala(Session session)
            : base(session)
        {
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string kodegejala
        {
            get
            {
                return KodeGejala;
            }
            set
            {
                SetPropertyValue("KodeGejala", ref KodeGejala, value);
            }
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string namagejala
        {
            get
            {
                return NamaGejala;
            }
            set
            {
                SetPropertyValue("NamaGejala", ref NamaGejala, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public double bobot
        {
            get
            {
                return Bobot;
            }
            set
            {
                SetPropertyValue("Bobot", ref Bobot, value);
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

        public void SPCBRPsoriasisInitializeAikode()
        {
            XPCollection<DataGejala> queryaikode = new XPCollection<DataGejala>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodegejala", SortingDirection.Ascending));
            queryaikode.Sorting = sort;
            foreach (DataGejala theRow in queryaikode)
            {
                this.Aikode = (Int32.Parse(theRow.kodegejala.Substring(1)) + 1).ToString();
            }

            if (this.Aikode != null)
            {

                if (this.Aikode.Length == 1)
                {
                    this.KodeGejala = "G000" + this.Aikode;
                }
                else if (this.Aikode.Length == 2)
                {
                    this.KodeGejala = "G00" + this.Aikode;
                }
                else if (this.Aikode.Length == 3)
                {
                    this.KodeGejala = "G0" + this.Aikode;
                }
                else
                {
                    this.KodeGejala = "G" + this.Aikode;
                }
            }
            else
            {
                this.KodeGejala = "G0001";
            }

        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.SPCBRPsoriasisInitializeAikode();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
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
