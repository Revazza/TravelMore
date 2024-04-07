namespace TravelMore.Domain.Subscriptions.Memberships.Enums;

[Flags]
public enum MembershipType
{
    None,
    CanBook,
    FreeOfBookingCharge,
    FreeBreakfast,
    LateCheckout,
    PrioritySupport,
    Standard = CanBook,
    Premium = Standard | FreeOfBookingCharge | FreeBreakfast | LateCheckout | PrioritySupport,
}
