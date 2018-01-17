using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace MIAAPI.Utilities
{
    public static class My
    {
        public static Func<DbConnection> ConnectionFactory = () => new SqlConnection(Shared.con_string);

        #region "table definisions"

        public static DbTable Table_City = new DbTable
        {
            Name = "City",
            Columns = new string[] { "cityId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "cityId" }
        };

        public static DbTable Table_PersonLanguage = new DbTable
        {
            Name = "PersonLanguage",
            Columns = new string[] { "personLanguageId", "personId", "languageId", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "personLanguageId" }
        };

        public static DbTable Table_PersonBusinessProductType = new DbTable
        {
            Name = "PersonBusinessProductType",
            Columns = new string[] { "personBusinessProductTypeId", "personBusinessId", "productTypeId", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "personBusinessProductTypeId" }
        };

        public static DbTable Table_PersonBusiness = new DbTable
        {
            Name = "PersonBusiness",
            Columns = new string[] { "personBusinessId", "personId", "businessId", "positionName", "departmentName", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "personBusinessId" }
        };

        public static DbTable Table_Business = new DbTable
        {
            Name = "Business",
            Columns = new string[] { "businessId", "businessTypeId", "name", "address", "cityId", "townshipId", "foundedDate", "licenseNumber", "otherLicense", "typeOfOwnership", "numberOfEmployee", "website", "email", "fax", "industryTypeId", "capital", "annualIncome", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "businessId" }
        };

        public static DbTable Table_BusinessFacility = new DbTable
        {
            Name = "BusinessFacility",
            Columns = new string[] { "businessFacilityId", "businessId", "facilityId", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "businessFacilityId" }
        };

        public static DbTable Table_BusinessBranch = new DbTable
        {
            Name = "BusinessBranch",
            Columns = new string[] { "businessBranchId", "businessId", "name", "address", "cityId", "townshipId", "phone", "locLat", "locLong", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "businessBranchId" }
        };

        public static DbTable Table_Township = new DbTable
        {
            Name = "Township",
            Columns = new string[] { "townshipId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "townshipId" }
        };

        public static DbTable Table_Person = new DbTable
        {
            Name = "Person",
            Columns = new string[] { "personId", "name", "fatherName", "motherName", "nrc", "dateOfBirth", "race", "nationality", "religion", "gender", "homeAddress", "homeCityId", "homeTownshipId", "mobilePhone", "phone", "email", "socialProfileUrl", "chatPhone", "education", "professional", "biography", "nativeCityId", "photo", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "personId" }
        };

        public static DbTable Table_AppUser = new DbTable
        {
            Name = "AppUser",
            Columns = new string[] { "userId", "loginName", "password", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "userId" }
        };

        public static DbTable Table_BusinessType = new DbTable
        {
            Name = "BusinessType",
            Columns = new string[] { "businessTypeId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "businessTypeId" }
        };

        public static DbTable Table_Facility = new DbTable
        {
            Name = "Facility",
            Columns = new string[] { "facilityId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "facilityId" }
        };

        public static DbTable Table_IndustryType = new DbTable
        {
            Name = "IndustryType",
            Columns = new string[] { "industryTypeId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "industryTypeId" }
        };

        public static DbTable Table_Language = new DbTable
        {
            Name = "Language",
            Columns = new string[] { "languageId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "languageId" }
        };

        public static DbTable Table_MemberType = new DbTable
        {
            Name = "MemberType",
            Columns = new string[] { "memberTypeId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "memberTypeId" }
        };

        public static DbTable Table_Member = new DbTable
        {
            Name = "Member",
            Columns = new string[] { "memberId", "personId", "registrationNo", "registrationDate", "memberTypeId", "loginName", "password", "memberstatus", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "memberId" }
        };

        public static DbTable Table_OtherAssociation = new DbTable
        {
            Name = "OtherAssociation",
            Columns = new string[] { "otherAssociationId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "otherAssociationId" }
        };

        public static DbTable Table_ProductType = new DbTable
        {
            Name = "ProductType",
            Columns = new string[] { "productTypeId", "name", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "productTypeId" }
        };

        public static DbTable Table_Reason = new DbTable 
        {
            Name = "Reason",
            Columns = new string[] { "reasonId", "description", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "reasonId" }
            
        };

        public static DbTable Table_FiscalYear = new DbTable 
        {
            Name = "FiscalYear",
            Columns = new string[] { "fiscalYearId", "fiscalYear", "startDate", "endDate", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "fiscalYearId" }
        };

        public static DbTable Table_Committee = new DbTable 
        {
            Name = "Committee",
            Columns = new string[] { "committeeId", "fiscalYearId", "name", "objective", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "committeeId" }
        };

        public static DbTable Table_AccountHead = new DbTable 
        {
            Name = "AccountHead",
            Columns = new string[] { "accountHeadId", "name", "accountType", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "accountHeadId" }
        };

        public static DbTable Table_Position = new DbTable 
        {
            Name = "Position",
            Columns = new string[] { "positionId", "committeeId", "parentPositionId", "name", "jobDescription", "appUserId", "entryDate", "modifiedDate", "remark", "recordStatus" },
            Keys = new string[] { "positionId" }
        };

        #endregion


        #region "extension functions"
        public static string GetColumnNames(this string[] columns)
        {
            string result = "";
            foreach (string c in columns)
                result = result + $"{c}, ";

            return result.Length > 0 ? result.Substring(0, result.Length - 2) : "";
        }

        public static string GetInsertParams(this string[] columns)
        {
            string result = "";
            foreach (string c in columns)
                result = result + $"@{c}, ";

            return result.Length > 0 ? result.Substring(0, result.Length - 2) : "";
        }

        public static string GetUpdateParams(this string[] columns)
        {
            string result = "";
            foreach (string c in columns)
                result = result + $"{c}=@{c}, ";

            return result.Length > 0 ? result.Substring(0, result.Length - 2) : "";
        }

        public static string GetWhere(this string[] columns)
        {
            string result = "";
            foreach (string c in columns)
                result = result + $"{c}=@{c} AND ";

            return " WHERE " +  ((result.Length>0)? result.Substring(0, result.Length - 4) : "");
        }
        #endregion
    }
}
