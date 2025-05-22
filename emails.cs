using System.Text.RegularExpressions;
internal class Program
{
    private static int mail(string namefile)
    { 
        string str = File.ReadAllText(namefile);
        string pattern = @"[a-zA-Z0-9._-]+@mail+\.ru";
        return Regex.Matches(str, pattern).Count();
    }
    private static void Main(string[] args)
    {
        int mails = mail(@"C:\Users\РПО1124\source\repos\pochta\pochta\emails.txt");
        
            Console.WriteLine(mails+24);
        
    }
}
