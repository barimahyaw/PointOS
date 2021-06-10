using System;

namespace PointOS.DataAccess.Entities
{
    public class Subscription
    {
        public int Id { get; set; }
        public Guid GuidId { get; set; }
        public string Token { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public int SubscriptionTypeId { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CreatedUserId { get; set; }
        public ApplicationUser CreatedUser { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
