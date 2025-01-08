namespace SubscriptionService.Domain.Constants;

public enum PaymentMethod
{
    GOOGLE_PLAY,
    APP_STORE
}

public enum PaymentStatus
{
    PENDING,
    CANCELED,
    SUCCESS
}
