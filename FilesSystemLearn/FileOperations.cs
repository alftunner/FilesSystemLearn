using System.Text;
using static System.Console;

namespace FilesSystemLearn;

public class FileOperations
{
    public string filePath;

    public FileOperations()
    {
        fileCreate();
    }
    
    public void fileCreate()
    {
        Write("Введите название файла вида \"name.txt\": ");
        string filePath = ReadLine();
        string[] fileNameParts = filePath.Split(".");
        try
        {
            if ((fileNameParts[0] != "") && (fileNameParts[1] == "txt"))
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        Console.Write($"Введите текст для записи в {filePath}: ");
                        string inputText = Console.ReadLine();
                        writer.WriteLine(inputText);
                        this.filePath = filePath;
                        Console.WriteLine($"Создан файл: {this.filePath}");
                        Console.WriteLine($"В него записан текст: {inputText}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Название файла должно быть вида \"name.txt\"");
                fileCreate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Название файла должно быть вида \"name.txt\"");
            fileCreate();
        }
    }

    public void fileRead()
    {
        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream, Encoding.Unicode))
            {
                string text = reader.ReadToEnd();
                string[] words = text.Split(" ");
                var cleanWords = from word in words where word != "" select word;
                var testCollection = from str in cleanWords
                    group str by str.Substring(0, 1).ToLower()
                    into firstChar
                    select firstChar;
                foreach (var item in testCollection)
                {
                    using (FileStream streamCreate = new FileStream($"{item.Key}.txt", FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(streamCreate, Encoding.Unicode))
                        {
                            string result = "";
                            foreach (var str in item)
                            {
                                result += str + " ";
                            }
                            writer.WriteLine(result);
                            Console.WriteLine($"Создан файл: \"{item.Key}.txt\"");
                            Console.WriteLine($"В него записан текст: {result}");
                        }
                    }
                }
            }
        }
    }
}