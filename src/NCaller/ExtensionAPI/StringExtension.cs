namespace NCaller.ExtensionAPI
{
    public static class StringExtension
    {
        public unsafe static long GetLong(this string value,int index)
        {
            fixed (char* c = value)
            {
                return *(long*)(c+index*4);
            }
        }
    }
}
