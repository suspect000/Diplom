using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dickplom1.DataFolder
{
    public class StaffViewModel
    {
        public int UserId { get; set; }
        public int AccountStatusId { get; set; }
        public int UserDataId { get; set; }
        public int Number { get; set; }
        public int UserPasswordDataId { get; set; }
        public int RoleId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string FIOStaff { get; set; }
        public string Role { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int CreatorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AccountStatus { get; set; }
        public double KPI { get; set; } // например, от 0 до 100
        public bool IsDeleted { get; set; }
        public byte[] UserPhoto { get; set; }
    }
}
