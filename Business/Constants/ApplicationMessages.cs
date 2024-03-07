using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class ApplicationMessages
    {
        public static string ApplicationAdded = "Application Added Successfully";
        public static string ApplicationDeleted = "Application Deleted Successfully";
        public static string ApplicationUpdated = "Application Updated Successfully";
        public static string ApplicationGetAll = "Applications Listed Successfully";
        public static string ApplicationGetById = "The Application Was Successfully Brought According To The ID";
        public static string ApplicationIdNotExist = "Application Id not exists";
        public static string ApplicantIsBlacklisted = "Application could not be created because this applicant is blacklisted.";
        public static string ApplicantAlreadyApplied = "Applicant has already applied to this bootcamp.";


    }
}
