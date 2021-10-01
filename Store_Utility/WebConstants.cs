using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Store_Utility
{
    public static class WebConstants
    {
        public const string ImagePath = @"\images\";
        public const string SessionCart = "ShoppingCartSession";
        public const string SessionInquiryId = "InquirySession";
        public const string CartIcon = "cart-white.png";
        public const string TrashIcon = "trash.png";
        public const string FacebookIcon = "facebook.png";
        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";
        public const string EmailAdmin = "***@gmail.com";
        public const string Host = "smtp.gmail.com";
        public const int Port = 587;
        public const string CategoryName = "Category";
        public const string ApplicationTypeName = "ApplicationType";
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";
        public static readonly IEnumerable<string> listStatus = new ReadOnlyCollection<string>(
            new List<string>
            {
                StatusApproved,StatusCancelled,StatusInProcess,StatusPending,StatusRefunded,StatusShipped
            });
    }
}
