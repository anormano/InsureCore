namespace InsureCore.Module.Mobile {
    partial class InsureCoreMobileModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            // 
            // InsureCoreMobileModule
            // 
            this.RequiredModuleTypes.Add(typeof(InsureCore.Module.InsureCoreModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Mobile.SystemModule.SystemMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.CloneObject.Mobile.CloneObjectMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ConditionalAppearance.Mobile.ConditionalAppearanceMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.FileAttachments.Mobile.FileAttachmentsMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Localization.Mobile.LocalizationMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Maps.Mobile.MapsMobileModule));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.Mobile.ReportsMobileModuleV2));
			this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.Mobile.ValidationMobileModule));
        }

        #endregion
    }
}