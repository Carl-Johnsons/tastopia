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
            // ======================= Recipe ============================
            public const string GET_RECIPE_DETAILS = "get-recipe-details-queue";
            public const string UPDATE_RECIPE_TAGS = "update-recipe-tags-queue";
            public const string REQUEST_ADD_TAGS = "request-add-tags-queue";
            public const string GET_ALL_TAGS = "get-all-tags-queue";



            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-queue";
            public const string VALIDATE_RECIPE = "validate-recipe-queue";

            // ======================= FILE ============================
            public const string UPLOAD_MULTIPLE_IMAGE_FILE = "upload-multiple-image-file-queue";
            public const string DELETE_MULTIPLE_IMAGE_FILE = "delete-multiple-image-file-queue";
            public const string UPDATE_MULTIPLE_IMAGE_FILE = "update-multiple-image-file-queue";






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
            // ======================= Recipe ============================
            public const string GET_RECIPE_DETAILS = "get-recipe-details-event";
            public const string GET_ALL_TAGS = "get-all-tags-event";
            public const string UPDATE_RECIPE_TAGS = "update-recipe-tags-event";
            public const string REQUEST_ADD_TAGS = "request-add-tags-event";

            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-event";
            public const string VALIDATE_RECIPE = "validate-recipe-event";

            // ======================= FILE ============================
            public const string UPLOAD_MULTIPLE_IMAGE_FILE = "upload-multiple-image-file-event";
            public const string DELETE_MULTIPLE_IMAGE_FILE = "delete-multiple-image-file-event";
            public const string UPDATE_MULTIPLE_IMAGE_FILE = "update-multiple-image-file-event";

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
