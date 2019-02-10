using Crud.Common.Request;
using System.Linq;

namespace Crud.API.Extensions
{
    public static class CrudExtensions
    {
        public static IQueryable<Database.Crud> FilterCruds(this IQueryable<Database.Crud> Cruds, CrudRequest request)
        {
            IQueryable<Database.Crud> FilterCruds = Cruds;

            FilterCrudId(ref FilterCruds, request.Id);
            FilterFullName(ref FilterCruds, request.FullName);
            FilterEmailAddress(ref FilterCruds, request.EmailAddress);
            FilterPhoneNumber(ref FilterCruds, request.PhoneNumber);

            return FilterCruds;
        }

        private static void FilterCrudId(ref IQueryable<Database.Crud> FilterCruds, int? crudId = null)
        {
            if (crudId.HasValue && crudId.Value > 0)
                FilterCruds = FilterCruds.Where(__crud => __crud.CrudId == crudId);
        }
        private static void FilterFullName(ref IQueryable<Database.Crud> FilterCruds, string fullName = null)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
                FilterCruds = FilterCruds.Where(__crud => fullName.Contains(__crud.FullName));
        }
        private static void FilterEmailAddress(ref IQueryable<Database.Crud> FilterCruds, string emailAddress = null)
        {
            if (!string.IsNullOrWhiteSpace(emailAddress))
                FilterCruds = FilterCruds.Where(__crud => emailAddress.Contains(__crud.EmailAddress));
        }
        private static void FilterPhoneNumber(ref IQueryable<Database.Crud> FilterCruds, string phoneNumber = null)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
                FilterCruds = FilterCruds.Where(__crud => phoneNumber.Contains(__crud.PhoneNumber));
        }
    }
}