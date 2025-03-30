namespace Contract.Constants;

public static class RabbitMQConstant
{
    public static class QUEUE
    {
        public static class NAME
        {
            // ======================= Identity ============================
            public const string UPDATE_ACCOUNT_IS_ACTIVE = "update-account-is-active-queue";
            // ======================= USER ============================
            public const string USER_REGISTER_NOTIFICATION = "user-register-notification-queue";
            public const string USER_REGISTER_USER = "user-register-user-queue";
            public const string LINK_ACCOUNT = "link-account-queue";
            public const string UNLINK_ACCOUNT = "unlink-account-queue";
            public const string USER_RESEND_OTP = "user-resend-otp-queue";
            public const string UPDATE_TOTAL_RECIPE = "update-total-recipe-queue";
            
            // ======================= Recipe ============================
            public const string UPDATE_RECIPE_TAGS = "update-recipe-tags-queue";
            public const string UPDATE_RECIPE_IS_ACTIVE = "update-recipe-is-active-queue";
            public const string REQUEST_ADD_TAGS = "request-add-tags-queue";
            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-queue";
            public const string SEND_SMS = "send-sms-queue";
            public const string PUSH_NOTIFICATION = "push-notification-queue";
            public const string VALIDATE_RECIPE = "validate-recipe-queue";

            // ======================= FILE ============================
            public const string UPLOAD_MULTIPLE_IMAGE_FILE = "upload-multiple-image-file-queue";
            public const string DELETE_MULTIPLE_IMAGE_FILE = "delete-multiple-image-file-queue";
            public const string UPDATE_MULTIPLE_IMAGE_FILE = "update-multiple-image-file-queue";

            // ======================= TRACKING ============================
            public const string CREATE_USER_VIEW_RECIPE_DETAIL = "create-user-view-recipe-detail-queue";
            public const string ADD_ACTIVITY_LOG = "add-activity-log-queue";
            // ======================= NOTIFICATION ============================
            public const string NOTIFY_USER = "notify-user-queue";
            public const string CREATE_USER_SEARCH_RECIPE = "create-user-search-recipe-queue";
            public const string CREATE_USER_SEARCH_USER = "create-user-search-user-queue";

        }
    }
    public static class EXCHANGE
    {
        public static class NAME
        {
            // ======================= Identity ============================
            public const string UPDATE_ACCOUNT_IS_ACTIVE = "update-account-is-active-event";
            // ======================= USER ============================
            public const string USER_REGISTER = "user-register-event";
            public const string LINK_ACCOUNT = "link-account-event";
            public const string UNLINK_ACCOUNT = "unlink-account-event";
            public const string USER_RESEND_OTP = "user-resend-otp-event";
            public const string UPDATE_TOTAL_RECIPE = "update-total-recipe-event";
            // ======================= Recipe ============================
            public const string UPDATE_RECIPE_TAGS = "update-recipe-tags-event";
            public const string UPDATE_RECIPE_IS_ACTIVE = "update-recipe-is-active-event";
            public const string REQUEST_ADD_TAGS = "request-add-tags-event";
            // ======================= OTHER ============================
            public const string SEND_EMAIL = "send-email-event";
            public const string SEND_SMS = "send-sms-event";
            public const string PUSH_NOTIFICATION = "push-notification-event";
            public const string VALIDATE_RECIPE = "validate-recipe-event";

            // ======================= FILE ============================
            public const string UPLOAD_MULTIPLE_IMAGE_FILE = "upload-multiple-image-file-event";
            public const string DELETE_MULTIPLE_IMAGE_FILE = "delete-multiple-image-file-event";
            public const string UPDATE_MULTIPLE_IMAGE_FILE = "update-multiple-image-file-event";

            // ======================= TRACKING ============================
            public const string CREATE_USER_VIEW_RECIPE_DETAIL = "create-user-view-recipe-detail-event";
            public const string ADD_ACTIVITY_LOG = "add-activity-log-event";
            // ======================= NOTIFICATION ============================
            public const string NOTIFY_USER = "notify-user-event";
            public const string CREATE_USER_SEARCH_RECIPE = "create-user-search-recipe-event";
            public const string CREATE_USER_SEARCH_USER = "create-user-search-user-event";
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
