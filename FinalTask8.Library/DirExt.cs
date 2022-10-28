namespace FinalTask8.Library
{
    public static class DirExt
    {
        public static DirectoryInfo Dir { get; set; }

        /// <summary>
        /// Удаление содержимого каталога
        /// </summary>
        public static void DelDir()
        {
            if (Directory.Exists(Dir.FullName))
            {
                string[] dirs = Directory.GetDirectories(Dir.FullName);
                foreach (var dir in dirs)
                {
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"каталог {dir} доступен только для чтения или используется");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"у вас отсутствует доступ к каталогу {dir}");
                    }

                }
                string[] files = Directory.GetFiles(Dir.FullName);
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (IOException)
                    {
                        Console.WriteLine($"файл {file} доступен только для чтения или используется");
                    }
                    catch (UnauthorizedAccessException)
                    {
                        Console.WriteLine($"у вас отсутствует доступ к файлу {file}");
                    }
                }
            }
            else
            {
                Console.WriteLine("наименование каталога указано неверно");
            }
        }


        /// <summary>
        /// Подсчет размера каталога
        /// </summary>
        /// <param name="dir"> путь к каталогу</param>
        /// <returns></returns>
        public static (long size, long count) DirSize(DirectoryInfo dir, bool islog)
        {
            long size = 0;
            long count = 0;

            if (!dir.Exists && islog) Console.WriteLine($"каталог {dir} отсутствует");

            FileInfo[] files = dir.GetFiles();
            foreach (var file in files)
            {

                try
                {
                    size += file.Length;
                    count++;
                }
                catch (IOException)
                {
                    if (islog) Console.WriteLine($"файл {file} доступен только для чтения или используется");
                }
                catch (UnauthorizedAccessException)
                {
                    if (islog) Console.WriteLine($"у вас отсутствует доступ к файлу {file}");
                }

                if (islog) Console.WriteLine($"{file.Name} - {ViewSize(file.Length)}");
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (var d in dirs)
            {
                size += DirSize(d, islog).size;
                count += DirSize(d, islog).count;
            }

            return (size, count);
        }

        /// <summary>
        /// Вывод размера с размерностью
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string ViewSize(long size)
        {
            return size switch
            {
                < 4000 => string.Concat(size.ToString(), " байт"),
                >= 4000 and < 4000000 => string.Concat(((double)size / 1024).ToString("0.00"), " Кбайт"),
                >= 4000000 and < 4000000000 => string.Concat(((double)size / 1048576).ToString("0.00"), " Мбайт"),
                >= 4000000000 => string.Concat(((double)size / 1073741824).ToString("0.00"), " Гбайт")
            };
        }
    }
}
