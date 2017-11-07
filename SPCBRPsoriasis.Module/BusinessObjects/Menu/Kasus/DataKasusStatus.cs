using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.StateMachine.NonPersistent;
using DevExpress.ExpressApp;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Kasus
{
    public enum DataKasusStatus
    {
        Pengisisan,
        Percobaan,
        Revisi,
        Validasi,
    }

    public class TSDataKasus : StateMachine<DataKasus>, IStateMachineUISettings
    {
        private IState StatusPengisian;

        public TSDataKasus(IObjectSpace objectSpace)
            : base(objectSpace)
        {
            StatusPengisian = new State(this, " Pengisian Data... ", DataKasusStatus.Pengisisan);
            IState StatusPercobaan = new State(this, " Percobaan... ", DataKasusStatus.Percobaan);
            IState StatusRevisi = new State(this, " Peninjauan Solusi Kembali... ", DataKasusStatus.Revisi);
            IState StatusValidasi = new State(this, " Penyimpanan Data ", DataKasusStatus.Validasi);

            // Status Percobaan

            Transition PerubahanPercobaan = new Transition(StatusPercobaan, StatusPercobaan.Caption, 1);
            PerubahanPercobaan.SaveAndCloseView = true;

            // Status Revisi

            Transition PerubahanRevisi = new Transition(StatusRevisi, StatusRevisi.Caption, 1);
            PerubahanRevisi.SaveAndCloseView = true;

            // Status Validasi
            Transition PerubahanValidasi = new Transition(StatusValidasi, StatusValidasi.Caption, 1);
            PerubahanValidasi.SaveAndCloseView = true;




            // Add Transition
            StatusPercobaan.Transitions.Add(PerubahanRevisi);
            StatusPercobaan.Transitions.Add(PerubahanValidasi);

            StatusRevisi.Transitions.Add(PerubahanPercobaan);

            StatusPengisian.Transitions.Add(PerubahanPercobaan);

            //Items

            disabledItem(DataKasusStatus.Pengisisan, StatusPengisian);
            disabledItem(DataKasusStatus.Percobaan, StatusPercobaan);
            disabledItem(DataKasusStatus.Revisi, StatusRevisi);
            disabledItem(DataKasusStatus.Validasi, StatusValidasi);




            States.Add(StatusPengisian);
            States.Add(StatusPercobaan);
            States.Add(StatusRevisi);
            States.Add(StatusValidasi);
        }

        public void disabledItem(DataKasusStatus flagState, IState approvedState)
        {

            switch (flagState)
            {

                case DataKasusStatus.Pengisisan:
                    {
                        StateAppearance TampilanKodeKasus = new StateAppearance(approvedState);
                        TampilanKodeKasus.TargetItems = "kodekasus";
                        TampilanKodeKasus.Enabled = true;

                        StateAppearance TampilanNamaPenyakit = new StateAppearance(approvedState);
                        TampilanNamaPenyakit.TargetItems = "namapenyakit";
                        TampilanNamaPenyakit.Enabled = true;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = true;

                        StateAppearance TampilanTanggalValidasiKasus = new StateAppearance(approvedState);
                        TampilanTanggalValidasiKasus.TargetItems = "tanggalvalidasikasus";
                        TampilanTanggalValidasiKasus.Enabled = false;

                        StateAppearance TampilanTanggalKasus = new StateAppearance(approvedState);
                        TampilanTanggalKasus.TargetItems = "tanggalkasus";
                        TampilanTanggalKasus.Enabled = true;

                        StateAppearance TampilanPilihanAktif = new StateAppearance(approvedState);
                        TampilanPilihanAktif.TargetItems = "aktif";
                        TampilanPilihanAktif.Enabled = false;


                        StateAppearance TampilanDataKasusDetail = new StateAppearance(approvedState);
                        TampilanDataKasusDetail.TargetItems = "DataKasusDetail";
                        TampilanDataKasusDetail.Enabled = true;

                    }
                    break;
                case DataKasusStatus.Percobaan:
                    {
                        StateAppearance TampilanKodeKasus = new StateAppearance(approvedState);
                        TampilanKodeKasus.TargetItems = "kodekasus";
                        TampilanKodeKasus.Enabled = false;

                        StateAppearance TampilanNamaPenyakit = new StateAppearance(approvedState);
                        TampilanNamaPenyakit.TargetItems = "namapenyakit";
                        TampilanNamaPenyakit.Enabled = false;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = false;

                        StateAppearance TampilanTanggalValidasiKasus = new StateAppearance(approvedState);
                        TampilanTanggalValidasiKasus.TargetItems = "tanggalvalidasikasus";
                        TampilanTanggalValidasiKasus.Enabled = false;

                        StateAppearance TampilanTanggalKasus = new StateAppearance(approvedState);
                        TampilanTanggalKasus.TargetItems = "tanggalkasus";
                        TampilanTanggalKasus.Enabled = false;

                        StateAppearance TampilanPilihanAktif = new StateAppearance(approvedState);
                        TampilanPilihanAktif.TargetItems = "aktif";
                        TampilanPilihanAktif.Enabled = false;


                        StateAppearance TampilanDataKasusDetail = new StateAppearance(approvedState);
                        TampilanDataKasusDetail.TargetItems = "DataKasusDetail";
                        TampilanDataKasusDetail.Enabled = false;

                    }
                    break;
                case DataKasusStatus.Revisi:
                    {
                        StateAppearance TampilanKodeKasus = new StateAppearance(approvedState);
                        TampilanKodeKasus.TargetItems = "kodekasus";
                        TampilanKodeKasus.Enabled = false;

                        StateAppearance TampilanNamaPenyakit = new StateAppearance(approvedState);
                        TampilanNamaPenyakit.TargetItems = "namapenyakit";
                        TampilanNamaPenyakit.Enabled = true;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = false;

                        StateAppearance TampilanTanggalValidasiKasus = new StateAppearance(approvedState);
                        TampilanTanggalValidasiKasus.TargetItems = "tanggalvalidasikasus";
                        TampilanTanggalValidasiKasus.Enabled = false;

                        StateAppearance TampilanTanggalKasus = new StateAppearance(approvedState);
                        TampilanTanggalKasus.TargetItems = "tanggalkasus";
                        TampilanTanggalKasus.Enabled = false;

                        StateAppearance TampilanPilihanAktif = new StateAppearance(approvedState);
                        TampilanPilihanAktif.TargetItems = "aktif";
                        TampilanPilihanAktif.Enabled = false;


                        StateAppearance TampilanDataKasusDetail = new StateAppearance(approvedState);
                        TampilanDataKasusDetail.TargetItems = "DataKasusDetail";
                        TampilanDataKasusDetail.Enabled = true;

                    }
                    break;
                case DataKasusStatus.Validasi:
                    {
                        StateAppearance TampilanKodeKasus = new StateAppearance(approvedState);
                        TampilanKodeKasus.TargetItems = "kodekasus";
                        TampilanKodeKasus.Enabled = false;

                        StateAppearance TampilanNamaPenyakit = new StateAppearance(approvedState);
                        TampilanNamaPenyakit.TargetItems = "namapenyakit";
                        TampilanNamaPenyakit.Enabled = false;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = false;

                        StateAppearance TampilanTanggalValidasiKasus = new StateAppearance(approvedState);
                        TampilanTanggalValidasiKasus.TargetItems = "tanggalvalidasikasus";
                        TampilanTanggalValidasiKasus.Enabled = false;

                        StateAppearance TampilanTanggalKasus = new StateAppearance(approvedState);
                        TampilanTanggalKasus.TargetItems = "tanggalkasus";
                        TampilanTanggalKasus.Enabled = false;

                        StateAppearance TampilanPilihanAktif = new StateAppearance(approvedState);
                        TampilanPilihanAktif.TargetItems = "aktif";
                        TampilanPilihanAktif.Enabled = false;


                        StateAppearance TampilanDataKasusDetail = new StateAppearance(approvedState);
                        TampilanDataKasusDetail.TargetItems = "DataKasusDetail";
                        TampilanDataKasusDetail.Enabled = false;

                    }
                    break;

            }


        }


        public override string Name
        {
            get { return "Change status to"; }
        }
        public override IState StartState { get { return StatusPengisian; } }
        public override string StatePropertyName
        {
            get { return "statuskasus"; }
        }

        public bool ExpandActionsInDetailView
        {
            get { return true; }
        }



    }

}
