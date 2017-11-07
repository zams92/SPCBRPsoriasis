using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using SPCBRPsoriasis.Module.BusinessObjects.Menu.Laporan;

namespace SPCBRPsoriasis.Module.Controllers
{
    public partial class DetailViewController : ViewController
    {
        const string keyCustomize = "Customize";
        public DetailViewController()
        {
            TargetViewType = ViewType.DetailView;
            TargetObjectType = typeof(ParameterHasilDiagnosa);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            DetailView detailView = (DetailView)View;
            string awalhuruf = SecuritySystem.CurrentUserName;
            if (awalhuruf.Substring(0, 1) == "P")
            {
                detailView.AllowNew[keyCustomize] = false;
                detailView.AllowEdit[keyCustomize] = false;
                detailView.AllowDelete[keyCustomize] = false;
            }
            
        }
    }
}
