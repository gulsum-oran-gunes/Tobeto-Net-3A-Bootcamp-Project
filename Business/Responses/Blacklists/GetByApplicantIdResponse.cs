﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Responses.Blacklists
{
    public class GetByApplicantIdResponse
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}
