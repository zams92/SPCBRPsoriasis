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

    public class Kedekatan : SPCBRPsoriasisBaseObject
    {
        private DataPasien DataPasien;
        private DataKeluhan DataKeluhan;
        private DataKasus DataKasus;
        private DataKasus DataKasus2;
        private double MaxKedekatan;

        public Kedekatan(Session session)
            : base(session)
        {
        }

        [ImmediatePostData]
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

        [ImmediatePostData]
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

        [ImmediatePostData]
        public DataKasus datakasus2
        {
            get
            {
                return DataKasus2;
            }
            set
            {
                SetPropertyValue("DataKasus2", ref DataKasus2, value);
            }
        }

        
        public double maxkedekatan
        {
            get
            {
                return MaxKedekatan;
            }
            set
            {
                SetPropertyValue("MaxKedekatan", ref MaxKedekatan, value);
            }
        }

        [Association("Kedekatan-PerhitunganKedekatan")]
        public XPCollection<PerhitunganKedekatan> PerhitunganKedekatan
        {
            get
            {
                return GetCollection<PerhitunganKedekatan>("PerhitunganKedekatan");
            }
        }
    }
}
