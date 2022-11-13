using Newtonsoft.Json;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ConsoleApp8
{
    internal class readandsave
    {
        string PATH;
        public readandsave(string path)
        {
            PATH = path;
        }

        public void readFile()
        {
            if (PATH.Contains(".txt"))
            {
                while (true)
                {
                    Console.WriteLine("Текст файла находится снизу. Для того чтобы поменять раcширение - нажмите F1, выход из программы - Escape");
                    Console.WriteLine(File.ReadAllText(PATH));
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        newExtension();
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                    Console.Clear();

                }
            }
            if (PATH.Contains(".json"))
            {
                while (true)
                {
                    Console.WriteLine("Текст файла находится снизу. Для того чтобы поменять раcширение - нажмите F1, выход из программы - Escape");
                    string text = File.ReadAllText(PATH);
                    List<Figures> result = JsonConvert.DeserializeObject<List<Figures>>(text);
                    for (int i = 0; i < result.Count; i++)
                    {
                        Console.WriteLine(result[i].name + '\n' + result[i].height + '\n' + result[i].width);
                    }
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        newExtension();
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                    Console.Clear();
                }
            }
            if (PATH.Contains(".xml"))
            {
                while (true)
                {
                    Console.WriteLine("Текст файла находится снизу. Для того чтобы поменять раcширение - нажмите F1, выход из программы - Escape");
                    XmlSerializer xml = new XmlSerializer(typeof(List<Figures>));
                    using (FileStream fs = new FileStream(PATH, FileMode.Open))
                    {
                        List<Figures> name = (List<Figures>)xml.Deserialize(fs);
                        for (int i = 0; i < name.Count; i++)
                        {
                            Console.WriteLine(name[i].name + '\n' + name[i].height + '\n' + name[i].width);
                        }
                    }
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.F1)
                    {
                        newExtension();
                        break;
                    }
                    if (key.Key == ConsoleKey.Escape) { break; }
                    Console.Clear();

                }
            }
        }
        private void newExtension()
        {
            List<Figures> list = new List<Figures>();
            Figures Triangle = new Figures();
            Triangle.name = "Triangle";
            Triangle.width = 30;
            Triangle.height = 50;
            list.Add(Triangle);
            Figures Square = new Figures();
            Square.name = "Square";
            Square.width = 30;
            Square.height = 30;
            list.Add(Square);
            Figures Circle = new Figures();
            Circle.name = "Circle";
            Circle.width = 60;
            Circle.height = 40;
            list.Add(Circle);

            Console.Write("Введите новый путь и укажите в конце новое расширение: ");
            string newpath = Console.ReadLine();
            if (File.Exists(newpath)) { File.Delete(newpath); }
            if (newpath.Contains(".json"))
            {
                string json = JsonConvert.SerializeObject(list);
                File.Create(newpath).Close();
                File.WriteAllText(newpath, json);
            }
            if (newpath.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figures>));
                using (FileStream fs = new FileStream(newpath, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, list);
                }
            }
            if (newpath.Contains(".txt"))
            {
                if (PATH.Contains(".json"))
                {
                    string text = File.ReadAllText(PATH);
                    List<Figures> result = JsonConvert.DeserializeObject<List<Figures>>(text);
                    File.WriteAllText(newpath, (result[0].name + '\n' + result[0].height + '\n' + result[0].width + '\n' + result[1].name + '\n' + result[1].height + '\n' + result[1].width + '\n' + result[2].name + '\n' + result[2].height + '\n' + result[2].width));
                }
                if (PATH.Contains(".xml"))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Figures>));
                    using (FileStream fs = new FileStream(PATH, FileMode.Open))
                    {
                        List<Figures> name = (List<Figures>)xml.Deserialize(fs);
                        File.WriteAllText(newpath, (name[0].name + '\n' + name[0].height + '\n' + name[0].width + '\n' + name[1].name + '\n' + name[1].height + '\n' + name[1].width + '\n' + name[2].name + '\n' + name[2].height + '\n' + name[2].width));
                    }
                }
            }

        }

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите полный путь до файла: ");
            string pathtofile = Console.ReadLine();
            readandsave file = new readandsave(pathtofile);
            file.readFile();


        }
    }
}
//D:\file\text.txt