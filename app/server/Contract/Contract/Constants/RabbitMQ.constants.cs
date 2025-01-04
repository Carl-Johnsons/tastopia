namespace Contract.Constants;

public static class RabbitMQConstant
{
    public static class QUEUE
    {
        public static class NAME
        {
            // ======================= USER ============================
            public const string USER_REGISTER_NOTIFICATION = "user-register-notification-queue";
            public const string USER_REGISTER_USER = "user-register-user-queue";
            public const string LINK_ACCOUNT = "link-account-queue";
            public const string USER_RESEND_OTP = "user-resend-otp-queue";
            public const string GET_SIMPLE_USERS = "get-simple-users-queue";
            public const string GET_USER_DETAILS = "get-user-details-queue";
            // ======================= ACCOUNT ============================
            public const string GET_ACCOUNT_DETAILS = "get-account-details-queue";


            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-queue";
        }
    }
    public static class EXCHANGE
    {
        public static class NAME
        {
            // ======================= USER ============================
            public const string USER_REGISTER = "user-register-event";
            public const string LINK_ACCOUNT = "link-account-event";
            public const string USER_RESEND_OTP = "user-resend-otp-event";
            public const string GET_SIMPLE_USERS = "get-simple-users-event";
            public const string GET_USER_DETAILS = "get-user-details-event";
            // ======================= Account ============================
            public const string GET_ACCOUNT_DETAILS = "get-account-details-event";


            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-event";
        }

        public static class TYPE
        {
            /// <summary>
            /// Exchange type used for AMQP direct exchanges.
            /// </summary>
            public const string Direct = "direct";

            /// <summary>
            /// Exchange type used for AMQP fanout exchanges.
            /// </summary>
            public const string Fanout = "fanout";

            /// <summary>
            /// Exchange type used for AMQP headers exchanges.
            /// </summary>
            public const string Headers = "headers";

            /// <summary>
            /// Exchange type used for AMQP topic exchanges.
            /// </summary>
            public const string Topic = "topic";

            private static readonly string[] s_all = { Fanout, Direct, Topic, Headers };

            /// <summary>
            /// Retrieve a collection containing all standard exchange types.
            /// </summary>
            public static ICollection<string> All()
            {
                return s_all;
            }
        }
    }
}
