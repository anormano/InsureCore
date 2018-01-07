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
using InsureCore.Module.BusinessObjects.Life.Acquisition;
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;

namespace InsureCore.Module.BusinessObjects.Life.Insured
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class InsurancePolicy : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InsurancePolicy(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
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
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
        public String PolicyNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodEnd { get; set; }
        public string VirtualAccount { get; set; }
        public PolicyStatus Status { get; set; }

        InsuranceApplication insuranceApplication = null;
        public InsuranceApplication InsuranceApplication
        {
            get
            {
                return insuranceApplication;
            }
            set
            {
                if (insuranceApplication == value)
                    return;
                InsuranceApplication prevInsuranceApplication = insuranceApplication;
                insuranceApplication = value;
                if (IsLoading) return;
                if (prevInsuranceApplication != null && prevInsuranceApplication.Policy == this)
                    prevInsuranceApplication.Policy = null;
                if (insuranceApplication != null)
                    insuranceApplication.Policy = this;
                OnChanged("InsuranceApplication");
            }
        }


        [Association("Policy-Premiums"), DevExpress.Xpo.Aggregated]
        public XPCollection<PolicyPremium> Premiums
        {
            get
            {
                return GetCollection<PolicyPremium>("Premiums");
            }
        }
    }
}