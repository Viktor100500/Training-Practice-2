using System;
using System.IO;

namespace Training_Practice__2
{
    internal class Program
    {
        private static double ToRad(double grad) // Фукнция перевода в радианы 
        {
            return grad * Math.PI / 180;
        }

        private static void Main() // Главная точка в приложение
        {
            double radius; // радиус, от 100 до 10 000 
            double lat1, lat2, long1, long2;
            var reader = new FileInOut("INPUT");

            // инициализация
            radius = Double.Parse(reader.Next().Replace('.', ','));
            lat1 = Double.Parse(reader.Next().Replace('.', ','));
            long1 = Double.Parse(reader.Next().Replace('.', ','));
            lat2 = Double.Parse(reader.Next().Replace('.', ','));
            long2 = Double.Parse(reader.Next().Replace('.', ','));

            // перевод в радианы
            lat1 = ToRad(lat1);
            lat2 = ToRad(lat2);
            long1 = ToRad(long1);
            long2 = ToRad(long2);

            // синусы и косинусы для вычисления
            double x, y;

            // вычисления
            // формула гаверсинусов с модификацией для антиподов
            y = Math.Sqrt(Math.Pow(Math.Cos(lat2) * Math.Sin(long2 - long1), 2)
                + Math.Pow(Math.Cos(lat1) * Math.Sin(lat2)
                - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1), 2));

            x = Math.Sin(lat1) * Math.Sin(lat2)
                + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(long2 - long1);

            double ans = Math.Atan2(y, x) * radius;

            FileInOut.ToFile("OUTPUT", ans.ToString("#.##").Replace(',', '.'));
        }

        public class FileInOut // Класс работы с файлами
        {
            private string[] _obj;                      // строки, которые нужно считать
            private int _position;                   // позиция считывателя

            /* конструктор для ввода из файла
             * name - название файла
             */
            public FileInOut(string name)
            {
                var reader = new StreamReader(name + ".txt");
                var strings = reader.ReadToEnd();
                _obj = strings.Split('\n', ' ');

                reader.Close();
            }

            // возрващает считываемую строку, делит по пробелам
            public string Next()
            {
                try
                {
                    return _obj[_position++];
                }
                catch (Exception)
                {
                    return " ";
                }
            }

            /* вывод в файл name строки obj
             * name вводится без расширения
             * выбор пути не поддерживается, все в папку исходную
             */
            public static void ToFile(string name, string obj)
            {
                var f = new StreamWriter(name + ".txt");
                var strings = obj.Split('\n', '\r');

                foreach (var t in strings)
                    f.WriteLine(t);

                f.Close();
            }
        }
    }
}
