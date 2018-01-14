using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using InsureCore.Module.BusinessObjects.HRM;
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;
using InsureCore.Module.BusinessObjects.General;
using InsureCore.Module.BusinessObjects.Life.Actuary;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace InsureCore.Module.BusinessObjects.Life.Acquisition
{
    [DefaultClassOptions]
    [NavigationItem(true, GroupName = "Workspace")]
    [Appearance("DisableProduct", Criteria = "BirthDay < #01/01/1900# AND Gender = 'Undefined'", TargetItems = "Product", Enabled = false)]
    [Appearance("DisableDateGender", Criteria = "Product <> null", TargetItems = "Gender,BirthDay", Enabled = false)]
    [Appearance("DisableAuditFields", TargetItems = "Agent,CreateDate", Enabled = false)]
    //[ImageName("BO_Contact")]
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
            CreateDate = DateTime.Now;
            Agent = Session.GetObjectByKey<Agent>(SecuritySystem.CurrentUserId);
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
        public string ApplicationNumber { get; set; }
        [RuleRequiredField]
        public string FullName { get; set; }
        [RuleRequiredField]
        [ImmediatePostData]
        public DateTime BirthDay { get; set; }
        [ImmediatePostData]
        public Gender Gender { get; set; }
        bool isSmoker;
        [ImmediatePostData]
        public bool IsSmoker
        {
            get
            {
                return isSmoker;
            }
            set
            {
                if (SetPropertyValue("IsSmoker", ref isSmoker, value))
                {
                    OnChanged("Riders");
                }
            }
        }

        public Occupation Occupation { get; set; }
        public Currency Currency { get; set; }
        public PaymentTerm PaymentTerm { get; set; }
        public int PaymentPlan { get; set; }
        public int NextYearAge
        {
            get
            {
                int nextYearAge = 0;
                if (BirthDay != DateTime.MinValue && !IsLoading)
                {
                    nextYearAge = CodeLibrary.CommonLibrary.CalculateAge(BirthDay, CreateDate.AddYears(1));
                }
                return nextYearAge;
            }
        }

        Product product;
        [ImmediatePostData]
        public Product Product
        {
            get
            {
                return product;
            }
            set
            {
                if (SetPropertyValue("Product", ref product, value))
                {
                    if (IsLoading)
                        return;
                    while (Riders.Count > 0)
                    {
                        ApplicationRider removeRider = Riders[0];
                        Riders.Remove(removeRider);
                    }

                    if (Product == null)
                        return;
                    PaymentPlan = Product.DefaultPaymentPlan;
                    ApplicationRider applicationRider = new ApplicationRider(this.Session);
                    applicationRider.Product = (BaseProduct)Session.GetObjectByKey<Product>(Product.Code);
                    applicationRider.SumInsured = Product.DefaultSumInsured;
                    Riders.Add(applicationRider);

                    if (product.RiderProducts.Count > 0)
                    {
                        foreach (RiderProduct rider in product.RiderProducts)
                        {
                            applicationRider = new ApplicationRider(this.Session);
                            applicationRider.Product = (BaseProduct)Session.GetObjectByKey<RiderProduct>(rider.Code);
                            applicationRider.SumInsured = rider.DefaultSumInsured;

                            Riders.Add(applicationRider);
                        }
                    }
                }
                OnChanged("Riders");
            }
        }
        [PersistentAlias("Riders.Sum(Premium)")]
        public decimal TotalPremium
        {
            get
            {
                if (Riders.Count > 0)
                    return (decimal)EvaluateAlias("TotalPremium");
                else
                    return 0;
            }
        }

        public DateTime CreateDate { get; set; }
        public Agent Agent { get; set; }

        [ModelDefault("Caption", "Products/Riders")]
        [Association("InsuranceApplication-Riders"), Aggregated]
        public XPCollection<ApplicationRider> Riders
        {
            get
            {
                return GetCollection<ApplicationRider>("Riders");
            }
        }
    }
}