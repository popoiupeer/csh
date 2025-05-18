using System;
class Program
{
    public static string DecodeMessage(string encoded)
    {
        char[] result = new char[encoded.Length];

        for (int i = 0; i < encoded.Length; i++)
        {
            char c = encoded[i];
            if (c == ' ')
            {
                result[i] = ' ';
            }
            else
            {
                result[i] = (char)('a' + 'z' - c);
            }
        }

        return new string (result);
    }
    public static void Main()
    {
        string encoded = "r slkv mlylwb wvxlwvh gsrh nvhhztv";
        string decoded = DecodeMessage(encoded);
        Console.WriteLine(decoded); 
    }

}
