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

namespace InsureCore.Module.BusinessObjects.Life.Actuary
{
    [DefaultClassOptions]
    [NavigationItem(false, GroupName = "Actuary")]
    [CreatableItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class PremiumRateDetail : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public PremiumRateDetail(Session session)
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
        [Association("PremiumRate-Rates")]
        public PremiumRate PremiumRate { get; set; }
        public int MaxAge { get; set; }
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "P2")]
        public decimal Male { get; set; }
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "P2")]
        public decimal MaleSmoker { get; set; }
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "P2")]
        public decimal Female { get; set; }
        [ModelDefault("DisplayFormat", "{0:n2} %")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "P2")]
        public decimal FemaleSmoker { get; set; }
    }
}