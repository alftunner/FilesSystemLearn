namespace FilesSystemLearn;

public static class Debug
{
    public static void debugMyStringArray(string[] array, bool count = false)
    {
        foreach (var item in array)
        {
            Console.WriteLine(!count ? item : item.Length);
        }
    }
}