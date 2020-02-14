using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IgnProtoView.Data;

namespace IgnProtoView.Models
{
    public class IgniteUserApplication
    {
        public int ApplicationId { get; set; }

        //PreQualificationQuestions
        public string ManagerName { get; set; }
        public string Title { get; set; }
        public string BuName { get; set; }
        public string Location { get; set; }
        public bool EmploymentOverOneYear { get; set; }
        public bool LongTermEmploymentEligibility { get; set; }
        public bool BachelorDegreeQualified { get; set; }

        public DateTime ApplicationCompletionDate { get; set; }
        public DateTime ManagerApplicationStatusChangeDate { get; set; }
        public DateTime UserApplicationCreationDate { get; set; }

        // Track Full Completion of 4 Questions
        public bool IsQualificationQuestionComplete { get; set; }

        #region Application User Nav

        public int FKApplicationStatusId { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }

        public QuestionToAnswer QuestionToAnswer { get; set; }

        public IgniteUser IgniteUser { get; set; }
        public int FkIgniteUserId { get; set; }


        #endregion
    }
}
