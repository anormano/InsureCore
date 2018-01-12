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
using InsureCore.Module.BusinessObjects.Life.Actuary;
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;

namespace InsureCore.Module.BusinessObjects.Life.Acquisition
{
    [DefaultClassOptions]
    [NavigationItem(false, GroupName = "Workspace")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class ApplicationRider : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ApplicationRider(Session session)
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
        [Association("InsuranceApplication-Riders")]
        public InsuranceApplication Application { get; set; }
        public BaseProduct Product { get; set; }
        public int? AgeLimit
        {
            get { return Product != null ? Product.CoverageAgeLimit : null; }
        }
        decimal sumInsured;
        [ImmediatePostData]
        public decimal SumInsured
        {
            get
            {
                return sumInsured;
            }
            set
            {
                if (SetPropertyValue("SumInsured", ref sumInsured, value))
                    OnChanged("Premium");
            }
        }
        public CoverageTerm? Term
        {
            get { return Product != null ? Product.Term : null; }
        }

        public decimal Premium
        {
            get
            {
                decimal premium = 0;
                decimal rate = 0;
                if (!IsLoading && Product != null)
                {
                    if (Product.Rate != null && Product.Rate.Rates.Count > 0)
                    {
                        foreach (PremiumRateDetail detail in Product.Rate.Rates)
                        {
                            if (Application.IsSmoker == false && Application.Gender == Gender.Female)
                                rate = detail.Female;
                            if (Application.IsSmoker == true && Application.Gender == Gender.Female)
                                rate = detail.FemaleSmoker;
                            if (Application.IsSmoker == false && Application.Gender == Gender.Male)
                                rate = detail.Male;
                            if (Application.IsSmoker == true && Application.Gender == Gender.Male)
                                rate = detail.MaleSmoker;

                            if (Application.NextYearAge <= detail.MaxAge)
                            {
                                if (Application.IsSmoker == false && Application.Gender == Gender.Female)
                                    rate = detail.Female;
                                if (Application.IsSmoker == true && Application.Gender == Gender.Female)
                                    rate = detail.FemaleSmoker;
                                if (Application.IsSmoker == false && Application.Gender == Gender.Male)
                                    rate = detail.Male;
                                if (Application.IsSmoker == true && Application.Gender == Gender.Male)
                                    rate = detail.MaleSmoker;
                            }
                        }
                        premium = (rate / 100) * SumInsured;
                    }
                }
                return premium;
            }
        }
    }
}