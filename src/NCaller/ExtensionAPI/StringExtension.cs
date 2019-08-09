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




        public unsafe static int GetInt(this string value, int index)
        {
            fixed (char* c = value)
            {
                return *(int*)(c + index * 2);
            }
        }

    }
}
