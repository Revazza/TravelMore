namespace TravelMore.Domain.Discounts.ValueObjects;

public record CouponCode
{
    public string Code { get; init; }

    private CouponCode(string code)
    {
        Code = code;
    }

    public static CouponCode Create() => new(Guid.NewGuid().ToString());
    public static CouponCode Create(string code) => new(code);

}
