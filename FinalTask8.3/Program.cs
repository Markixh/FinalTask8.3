using FinalTask8.Library;

namespace FinalTask8_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Directory.Exists(args[0]))
            {
                DirExt.Dir = new(args[0]);

                (long size, long count) = DirExt.DirSize(DirExt.Dir, false); //подсчет размера каталогу
                Console.WriteLine($"Исходный размер каталога {DirExt.Dir.Name}: {DirExt.ViewSize(size)}");
                DirExt.DelDir(); //удаление содержимого каталога
                size -= DirExt.DirSize(DirExt.Dir, false).size; //расчет размера удаленных файлов
                count -= DirExt.DirSize(DirExt.Dir, false).count; //подсчет количества удаленных файлов 
                Console.WriteLine($"освобождено {count} файлов: {DirExt.ViewSize(size)}");
                size = DirExt.DirSize(DirExt.Dir, false).size; //новый размер каталога
                Console.WriteLine($"Размер каталога: {DirExt.ViewSize(size)}");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("наименование каталога указано неверно");
            }
        }
    }
}