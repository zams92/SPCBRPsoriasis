using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.StateMachine;
using DevExpress.ExpressApp.StateMachine.NonPersistent;
using DevExpress.ExpressApp;

namespace SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien
{
    public enum DataKeluhanStatus
    {
        Pengisisan,
        Proses,
    }

    public class TSDataKeluhan : StateMachine<DataKeluhan>, IStateMachineUISettings
    {
        private IState StatusPengisian;

        public TSDataKeluhan(IObjectSpace objectSpace)
            : base(objectSpace)
        {
            StatusPengisian = new State(this, " Pengisian Data... ", DataKeluhanStatus.Pengisisan);
            IState StatusProses = new State(this, " Proses... ", DataKeluhanStatus.Proses);

            // Status Proses

            Transition PerubahanProses = new Transition(StatusProses, StatusProses.Caption, 1);
            PerubahanProses.SaveAndCloseView = true;

            

           
            StatusPengisian.Transitions.Add(PerubahanProses);

            //Items

            disabledItem(DataKeluhanStatus.Pengisisan, StatusPengisian);
            disabledItem(DataKeluhanStatus.Proses, StatusProses);




            States.Add(StatusPengisian);
            States.Add(StatusProses);
        }

        public void disabledItem(DataKeluhanStatus flagState, IState approvedState)
        {

            switch (flagState)
            {

                case DataKeluhanStatus.Pengisisan:
                    {
                        StateAppearance TampilanKodeKeluhan = new StateAppearance(approvedState);
                        TampilanKodeKeluhan.TargetItems = "kodekeluhan";
                        TampilanKodeKeluhan.Enabled = true;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = true;

                        StateAppearance TampilanTanggalKeluhan = new StateAppearance(approvedState);
                        TampilanTanggalKeluhan.TargetItems = "tanggalkeluhan";
                        TampilanTanggalKeluhan.Enabled = true;

                        StateAppearance TampilanCatatan = new StateAppearance(approvedState);
                        TampilanCatatan.TargetItems = "catatan";
                        TampilanCatatan.Enabled = false;


                        StateAppearance TampilanDataKeluhanDetail = new StateAppearance(approvedState);
                        TampilanDataKeluhanDetail.TargetItems = "DataKeluhanDetail";
                        TampilanDataKeluhanDetail.Enabled = true;

                    }
                    break;
                case DataKeluhanStatus.Proses:
                    {
                        StateAppearance TampilanKodeKeluhan = new StateAppearance(approvedState);
                        TampilanKodeKeluhan.TargetItems = "kodekeluhan";
                        TampilanKodeKeluhan.Enabled = false;

                        StateAppearance TampilanDataPasien = new StateAppearance(approvedState);
                        TampilanDataPasien.TargetItems = "datapasien";
                        TampilanDataPasien.Enabled = false;

                        StateAppearance TampilanTanggalKeluhan = new StateAppearance(approvedState);
                        TampilanTanggalKeluhan.TargetItems = "tanggalkeluhan";
                        TampilanTanggalKeluhan.Enabled = false;

                        StateAppearance TampilanCatatan = new StateAppearance(approvedState);
                        TampilanCatatan.TargetItems = "catatan";
                        TampilanCatatan.Enabled = false;

                        StateAppearance TampilanDataKeluhanDetail = new StateAppearance(approvedState);
                        TampilanDataKeluhanDetail.TargetItems = "DataKeluhanDetail";
                        TampilanDataKeluhanDetail.Enabled = false;

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
            get { return "statuskeluhan"; }
        }

        public bool ExpandActionsInDetailView
        {
            get { return true; }
        }



    }
}
