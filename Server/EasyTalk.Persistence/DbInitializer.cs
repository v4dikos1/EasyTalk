namespace EasyTalk.Persistence
{
    public static class DbInitializer
    {
        public static void Initialize(EasyTalkDbContext easyTalkDbContext)
        {
            easyTalkDbContext.Database.EnsureCreated();
        }
    }
}
