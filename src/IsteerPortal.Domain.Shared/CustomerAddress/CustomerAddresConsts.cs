namespace IsteerPortal.CustomerAddress
{
    public static class CustomerAddresConsts
    {
        private const string DefaultSorting = "{0}Address1 asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "CustomerAddres." : string.Empty);
        }

    }
}