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
using DevExpress.Xpo.DB;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
   
    public class DataPasien : SPCBRPsoriasisBaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).

        private string KodePasien;
        private string NamaPasien;
        private string Alamat;
        private string TTL;
        private int Umur;
        private Jk JenisKelamin;
        private string NoTelp;
        private Boolean Perokok;
        private Boolean MinumanKeras;
        private Boolean Narkotika;
        private Boolean Aktif;

        private string Aikode;

        public DataPasien(Session session)
            : base(session)
        {
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string kodepasien
        {
            get
            {
                return KodePasien;
            }
            set
            {
                SetPropertyValue("KodePasien", ref KodePasien, value);
            }
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string namapasien
        {
            get
            {
                return NamaPasien;
            }
            set
            {
                SetPropertyValue("NamaPasien", ref NamaPasien, value);
            }
        }

        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string alamat
        {
            get
            {
                return Alamat;
            }
            set
            {
                SetPropertyValue("Alamat", ref Alamat, value);
            }
        }


        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string ttl
        {
            get
            {
                return TTL;
            }
            set
            {
                SetPropertyValue("TTL", ref TTL, value);
            }
        }


        [RuleRequiredField(DefaultContexts.Save)]
        public int umur
        {
            get
            {
                return Umur;
            }
            set
            {
                SetPropertyValue("Umur", ref Umur, value);
            }
        }


        [Size(500)]
        [RuleRequiredField(DefaultContexts.Save)]
        public Jk jeniskelamin
        {
            get
            {
                return JenisKelamin;
            }
            set
            {
                SetPropertyValue("JenisKelamin", ref JenisKelamin, value);
            }
        }


        public string notelp
        {
            get
            {
                return NoTelp;
            }
            set
            {
                SetPropertyValue("NoTelp", ref NoTelp, value);
            }
        }


        [RuleRequiredField(DefaultContexts.Save)]
        public Boolean perokok
        {
            get
            {
                return Perokok;
            }
            set
            {
                SetPropertyValue("Perokok", ref Perokok, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Boolean minumakeras
        {
            get
            {
                return MinumanKeras;
            }
            set
            {
                SetPropertyValue("MinumanKeras", ref MinumanKeras, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        public Boolean narkotika
        {
            get
            {
                return Narkotika;
            }
            set
            {
                SetPropertyValue("Narkotika", ref Narkotika, value);
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
            XPCollection<DataPasien> queryaikode = new XPCollection<DataPasien>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodepasien", SortingDirection.Ascending));
            queryaikode.Sorting = sort;
            foreach (DataPasien theRow in queryaikode)
            {
                this.Aikode = (Int32.Parse(theRow.kodepasien.Substring(1)) + 1).ToString();
            }

            if (this.Aikode.Length == 1)
            {
                this.KodePasien = "P000" + this.Aikode;
            }
            else if (this.Aikode.Length == 2)
            {
                this.KodePasien = "P00" + this.Aikode;
            }
            else if (this.Aikode.Length == 3)
            {
                this.KodePasien = "P0" + this.Aikode;
            }
            else
            {
                this.KodePasien = "P" + this.Aikode;
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.SPCBRPsoriasisInitializeAikode();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        //private string _PersistentProperty;s
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
