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
using InsureCore.Module.BusinessObjects.Life.Actuary;
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;
using InsureCore.Module.BusinessObjects.HRM;
using InsureCore.Module.BusinessObjects.Area;
using DevExpress.ExpressApp.ConditionalAppearance;
using InsureCore.Module.BusinessObjects.Life.Insured;

namespace InsureCore.Module.BusinessObjects.Life.Acquisition
{
    [DefaultClassOptions]
    [NavigationItem("Workspace")]
    [Appearance("HideInsured", "IsNotInsured=False", TargetItems = "InsuredGroup", Visibility = ViewItemVisibility.Hide, AppearanceItemType = "LayoutItem")]
    //[Appearance("ShowInsured", "IsNotInsured=True", TargetItems = "InsuredGroup", Visibility = ViewItemVisibility.Show, AppearanceItemType = "LayoutItem")]
    [Appearance("HideOtherEntry", "Other=False", TargetItems = "OtherEntry", Visibility = ViewItemVisibility.Hide, AppearanceItemType = "ViewItem")]
    [Appearance("ShowOtherEntry", "Other=True", TargetItems = "OtherEntry", Visibility = ViewItemVisibility.Show, AppearanceItemType = "ViewItem")]
    [Appearance("ShowExplanation", "IsHospitalized = true or IsDiabetic = true or IsCancer = true or IsHepatitic = true or IsKidneyDefect = true or IsBoneDefect = true or IsBloodDefect = true or IsHormonalDefect = true or IsAsthma = true or IsAids = true or IsCongenital = true or Other = true", TargetItems = "Explanation", Visibility = ViewItemVisibility.Show, AppearanceItemType = "ViewItem")]
    [Appearance("HideExplanation", "IsHospitalized = false and IsDiabetic = false and IsCancer = false and IsHepatitic = false and IsKidneyDefect = false and IsBoneDefect = false and IsBloodDefect = false and IsHormonalDefect = false and IsAsthma = false and IsAids = false and IsCongenital = false and Other = false", TargetItems = "Explanation", Visibility = ViewItemVisibility.Hide, AppearanceItemType = "ViewItem")]
    [ModelDefault("Caption", "Insurance Application")]
    [ImageName("BO_Contract")]
    [RuleCriteria("ExplanationRequired", DefaultContexts.Save, @"(IsHospitalized OR IsDiabetic OR IsCancer OR IsHepatitic OR IsKidneyDefect OR IsBoneDefect OR IsHormonalDefect OR IsAsthma OR IsAids OR IsCongenital OR Other Or IsBloodDefect) AND IsNullOrEmpty(Explanation)", SkipNullOrEmptyValues = false, InvertResult = true)]
    [RuleCriteria("OtherPreExistingRequired", DefaultContexts.Save, @"Other AND IsNullOrEmpty(OtherEntry)", SkipNullOrEmptyValues = false, InvertResult = true)]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class InsuranceApplication : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public InsuranceApplication(Session session)
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

        public string FormNumber { get; set; }
        [ImmediatePostData(true)]
        public Product Product { get; set; }
        public Agent Agent { get; set; }
        public LifeInsuranceRequestFormStatus Status { get; set; }
        [DataSourceProperty("Product.InvestmentAllocations")]
        public InvestmentAllocation InvestmentAllocation { get; set; }
        [DataSourceProperty("Product.SumInsuredSpecifications")]
        [ImmediatePostData]
        public SumInsuredSpecification SumInsured { get; set; }

        InsurancePolicy policy = null;
        public InsurancePolicy Policy
        {
            get
            {
                return policy;
            }
            set
            {
                if (policy == value)
                    return;
                InsurancePolicy prevPolicy = policy;
                policy = value;
                if (IsLoading) return;
                if (prevPolicy != null && prevPolicy.InsuranceApplication == this)
                    prevPolicy.InsuranceApplication = null;
                if (policy != null)
                    policy.InsuranceApplication = this;
                OnChanged("Policy");
            }
        }

        [ModelDefault("Caption", "Full Name")]
        public string PolicyHolderName { get; set; }
        [ModelDefault("Caption", "Gender")]
        [VisibleInListView(false)]
        public Gender PolicyHolderGender { get; set; }
        [ModelDefault("Caption", "Birth Date")]
        [VisibleInListView(false)]
        public DateTime PolicyHolderBirthDate { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Birth Place")]
        public string PolicyHolderBirthPlace { get; set; }
        [ModelDefault("Caption", "Religion")]
        [VisibleInListView(false)]
        public Religion PolicyHolderReligion { get; set; }
        [ModelDefault("Caption", "Identity Type")]
        [VisibleInListView(false)]
        public IdentityType PolicyHolderIdentityType { get; set; }
        [ModelDefault("Caption", "Identity Number")]
        [VisibleInListView(false)]
        public string PolicyHolderIdentityNumber { get; set; }


        [ModelDefault("Caption", "Address")]
        [ModelDefault("RowCount", "2")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        public string PolicyHolderAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country PolicyHolderCountry { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderCountry.Provinces")]
        [ModelDefault("Caption", "State/Province")]
        public Province PolicyHolderProvince { get; set; }
        [ImmediatePostData]
        [VisibleInListView(false)]
        [DataSourceProperty("PolicyHolderProvince.Districts")]
        [ModelDefault("Caption", "District/Regency")]
        public District PolicyHolderDistrict { get; set; }
        [ImmediatePostData]
        [VisibleInListView(false)]
        [DataSourceProperty("PolicyHolderDistrict.SubDistricts")]
        [ModelDefault("Caption", "Sub District")]
        public SubDistrict PolicyHolderSubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string PolicyHolderPostalCode { get; set; }


        [VisibleInListView(false)]
        [ModelDefault("Caption", "Address")]
        [ModelDefault("RowCount", "2")]
        [Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        public string PolicyHolderMailingAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country PolicyHolderMailingCountry { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderMailingCountry.Provinces")]
        [ModelDefault("Caption", "State/Province")]
        public Province PolicyHolderMailingProvince { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderMailingProvince.Districts")]
        [ModelDefault("Caption", "District/Regency")]
        public District PolicyHolderMailingDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Sub District")]
        [DataSourceProperty("PolicyHolderMailingDistrict.SubDistricts")]
        public SubDistrict PolicyHolderMailingSubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string PolicyHolderMailingPostalCode { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Handphone")]
        public string PolicyHolderHandphone { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "E-Mail")]
        public string PolicyHolderEmail { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Occupation")]
        public string PolicyHolderOccupation { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Position")]
        public string PolicyHolderPosition { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Company")]
        public string PolicyHolderCompany { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Address")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "2")]
        [Size(SizeAttribute.Unlimited)]
        public string PolicyHolderCompanyAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country PolicyHolderCompanyCountry { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "State/Province")]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderCompanyCountry.Provinces")]
        public Province PolicyHolderCompanyProvince { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderCompanyProvince.Districts")]
        [ModelDefault("Caption", "District/Regency")]
        public District PolicyHolderCompanyDistrict { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("PolicyHolderCompanyDistrict.SubDistricts")]
        [ModelDefault("Caption", "Sub District")]
        public SubDistrict PolicyHolderCompanySubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string PolicyHolderCompanyPostalCode { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:#.## CM}")]
        [ModelDefault("Caption", "Height")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "n")]
        public decimal PolicyHolderHeight { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("DisplayFormat", "{0:#.## KG}")]
        [ModelDefault("Caption", "Weight")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "n")]
        public decimal PolicyHolderWeight { get; set; }

        [ModelDefault("Caption", "Policy Holder is not Insured")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public bool IsNotInsured { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Full Name")]
        public string InsuredName { get; set; }
        [ModelDefault("Caption", "Gender")]
        [VisibleInListView(false)]
        public Gender InsuredGender { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Birth Date")]
        public DateTime InsuredBirthDate { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Birth Place")]
        public string InsuredBirthPlace { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Religion")]
        public Religion InsuredReligion { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Identity Number")]
        public string InsuredIdentityNumber { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Identity Type")]
        public IdentityType InsuredIdentityType { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Address")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "2")]
        [Size(SizeAttribute.Unlimited)]
        public string InsuredAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country InsuredCountry { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredCountry.Provinces")]
        [ModelDefault("Caption", "State/Province")]
        public Province InsuredProvince { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredProvince.Districts")]
        [ModelDefault("Caption", "District/Regency")]
        public District InsuredDistrict { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredDistrict.SubDistricts")]
        [ModelDefault("Caption", "Sub District")]
        public SubDistrict InsuredSubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string InsuredPostalCode { get; set; }


        [VisibleInListView(false)]
        [ModelDefault("Caption", "Address")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "2")]
        [Size(SizeAttribute.Unlimited)]
        public string InsuredMailingAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country InsuredMailingCountry { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "State/Province")]
        [ImmediatePostData]
        [DataSourceProperty("InsuredMailingCountry.Provinces")]
        public Province InsuredMailingProvince { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "District/Regency")]
        [ImmediatePostData]
        [DataSourceProperty("InsuredMailingProvince.Districts")]
        public District InsuredMailingDistrict { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredMailingDistrict.SubDistricts")]
        [ModelDefault("Caption", "Sub-District")]
        public SubDistrict InsuredMailingSubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string InsuredMailingPostalCode { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Handphone")]
        public string InsuredHandphone { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "E-Mail")]
        public string InsuredEmail { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Occupation")]
        public string InsuredOccupation { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Position")]
        public string InsuredPosition { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Company")]
        public string InsuredCompany { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Address")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [ModelDefault("RowCount", "2")]
        [Size(SizeAttribute.Unlimited)]
        public string InsuredCompanyAddress { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Country")]
        [ImmediatePostData]
        public Area.Country InsuredCompanyCountry { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredCompanyCountry.Provinces")]
        [ModelDefault("Caption", "State/Province")]
        public Province InsuredCompanyProvince { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData]
        [DataSourceProperty("InsuredCompanyProvince.Districts")]
        [ModelDefault("Caption", "District/Regency")]
        public District InsuredCompanyDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Sub District")]
        [ImmediatePostData]
        [DataSourceProperty("InsuredCompanyDistrict.SubDistricts")]
        public SubDistrict InsuredCompanySubDistrict { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Postal Code")]
        public string InsuredCompanyPostalCode { get; set; }

        [VisibleInListView(false)]
        [ModelDefault("Caption", "Height")]
        [ModelDefault("DisplayFormat", "{0:#.## CM}")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "n")]
        public decimal InsuredHeight { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Weight")]
        [ModelDefault("DisplayFormat", "{0:#.## KG}")]
        [ModelDefault("EditMaskType", "Simple")]
        [ModelDefault("EditMask", "n")]
        public decimal InsuredWeight { get; set; }

        [VisibleInListView(false)]
        public InsuredRelationship Relationship { get; set; }
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Other")]
        public string OtherRelationship { get; set; }

        #region Pre-Existing
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsHospitalized { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsDiabetic { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsCancer { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsHepatitic { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsKidneyDefect { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsBoneDefect { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsBloodDefect { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsHormonalDefect { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsAsthma { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsAids { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool IsCongenital { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public bool Other { get; set; }
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        public string OtherEntry { get; set; }
        [VisibleInListView(false)]
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "2")]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        
        
        public string Explanation { get; set; }
        #endregion

        [RuleCriteria("Must100", DefaultContexts.Save, @"InsuranceApplication.Beneficiaries.Sum([SumInsuredPercent]) = 100", InvertResult = false, SkipNullOrEmptyValues = true)]
        [RuleRequiredField]
        [Association("InsuranceApplication-Beneficiaries"), DevExpress.Xpo.Aggregated]
        public XPCollection<Beneficiary> Beneficiaries
        {
            get
            {
                return GetCollection<Beneficiary>("Beneficiaries");
            }
        }
    }
}