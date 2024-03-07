using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public class BlacklistMessages
    {
        public static string BlacklistAdded = "Blacklist Added Successfully";
        public static string BlacklistDeleted = "Blacklist Deleted Successfully";
        public static string BlacklistUpdated = "Blacklist Updated Successfully";
        public static string BlacklistGetAll = "Blacklists Listed Successfully";
        public static string BlacklistGetById = "The Blacklist Was Successfully Brought According To The Id";
        public static string BlacklistGetByApplicantId = "The Blacklist Was Successfully Brought According To The ApplicantId";
        public static string BlacklistIdNotExists = " Blacklist Id not exists.";
        public static string ApplicantAlreadyBlacklist = "Applicant is already blacklisted.";
    }
}
