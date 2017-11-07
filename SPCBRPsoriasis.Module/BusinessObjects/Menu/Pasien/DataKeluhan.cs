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
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Kasus;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Laporan;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Gejala;
using System.Diagnostics;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Solusi;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien
{
    [DefaultClassOptions]

    public class DataKeluhan : SPCBRPsoriasisBaseObject, IStateMachineProvider
    { 
        private string KodeKeluhan;
        private string Aikode;
        private DataPasien DataPasien;
        private DateTime TanggalKeluhan;
        private string Catatan;
 
        private DataKeluhanStatus StatusKeluhan;


        private XPCollection<DataPasien> QueryAvailableDataPasien;

        public DataKeluhan(Session session)
            : base(session)
        {
        }

        [Size(200)]
        [RuleRequiredField(DefaultContexts.Save)]
        public string kodekeluhan
        {
            get
            {
                return KodeKeluhan;
            }
            set
            {
                SetPropertyValue("KodeKeluhan", ref KodeKeluhan, value);
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
        public DateTime tanggalkeluhan
        {
            get
            {
                return TanggalKeluhan;
            }
            set
            {
                SetPropertyValue("TanggalKeluhan", ref TanggalKeluhan, value);
            }
        }

        public string catatan
        {
            get
            {
                return Catatan;
            }
            set
            {
                SetPropertyValue("Catatan", ref Catatan, value);
            }
        }

        public DataKeluhanStatus statuskeluhan
        {
            get
            {
                return StatusKeluhan;
            }
            set
            {
                SetPropertyValue("StatusKeluhan", ref StatusKeluhan, value);
            }
        }

        public void ProsesPerhitunganKedekatan()
        {
            XPCollection<DataKasus> querykasus = new XPCollection<DataKasus>(Session);
            XPCollection<DataGejala> querygejala = new XPCollection<DataGejala>(Session);
            XPCollection<DataKasusDetail> querykasusdetail = new XPCollection<DataKasusDetail>(Session);
            XPCollection<DataKeluhanDetail> querykeluhandetail = new XPCollection<DataKeluhanDetail>(Session);
            CriteriaOperator criteriakasus;
            CriteriaOperator criteriagejala;
            SortingCollection sortkasus = new SortingCollection();
            SortingCollection sortgejala = new SortingCollection();
            CriteriaOperator criteriakasusdetail;
            CriteriaOperator criteriakeluhandetail;
            PerhitunganKedekatan newdataperhitungan;
            Kedekatan newdatakedekatan;
            criteriakasus = BinaryOperator.Parse("aktif = true");
            sortkasus.Add(new SortProperty("kodekasus", SortingDirection.Ascending));
            querykasus.Sorting = sortkasus;
            querykasus.Criteria = criteriakasus;
            criteriagejala = BinaryOperator.Parse("aktif = true");
            sortgejala.Add(new SortProperty("kodegejala", SortingDirection.Ascending));
            querygejala.Sorting = sortgejala;
            querygejala.Criteria = criteriagejala;

            Debug.WriteLine("querykasus  : " + querykasus);
            List<PerhitunganKedekatan> listperhitungan = new List<PerhitunganKedekatan>();
            List<PerhitunganKedekatan> theDatax = new List<PerhitunganKedekatan>();
            newdatakedekatan = new Kedekatan(Session);
            newdatakedekatan.datakeluhan = this;
            newdatakedekatan.datapasien = this.datapasien;
            foreach (DataKasus thedatakasus in querykasus)
            {
                string perhitunganatas = "= ";
                double intperhitunganatas = 0;
                string pembagi = "/";
                double intpembagi = 0;
                int temp = 0;
                //Debug.WriteLine("kode kasus : " + thedatakasus.kodekasus);
                newdataperhitungan = new PerhitunganKedekatan(Session);
                newdataperhitungan.datakasus = thedatakasus;
                //Debug.WriteLine("kode kasus2 : " + newdataperhitungan.datakasus.kodekasus);
                foreach (DataGejala thedatagejala in querygejala)
                {

                    criteriakasusdetail = BinaryOperator.Parse("datakasus = '" + thedatakasus.Oid + "' and datagejala = '"+ thedatagejala.Oid +"'");
                    querykasusdetail.Criteria = criteriakasusdetail;
                    criteriakeluhandetail = BinaryOperator.Parse("datakeluhan = '" + this.Oid + "' and datagejala = '" + thedatagejala.Oid + "'");
                    querykeluhandetail.Criteria = criteriakeluhandetail;
                    intpembagi = intpembagi + thedatagejala.bobot;
                    temp = temp + 1;
                  //  Debug.WriteLine("xxx temp xxx : xxx " + temp);
                    if (temp == 1) 
                    {
                        pembagi = pembagi + thedatagejala.bobot;

                    }
                    else
                    {
                        pembagi = pembagi + "+" + thedatagejala.bobot;
                    }

                    if(querykasusdetail.Count() == 0 && querykeluhandetail.Count() == 0 )
                    {
                        if (temp == 1)
                        {
                            perhitunganatas = perhitunganatas + "(1x" + thedatagejala.bobot + ")";

                            temp = temp + 1;
                        }
                        else
                        {
                            perhitunganatas = perhitunganatas + "+(1x" + thedatagejala.bobot + ")";
                        }
                        intperhitunganatas = intperhitunganatas + 1 * thedatagejala.bobot;
                    }
                    else if (querykasusdetail.Count() != 0 && querykeluhandetail.Count() != 0)
                    {
                        foreach (DataKeluhanDetail abc in querykeluhandetail)
                        {
                            if (temp == 1)
                                {
                                    perhitunganatas = perhitunganatas + "(1x" + thedatagejala.bobot + ")";

                                    temp = temp + 1;
                                }
                                else
                                {
                                    perhitunganatas = perhitunganatas + "+(1x" + thedatagejala.bobot + ")";
                                }
                                intperhitunganatas = intperhitunganatas + 1 * thedatagejala.bobot;
                           
                        }
                    }
                    else
                    {
                        if (temp == 1)
                        {
                            perhitunganatas = perhitunganatas + "(0x" + thedatagejala.bobot + ")";
                        }
                        else
                        {
                            perhitunganatas = perhitunganatas + "+(0x" + thedatagejala.bobot + ")";
                        }
                        intperhitunganatas = intperhitunganatas + 0 * thedatagejala.bobot;
                    }
                    newdataperhitungan.perhitungan = perhitunganatas + pembagi + " \r\n = " + intperhitunganatas + "/" + intpembagi + " \r\n = " + (intperhitunganatas / intpembagi)*100 + "%";
                    newdataperhitungan.total = intperhitunganatas / intpembagi;
                }
                newdataperhitungan.kedekatan = newdatakedekatan;
                newdatakedekatan.PerhitunganKedekatan.Add(newdataperhitungan);
                listperhitungan.Add(newdataperhitungan);
            }

            PerhitunganKedekatan xxx = listperhitungan.OrderByDescending(obj => obj.total).First();
           
            DataKasus newdatakasus;
            DataKasusDetail newdatakasusdetail;
            DataSolusi newdatasolusi;
            List<DataKasusDetail> listdatakasusdetail = new List<DataKasusDetail>();
            List<DataSolusi> listdatasolusi = new List<DataSolusi>();
            XPCollection<DataKasus> quergetdatakasus = new XPCollection<DataKasus>(Session);
            CriteriaOperator criteiagetdatakasus;
            criteiagetdatakasus = BinaryOperator.Parse("Oid = '" + xxx.datakasus.Oid.ToString() + "'");
            quergetdatakasus.Criteria = criteiagetdatakasus;
            foreach (DataKasus dk in quergetdatakasus)
            {
                newdatakasus = new DataKasus(Session);
                newdatakasus.namapenyakit = dk.namapenyakit;
                newdatakasus.datapasien = this.datapasien;
                newdatakasus.tanggalkasus = this.tanggalkeluhan;
                newdatakasus.statuskasus = DataKasusStatus.Percobaan;
                newdatakasus.kodekasus = dk.kodekasusauto();
                XPCollection<DataKeluhanDetail> querydkdetail = new XPCollection<DataKeluhanDetail>(Session);
                CriteriaOperator criteiadkdetail;
                criteiadkdetail = BinaryOperator.Parse("datakeluhan = '" + this.Oid.ToString() + "'");
                querydkdetail.Criteria = criteiadkdetail;
                foreach (DataKeluhanDetail dkdetail in querydkdetail)
                {
                    newdatakasusdetail = new DataKasusDetail(Session);
                    newdatakasusdetail.datakasus = newdatakasus;
                    newdatakasusdetail.datagejala = dkdetail.datagejala;
                    newdatakasusdetail.deskripsi = dkdetail.deskripsi;
                    newdatakasus.DataKasusDetail.Add(newdatakasusdetail);
                }

                XPCollection<DataSolusi> querysolusi = new XPCollection<DataSolusi>(Session);
                CriteriaOperator criteiasolusi;
                criteiasolusi = BinaryOperator.Parse("datakasus = '" + xxx.datakasus.Oid.ToString() + "'");
                querysolusi.Criteria = criteiasolusi;
                foreach (DataSolusi datasolusii in querysolusi)
                {
                    newdatasolusi = new DataSolusi(Session);
                    newdatasolusi.datakasus = newdatakasus;
                    newdatasolusi.pengobatan = datasolusii.pengobatan;
                    newdatasolusi.kodesolusi = datasolusii.kosulauto();
                    newdatakasus.DataSolusi.Add(newdatasolusi);
                }


                newdatakedekatan.datakasus = xxx.datakasus;
                newdatakedekatan.datakasus2 = newdatakasus;
                newdatakedekatan.maxkedekatan = xxx.total;
                newdatakedekatan.Save();
                newdatakasus.Save();

               
            }
           
            
        }

        protected override void OnSaving()
        {
            if (this.statuskeluhan.ToString() == DataKeluhanStatus.Proses.ToString())
            {
                this.ProsesPerhitunganKedekatan();
            }

            base.OnSaving();
        }

        [Association("DataKeluhan-DataKeluhanDetail")]
        public XPCollection<DataKeluhanDetail> DataKeluhanDetail
        {
            get
            {
                return GetCollection<DataKeluhanDetail>("DataKeluhanDetail");
            }
        }

        public void SPCBRPsoriasisInitializeDataPasien()
        {
            // Get Collection of ROWS (Multi Records), by NAF
            XPCollection<DataPasien> queryDataPasien = new XPCollection<DataPasien>(Session);

            CriteriaOperator criteria1;

            criteria1 = BinaryOperator.Parse("aktif like '" + true + "'");
            queryDataPasien.Filter = criteria1;
            this.QueryAvailableDataPasien = queryDataPasien;

        }

        public void SPCBRPsoriasisInitializeAikode()
        {
            XPCollection<DataKeluhan> queryaikode = new XPCollection<DataKeluhan>(Session);
            SortingCollection sort = new SortingCollection();
            sort.Add(new SortProperty("kodekeluhan", SortingDirection.Ascending));
            queryaikode.Sorting = sort;
            foreach (DataKeluhan theRow in queryaikode)
            {
                this.Aikode = (Int32.Parse(theRow.KodeKeluhan.Substring(2)) + 1).ToString();
            }
            if (this.Aikode != null)
            {
                if (this.Aikode.Length == 1)
                {
                    this.KodeKeluhan = "KL000" + this.Aikode;
                }
                else if (this.Aikode.Length == 2)
                {
                    this.KodeKeluhan = "KL00" + this.Aikode;
                }
                else if (this.Aikode.Length == 3)
                {
                    this.KodeKeluhan = "KL0" + this.Aikode;
                }
                else
                {
                    this.KodeKeluhan = "KL" + this.Aikode;
                }
            }
            else
            {
                this.KodeKeluhan = "KL0001";
            }
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
            result.Add(new TSDataKeluhan(XPObjectSpace.FindObjectSpaceByObject(this)));
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
