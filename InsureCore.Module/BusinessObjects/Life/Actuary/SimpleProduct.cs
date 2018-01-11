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
using DevExpress.ExpressApp.Editors;
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;

namespace InsureCore.Module.BusinessObjects.Life.Actuary
{
    [DefaultClassOptions]
    [NavigationItem("Actuary")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class SimpleProduct : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SimpleProduct(Session session)
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
        [RuleRequiredField]
        public string Name { get; set; }
        [RuleValueComparison(ValueComparisonType.GreaterThan, 0)]
        public int PaymentPeriod { get; set; }
        public PaymentPeriodType PeriodType { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public bool IsActive { get; set; }


        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        [Size(SizeAttribute.Unlimited)]
        public string Description { get; set; }


        [Association("Product-SumInsuredSpecifications"), DevExpress.Xpo.Aggregated]
        public XPCollection<SumInsuredSpecification> SumInsuredSpecifications
        {
            get
            {
                return GetCollection<SumInsuredSpecification>("SumInsuredSpecifications");
            }
        }

        [Association("Product-Benefits")]
        public XPCollection<ProductBenefit> Benefits
        {
            get
            {
                return GetCollection<ProductBenefit>("Benefits");
            }
        }

        [Association("Product-InvestmentAllocations"), DevExpress.Xpo.Aggregated]
        public XPCollection<InvestmentAllocation> InvestmentAllocations
        {
            get
            {
                return GetCollection<InvestmentAllocation>("InvestmentAllocations");
            }
        }
    }
}