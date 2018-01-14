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
using InsureCore.Module.BusinessObjects.Area;
using InsureCore.Module.BusinessObjects.HRM;

namespace InsureCore.Module.BusinessObjects.General
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Name")]
    [NavigationItem("Administration")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Branch : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Branch(Session session)
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

        public BaseEmployee Manager { get; set; }

        [ImmediatePostData]
        public Area.Country Country { get; set; }
        [ImmediatePostData]
        [DataSourceProperty("Country.Provinces")]
        public Province Province { get; set; }
        [ImmediatePostData]
        [DataSourceProperty("Province.Districts")]
        public District District { get; set; }
        [ImmediatePostData]
        [DataSourceProperty("District.SubDistricts")]
        public SubDistrict SubDistrict { get; set; }
        [DataSourceProperty("SubDistrict.Villages")]
        public Village Village { get; set; }

        public string Phone { get; set; }
        public string Fax { get; set; }

        [Association("Branch-Employees")]
        public XPCollection<BaseEmployee> Employees
        {
            get
            {
                return GetCollection<BaseEmployee>("Employees");
            }
        }
    }
}