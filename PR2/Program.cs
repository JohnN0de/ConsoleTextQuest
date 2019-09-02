using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace PR2
{
    class Program
    {
        static string[] NasaOpening = new string[] { "Nasa 2019 'Clear Horizont!'", "Войдите в аккаунт!", "Логин: Хьюстон", "Пароль: " };
        static string[] lineHistory = new string[] { "Алло, Хьюстон, меня слышно?", "Насчет обстановки... Давай я тебе фотографию отправлю?", "Отправляю фотографию...", "Как тебе?", "Доусена и Джеймса поглотило ", "Я никак не мог им помочь!!!", "Скоро ОНО и меня поглотит..." };
        static string[,] Answer = new string[5, 2] { { "Слышно отлично, связь стабильна.", "Доложи обстановку!" }, {"Да, команде не сложно будеть сфотографироваться?", "Да, сфотографируйся вместе с командой." }, {"Вышло не плохо, а где остальная часть команды?", "Я же тебе велел сфотографироваться с командой!" }, {"ЧТО?!", "Сержант докладывай, это приказ!" }, {"Сержант, возьми себя в руки!", "Сержант, под трибунал захотел?!" } };
        static string[] DLCHistory = new string[] {"Ха, не зря я настраивал передатчик!", "Команда...", "...", "...", "Взять себя в руки?!"};
        static byte[] chooseNum = new byte[] {1, 2, 4, 5, 6 }; // инициализация массива индексов вопросов, на которые есть варианты ответа
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 6); // Установка размера окна
            Console.CursorVisible = false; // Скрытие курсора
            Console.Title = "СМ #2, Исмоилов Ш. А. И-4-16"; // Установка названия окна
            Console.BackgroundColor = ConsoleColor.DarkBlue; // Установка темно-синего фона
            Console.ForegroundColor = ConsoleColor.Yellow; // Установка желтого текста
            Console.Clear(); // Очищение консоли, для изменения фона
            WriteText("*", NasaOpening); // Окно приветсвия
            Console.ForegroundColor = ConsoleColor.Green; // Установка зеленого цвета текста
            Console.BackgroundColor = ConsoleColor.Black; // Установка черного фона
            Console.SetWindowSize(Console.LargestWindowWidth / 2, Console.LargestWindowHeight); // Изменение размера окна
            Console.Clear(); // Очищение консоли, для изменения фона
            byte Tic = 0; // Глобальный счетчик индексов
            byte ansTic = 0; // Счетчик индексов ответов
            foreach (char cr in "Поиск сигнала") WriteWait(cr, 1, 10); // Вывод на экран сообщения
            WriteWait('.', 3, 1000); // Вывод на экран сообщения с ожиданием в секунду
            foreach (char cr in "\nСигнал найден, вывожу на экран") WriteWait(cr, 1, 10); // Вывод на экран сообщения
            WriteWait('.', 3, 1000); // Вывод на экран сообщения с ожиданием в секунду
            foreach (string text in lineHistory) // Цикл выводов сообщений
            {
                string namedText = "\n" + DateTime.Now.ToString() + " Ричард: " + text; // Преобразование сообщений в тру-вид
                foreach (char cr in namedText) WriteWait(cr, 1, 50); // Вывод на экран сообщения
                if(Tic == 4) // Действия на четвертое сообщение
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Установка красного цвета текста
                    foreach (char cr in "ОНО") WriteWait(cr, 1, 50); // Цикл вывода сообщения
                    Console.ForegroundColor = ConsoleColor.Green; // Установка зеленого цвета текста
                }
                Thread.Sleep(500); // Ожидание в 0.5 сек
                if (Array.IndexOf(chooseNum, ++Tic) != -1) // Действие при условии, что на данное сообщение есть ответ
                    if (WriteChoose(" --> ", new string[] { Answer[ansTic, 0], Answer[ansTic++, 1] }, 
                    new Point(0, Console.CursorTop + 1)) == 0) // Вывод на консоль выбора и действие по возвращаемому индексу
                    {
                        string namedTextDLC = "\n" + DateTime.Now.ToString() + " Ричард: " + DLCHistory[ansTic - 1]; // Преобразование сообщения
                        foreach (char cr in namedTextDLC) WriteWait(cr, 1, 50); // Вывод сообщения на экран
                        Thread.Sleep(500); // Ожидание в 0.5 сек
                    }
                switch (Tic) // Действия относительно значения 
                {
                    case 3: // Действие при третьем сообщении
                        foreach (char cr in "\nВходящий файл, расшифровываю") WriteWait(cr, 1, 10); // Вывод сообщения
                        WriteWait('.', 3, 1000); // Вывод сообщения с задержкой в 1 секунду
                        Point cur = new Point(0, Console.CursorTop + 1); // Инициализация позиции курсора
                        Console.SetCursorPosition(0, Console.CursorTop + 16); // перещение курсора 
                        drawPic(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Arrived.jpg"), cur); Thread.Sleep(500); // Вывод изображения
                        break; 
                    
                }
                
            }
            Console.ForegroundColor = ConsoleColor.DarkRed; // Установка темно-красного цвета текста
            foreach (char ch in "\nСигнал прерван!") WriteWait(ch, 1, 30); // Вывод сообщения
            Thread.Sleep(500); // Ожидание в 0.5 сек
            Console.ForegroundColor = ConsoleColor.Green; // Установка темно-красного цвета текста
            foreach (char ch in "\nПоиск сигнала") WriteWait(ch, 1, 30); // Вывод сообщения
            WriteWait('.', 3, 1000); // Вывод сообщения
            Console.ForegroundColor = ConsoleColor.Red; // Установка красного цвета текста
            Thread.Sleep(500); // Ожидание в 0.5 сек
            foreach (char ch in "\nСигнал не найден!") WriteWait(ch, 1, 30); // Вывод сообщения
            Console.ReadKey(); // Ожидание нажатия кнопки
        }

        /// <summary>
        /// Вывод текста на консоль, обводя его символами.
        /// </summary>
        /// <param name="_char">Символ для обводки текста.</param>
        /// <param name="texts">Коллекция текста для вывода на консоль.</param>
        static void WriteText(string _char, string[] texts)
        {
            for (int i = 0; i < Console.WindowWidth / _char.Length + 1; i++)
            {
                Thread.Sleep(10);
                Console.Write(_char); /// Вывод верхней границы
            }
            foreach (string text in texts) /// Цикл foreach, пробегающий по всем элементам коллекции текста.
            {
                for (int i = _char.Length; i < Console.WindowWidth - (text.Length + _char.Length - 1); i++)
                { /// Цикл, пробегающий до половины строки консоли.
                    Thread.Sleep(10);
                    if (i == (Console.WindowWidth - text.Length - _char.Length) / 2)
                    {
                        foreach (char cr in text) WriteWait(cr, 1, 10);
                        if (text == "Пароль: ")
                        break;
                    }
                    else Console.Write(" ");
                }
            }
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                Console.Write('*');
            }
            Console.SetCursorPosition((Console.WindowWidth - 20) / 2, Console.CursorTop + 1);
            Thread.Sleep(30);
            foreach (char cr in "Проверка пароля") WriteWait(cr, 1, 10);
            WriteWait('.', 3, 1000);           
            Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop + 1);
            foreach (char cr in "Идентификация пройдена. Ожидайте перенаправления!") WriteWait(cr, 1, 10);
            Console.WriteLine();
            for (int i = 0; i <= Console.WindowWidth / _char.Length - 1; i++)
            {
                Thread.Sleep(20);
                Console.Write(_char); /// Вывод нижней границы
            }
        }

        static void WriteWait(char cr, byte count, int sleepTime)
        {
            for(byte c = 0; c < count; c++)
            {
                Thread.Sleep(sleepTime);
                Console.Write(cr);
            }
        }

        /// <summary>
        /// Метод, позволяющие выводить на экран консоли выбор.
        /// </summary>
        /// <param name="_char">Символы на подобии стрелочки для ориентации по индексам выбора.</param>
        /// <param name="texts">Коллекция текста с разными вариантами.</param>
        /// <param name="indx">Стандартный индекс на котором стрелочка будет стоят, при отсутствии значение, принимает индекс 0</param>
        /// <param name="curPos">Экземпляр класса, хранящий координаты курсора консоли.</param>
        /// <returns>Возвращает индекс выбранной строки.</returns>
        static byte WriteChoose(string _char, string[] texts, Point curPos, byte indx = 0)
        {
            Console.SetCursorPosition(curPos.X, curPos.Y); /// Выставление курсора на начальную позицию.
            for (int i = 0; i < Console.WindowHeight * Console.WindowHeight - curPos.X * curPos.Y; i++) Console.Write(" ");
            string _charVoid = String.Empty; /// Инициализация пустой строки, равной длине символьной строки стрелочки.
            for (byte i = 0; i < _char.Length; i++) _charVoid += " "; /// Присвоение пустой строке пробелов длиной равной длине строки стрелочки.

            while (true) /// Бесконечный цикл, с возможностью дальнейшего выхода.
            {
                Console.SetCursorPosition(curPos.X, curPos.Y); /// Выставление курсора на начальную позицию.
                for (byte i = 0; i < texts.Length; i++) Console.WriteLine((indx == i ? _char : _charVoid) + texts[i]); /// Вывод текста на консоль вместе с отступами и стрелочкой.
                switch (Console.ReadKey().Key) /// Проверка на введенную клавишу.
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        indx = (byte)(--indx % texts.Length); /// Уменьшение индекса выбора при нажатии "W" или стрелочки вверх.
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        indx = (byte)(++indx % texts.Length); /// Увеличение индекса выбора при нажатии "W" или стрелочки вверх.
                        break;
                    case ConsoleKey.Enter: return indx; /// Выход из метода с возвращением индекса при нажатии "Enter".
                }
            }

        }

        private static void drawPic(string path, Point location)
        {

            Size imageSize = new Size(25, 15); // desired image size in characters


            using (Graphics g = Graphics.FromHwnd(GetConsoleWindow()))
            {
                using (Image image = Image.FromFile(path))
                {
                    Size fontSize = GetConsoleFontSize();

                    // translating the character positions to pixels
                    Rectangle imageRect = new Rectangle(
                        location.X * fontSize.Width,
                        location.Y * fontSize.Height,
                        imageSize.Width * fontSize.Width,
                        imageSize.Height * fontSize.Height);
                    g.DrawImage(image, imageRect);
                }
            }
        }



        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(
            string lpFileName,
            int dwDesiredAccess,
            int dwShareMode,
            IntPtr lpSecurityAttributes,
            int dwCreationDisposition,
            int dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetCurrentConsoleFont(
            IntPtr hConsoleOutput,
            bool bMaximumWindow,
            [Out][MarshalAs(UnmanagedType.LPStruct)]ConsoleFontInfo lpConsoleCurrentFont);

        [StructLayout(LayoutKind.Sequential)]
        internal class ConsoleFontInfo
        {
            internal int nFont;
            internal Coord dwFontSize;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct Coord
        {
            [FieldOffset(0)]
            internal short X;
            [FieldOffset(2)]
            internal short Y;
        }

        private const int GENERIC_READ = unchecked((int)0x80000000);
        private const int GENERIC_WRITE = 0x40000000;
        private const int FILE_SHARE_READ = 1;
        private const int FILE_SHARE_WRITE = 2;
        private const int INVALID_HANDLE_VALUE = -1;
        private const int OPEN_EXISTING = 3;
        private static Size GetConsoleFontSize()
        {
            // getting the console out buffer handle
            IntPtr outHandle = CreateFile("CONOUT$", GENERIC_READ | GENERIC_WRITE,
                FILE_SHARE_READ | FILE_SHARE_WRITE,
                IntPtr.Zero,
                OPEN_EXISTING,
                0,
                IntPtr.Zero);
            int errorCode = Marshal.GetLastWin32Error();
            if (outHandle.ToInt32() == INVALID_HANDLE_VALUE)
            {
                throw new IOException("Unable to open CONOUT$", errorCode);
            }

            ConsoleFontInfo cfi = new ConsoleFontInfo();
            if (!GetCurrentConsoleFont(outHandle, false, cfi))
            {
                throw new InvalidOperationException("Unable to get font information.");
            }

            return new Size(cfi.dwFontSize.X, cfi.dwFontSize.Y);
        }

    }
    }
