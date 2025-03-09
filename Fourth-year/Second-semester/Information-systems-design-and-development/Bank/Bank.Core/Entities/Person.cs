using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Bank.Core.Entities
{
    public class Person : Entity<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string SeriaPassport { get; set; }

        public string PassportNumber { get; set; }

        public string KemVidan { get; set; }

        public DateTime DateVidan { get; set; }

        public string IdentNumber{get;set;}

        public string BirthPlace { get; set; }

        public Guid CityId { get; set; }

        public City City { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobPhone { get; set; }

        public string Email { get; set; }

        public string WorkPlace { get; set; }

        public string Position { get; set; }

        public FamilyStatus FamilyStatus { get; set; }

        public Nationality Nationality { get; set; }

        public Disability Disability { get; set; }

        public bool Pensioner { get; set; }

        public double? Salary { get; set; }

        public bool Army { get; set; }


        private Person()
        {
        }

        public static Person Create(string firstName, string lastName, string patronymic,
            DateTime dateOfBirth, string serPass, string passNum, string kemvidan, DateTime dateVidan,
            string idpass, string birthPlace, Guid cityId, string address, string hPhone,
            string mPhone, string email, string workPlace, string position, 
            FamilyStatus familyStatus, Nationality nationality, Disability disability, bool pens,
            string salary, bool army)
        {
            return new Person
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                DateOfBirth = dateOfBirth,
                SeriaPassport = serPass,
                PassportNumber = passNum,
                KemVidan = kemvidan,
                DateVidan = dateVidan,
                IdentNumber = idpass,
                BirthPlace = birthPlace,
                CityId = cityId,
                Address = address,
                HomePhone = hPhone,
                MobPhone = mPhone,
                Email = email,
                WorkPlace = workPlace,
                Position = position,
                FamilyStatus = familyStatus,
                Nationality = nationality,
                Disability = disability,
                Pensioner = pens,
                Salary = string.IsNullOrWhiteSpace(salary) ? (double?) null : Convert.ToDouble(salary, CultureInfo.InvariantCulture),
                Army = army
            };
        }

        public string GetFio()
        {
            return $"{LastName} {FirstName}";
        }
    }
}
