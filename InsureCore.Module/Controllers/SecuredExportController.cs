using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using InsureCore.Module.BusinessObjects.Administration;

namespace InsureCore.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SecuredExportController : ViewController
    {
        public SecuredExportController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ExportController controller = Frame.GetController<ExportController>();
            if (controller != null)
            {
                controller.ExportAction.Executing += ExportAction_Executing;
                if (SecuritySystem.Instance is IRequestSecurity)
                {
                    controller.Active.SetItemValue("Security", SecuritySystem.IsGranted(new ExportPermissionRequest()));
                }
            }
            void ExportAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
            {
                SecuritySystem.Demand(new ExportPermissionRequest());
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
