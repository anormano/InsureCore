using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureCore.Module.BusinessObjects.BaseObjects
{
    public class EnumLibrary
    {
        public enum Gender
        {
            Undefined,
            Male,
            Female
        }

        public enum MaritalStatus
        {
            Undefined,
            Single,
            Maried,
            Divorced,
            Widowed
        }

        public enum Religion
        {
            Undefined,
            Buddha,
            Hindu,
            Islam,
            Kristen,
            Katolik,
            KongHuCu
        }

        public enum IdentityType
        {
            Undefined,
            KTP,
            SIM,
            Passpor
        }

        public enum InsuredRelationship
        {
            Undefined,
            Parent,
            Children,
            Spouse,
            Others
        }

        public enum PaymentPeriodType
        {
            Single,
            Month,
            Quarter,
            BiAnnual,
            Year

        }
        public enum PaymentTerm
        {
            Single,
            Monthly,
            Quarterly,
            BiAnually,
            Anually
        }

        public enum LifeInsuranceRequestFormStatus
        {
            Draft,
            Pending,
            Completed,
            Canceled,
            Denied
        }

        public enum Answer
        {
            Unanswered,
            Yes,
            No
        }

        public enum RequestFormAttachmentType
        {
            KTP,
            SIM,
            KK,
            MedicalCheckUp,
            SignedForm,
            Other
        }

        public enum CareerLevel
        {
            JuniorStaff,
            Staff,
            SeniorStaff,
            JuniorSupervisor,
            Supervisor,
            SeniorSupervisor,
            JuniorManager,
            Manager,
            SeniorManager,
            JuniorGeneralManager,
            GeneralManager,
            SeniorGeneralManager,
            AssociateDirector,
            Director,
            SeniorDirector,
            ViceCommissioner,
            Commissioner,
            SeniorCommissioner
        }

        public enum EmployementStatus
        {
            Active,
            Resigned,
            Terminated,
            Retired
        }

        public enum PolicyStatus
        {
            AwaitingApproval,
            AwaitingFirstPayment,
            Inforce,
            Lapse,
            Canceled,
            Completed
        }
    }
}
