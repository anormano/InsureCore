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
using static InsureCore.Module.BusinessObjects.BaseObjects.EnumLibrary;
using DevExpress.ExpressApp.Editors;
using InsureCore.Module.BusinessObjects.Area;
using InsureCore.Module.CodeLibrary;

namespace InsureCore.Module.BusinessObjects.BaseObjects
{
    [NavigationItem(false), CreatableItem(false), DefaultProperty("DisplayName"), VisibleInReports(false), ImageName("BO_Employee")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class People : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public People(Session session)
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
        [PersistentAlias("Concat([FirstName], ' ', [LastName])")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        public string DisplayName
        {
            get
            {
                return (string)EvaluateAlias("DisplayName");
            }
        }

        string firstName;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize), ImmediatePostData(true)]
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                SetPropertyValue("FirstName", ref firstName, value);
            }
        }

        string lastName;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize), ImmediatePostData(true)]
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                SetPropertyValue("LastName", ref lastName, value);
            }
        }

        DateTime birthDate;
        [VisibleInListView(false)]
        public DateTime BirthDate
        {
            get
            {
                return birthDate;
            }
            set
            {
                SetPropertyValue("BirthDate", ref birthDate, value);
            }
        }

        int age;
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false), NonPersistent]
        public int Age
        {
            get
            {
                age = 0;
                if (BirthDate > DateTime.MinValue)
                {
                    age = CodeLibrary.CommonLibrary.CalculateAge(BirthDate, DateTime.Now);
                }
                return age;
            }
        }

        string birthPlace;
        [VisibleInListView(false)]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string BirthPlace
        {
            get
            {
                return birthPlace;
            }
            set
            {
                SetPropertyValue("BirthPlace", ref birthPlace, value);
            }
        }

        MediaDataObject photo;
        [VisibleInListView(true)]
        [ImageEditor(ListViewImageEditorCustomHeight = 80, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedHeight = 200)]
        public MediaDataObject Photo
        {
            get
            {
                return photo;
            }
            set
            {
                SetPropertyValue("Photo", ref photo, value);
            }
        }

        Gender gender;
        [VisibleInListView(false)]
        public Gender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                SetPropertyValue("Gender", ref gender, value);
            }
        }

        MaritalStatus maritalStatus;
        [VisibleInListView(false)]
        public MaritalStatus MaritalStatus
        {
            get
            {
                return maritalStatus;
            }
            set
            {
                SetPropertyValue("MaritalStatus", ref maritalStatus, value);
            }
        }

        string spouseName;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string SpouseName
        {
            get
            {
                return spouseName;
            }
            set
            {
                SetPropertyValue("SpouseName", ref spouseName, value);
            }
        }

        DateTime anniversary;
        [VisibleInListView(false)]
        public DateTime Anniversary
        {
            get
            {
                return anniversary;
            }
            set
            {
                SetPropertyValue("Anniversary", ref anniversary, value);
            }
        }

        string jobTitle;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string JobTitle
        {
            get
            {
                return jobTitle;
            }
            set
            {
                SetPropertyValue("JobTitle", ref jobTitle, value);
            }
        }

        string address;
        [VisibleInListView(false)]
        [EditorAlias(EditorAliases.StringPropertyEditor)]
        [Size(SizeAttribute.Unlimited)]
        [ModelDefault("RowCount", "3")]
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                SetPropertyValue("Address", ref address, value);
            }
        }

        InsureCore.Module.BusinessObjects.Area.Country country;
        [ImmediatePostData]
        [VisibleInListView(false)]
        public InsureCore.Module.BusinessObjects.Area.Country Country
        {
            get
            {
                return country;
            }
            set
            {
                SetPropertyValue("Country", ref country, value);
            }
        }

        Province province;
        [ImmediatePostData]
        [VisibleInListView(false)]
        [DataSourceProperty("Country.Provinces")]
        public Province Province
        {
            get
            {
                return province;
            }
            set
            {
                SetPropertyValue("Province", ref province, value);
            }
        }

        District district;
        [ImmediatePostData]
        [VisibleInListView(false)]
        [DataSourceProperty("Province.Districts")]
        public District District
        {
            get
            {
                return district;
            }
            set
            {
                SetPropertyValue("District", ref district, value);
            }
        }

        SubDistrict subDistrict;
        [DataSourceProperty("District.SubDistricts")]
        [VisibleInListView(false)]
        public SubDistrict SubDistrict
        {
            get
            {
                return subDistrict;
            }
            set
            {
                SetPropertyValue("SubDistrict", ref subDistrict, value);
            }
        }

        string email;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(true)]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                SetPropertyValue("Email", ref email, value);
            }
        }

        string officePhone;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(true)]
        public string OfficePhone
        {
            get
            {
                return officePhone;
            }
            set
            {
                SetPropertyValue("OfficePhone", ref officePhone, value);
            }
        }

        string homePhone;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string HomePhone
        {
            get
            {
                return homePhone;
            }
            set
            {
                SetPropertyValue("HomePhone", ref homePhone, value);
            }
        }

        string mobilePhone;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(true)]
        public string MobilePhone
        {
            get
            {
                return mobilePhone;
            }
            set
            {
                SetPropertyValue("MobilePhone", ref mobilePhone, value);
            }
        }
        string fax;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string Fax
        {
            get
            {
                return fax;
            }
            set
            {
                SetPropertyValue("Fax", ref fax, value);
            }
        }

        string otherPhone;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [VisibleInListView(false)]
        public string OtherPhone
        {
            get
            {
                return otherPhone;
            }
            set
            {
                SetPropertyValue("OtherPhone", ref otherPhone, value);
            }
        }
    }
}