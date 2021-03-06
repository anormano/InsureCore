﻿using System;
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
    [ImageName("BO_Product")]
    [DefaultProperty("Name")]
    [CreatableItem(false)]
    [NavigationItem(false, GroupName = "Actuary")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class BaseProduct : XPLiteObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public BaseProduct(Session session)
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
        //    this.PersistentProperty = "Paidpublic bool IsActive { get; set; }";
        //}
        [RuleRequiredField]
        [RuleUniqueValue]
        [Key]
        public string Code { get; set; }
        [RuleRequiredField]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public PremiumRate Rate { get; set; }
        [RuleRange(1, 100)]
        public int InsurancePeriod { get; set; }
        public int MaximumAge { get; set; }
        public int MedicalRequirementAge { get; set; }
        public decimal DefaultSumInsured { get; set; }
        public int DefaultPaymentPlan { get; set; }
        public int? CoverageAgeLimit { get; set; }
        public CoverageTerm? Term { get; set; }
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.HtmlPropertyEditor)]
        public string Description { get; set; }
    }
}