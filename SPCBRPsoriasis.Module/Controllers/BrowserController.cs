using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Pasien;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;

namespace SPCBRPsoriasis.Module.Controllers
{
    public partial class BrowserControler : ObjectViewController
    {
        public string ListView;

        public string DataPasien_ListView = "DataPasien_ListView";
        public string DataKeluhan_ListView = "DataKeluhan_ListView";
        
        public BrowserControler()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewChanging(View view)
        {
            base.OnViewChanging(view);
            ListView = view.Id.ToString();

            this.TargetObjectType = null;

            this.valid_Menu();

        }

        protected override void OnActivated()
        {
            base.OnActivated();

            //Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("myReason", false);
            //Frame.GetController<LinkUnlinkController>().UnlinkAction.Active.SetItemValue("myReason", false);

            this.Activate_Menu();

        }

        public void valid_Menu()
        {
            if (ListView == DataPasien_ListView)
            {
                this.filteringCriterionAction.TargetObjectType = typeof(DataPasien);
            }
            if (ListView == DataKeluhan_ListView)
            {
                this.filteringCriterionAction.TargetObjectType = typeof(DataKeluhan);
            }

        }

        public void Activate_Menu()
        {
            if (ListView == DataPasien_ListView)
            {
                string awalhuruf = SecuritySystem.CurrentUserName;
                if (awalhuruf.Substring(0, 1) == "P")
                {
                   
                        ((ListView)View).CollectionSource.Criteria.Clear();
                        ((ListView)View).CollectionSource.Criteria["AutomaticBrowse"] =
                            CriteriaEditorHelper.GetCriteriaOperator(
                            "kodepasien = '" + awalhuruf + "'", View.ObjectTypeInfo.Type, ObjectSpace);
                    
                }
            }
            if (ListView == DataKeluhan_ListView)
            {
                string awalhuruf = SecuritySystem.CurrentUserName;
                if (awalhuruf.Substring(0, 1) == "P")
                {

                    ((ListView)View).CollectionSource.Criteria.Clear();
                    ((ListView)View).CollectionSource.Criteria["AutomaticBrowse"] =
                        CriteriaEditorHelper.GetCriteriaOperator(
                        "datapasien.kodepasien = '" + awalhuruf + "'", View.ObjectTypeInfo.Type, ObjectSpace);

                }
            }
        }

        
    }
}
