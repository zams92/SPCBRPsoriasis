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

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien
{
    [DefaultClassOptions]

    public class DataKeluhanDetail : SPCBRPsoriasisBaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).


        private DataKeluhan DataKeluhan;
        private DataGejala DataGejala;
        private string Deskripsi;

        private XPCollection<DataGejala> QueryAvailableDataGejala;



        public DataKeluhanDetail(Session session)
            : base(session)
        {
        }

        [Association("DataKeluhan-DataKeluhanDetail"), ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public DataKeluhan datakeluhan
        {
            get
            {
                return DataKeluhan;
            }
            set
            {
                SetPropertyValue("DataKeluhan", ref DataKeluhan, value);
            }
        }


        [ImmediatePostData]
        [Custom("DataSourceProperty", "AvailableDataGejala")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DataGejala datagejala
        {
            get
            {
                return DataGejala;
            }
            set
            {
                SetPropertyValue("DataGejala", ref DataGejala, value);
            }
        }

        [Size(256)]
        public string deskripsi
        {
            get
            {
                return Deskripsi;
            }
            set
            {
                SetPropertyValue("Deskripsi", ref Deskripsi, value);
            }
        }





        public void SPCBRPsoriasisInitializeDataGejala()
        {
            // Get Collection of ROWS (Multi Records), by NAF
            XPCollection<DataGejala> queryDataGejala = new XPCollection<DataGejala>(Session);

            CriteriaOperator criteria1;

            criteria1 = BinaryOperator.Parse("aktif like '" + true + "' ");
            queryDataGejala.Criteria = criteria1;
            this.QueryAvailableDataGejala = queryDataGejala;

        }


        [Browsable(false)]
        public XPCollection<DataGejala> AvailableDataGejala
        {
            get
            {
                if (this.QueryAvailableDataGejala == null)
                {
                    this.QueryAvailableDataGejala = new XPCollection<DataGejala>(Session);
                    SPCBRPsoriasisInitializeDataGejala();
                }
                else
                {
                    SPCBRPsoriasisInitializeDataGejala();
                }
                return this.QueryAvailableDataGejala;
            }
        }



        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
            this.SPCBRPsoriasisInitializeDataGejala();

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
