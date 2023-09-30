namespace Installment
{
    public sealed class Global
    {
        private static Global instance = null;
        private const int CompanyId = 1;
        public static Global Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Global();
                }
                return instance;
            }
        }
        public int GetCompanyId()
        {
            return CompanyId;
        }
    }
}
