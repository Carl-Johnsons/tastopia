namespace Contract.Constants;

public static class GrpcUploadFileConfig
{
    public static readonly int MaxMessageSize = 15 * 3 * 20 * 1024 * 1024; //1800mb => max 10bm per image
}
