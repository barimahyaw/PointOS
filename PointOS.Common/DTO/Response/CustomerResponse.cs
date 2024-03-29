﻿using System;

namespace PointOS.Common.DTO.Response
{
    public class CustomerResponse : ResponseBody
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }
        public string NationalIdCardNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
    }
}
