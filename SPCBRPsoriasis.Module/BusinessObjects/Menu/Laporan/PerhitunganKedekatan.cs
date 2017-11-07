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
using DevExpress.ExpressApp.Xpo;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Kasus;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Laporan
{
    [DefaultClassOptions]

    public class PerhitunganKedekatan : SPCBRPsoriasisBaseObject
    {
        private Kedekatan Kedekatan;
        private DataKasus DataKasus;
        private string Perhitungan;
        private double Total;

        public PerhitunganKedekatan(Session session)
            : base(session)
        {
        }

        [Association("Kedekatan-PerhitunganKedekatan"), ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public Kedekatan kedekatan
        {
            get
            {
                return Kedekatan;
            }
            set
            {
                SetPropertyValue("Kedekatan", ref Kedekatan, value);
            }
        }

        
        [ImmediatePostData]
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


        [Size(12000)]
        public string perhitungan
        {
            get
            {
                return Perhitungan;
            }
            set
            {
                SetPropertyValue("Perhitungan", ref Perhitungan, value);
            }
        }

        public double total
        {
            get
            {
                return Total;
            }
            set
            {
                SetPropertyValue("Total", ref Total, value);
            }
        }

    }
}
