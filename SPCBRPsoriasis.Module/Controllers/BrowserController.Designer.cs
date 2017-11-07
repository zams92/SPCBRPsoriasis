namespace SPCBRPsoriasis.Module.Controllers
{
    partial class BrowserControler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.filteringCriterionAction = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // filteringCriterionAction
            // 
            this.filteringCriterionAction.Caption = "filtering Criterion Action";
            this.filteringCriterionAction.ConfirmationMessage = null;
            this.filteringCriterionAction.Id = "filteringCriterionAction";
            this.filteringCriterionAction.ImageName = null;
            this.filteringCriterionAction.Shortcut = null;
            this.filteringCriterionAction.Tag = null;
            this.filteringCriterionAction.TargetObjectsCriteria = null;
            this.filteringCriterionAction.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            this.filteringCriterionAction.TargetViewId = null;
            this.filteringCriterionAction.ToolTip = null;
            this.filteringCriterionAction.TypeOfView = null;

            //this.filteringCriterionAction.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.FilteringCriterionAction_Execute);
            // 
            // CriteriaController
            // 


            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            //this.Activated += new System.EventHandler(this.OnActivated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction filteringCriterionAction;
    }
}
