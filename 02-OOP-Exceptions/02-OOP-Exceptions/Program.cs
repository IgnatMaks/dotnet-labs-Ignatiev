abstract class Shape : IComparable<Shape>
{
    public string Name { get; protected set; }

    protected Shape(string name)
    {
        Name = name;
    }

    public abstract double GetArea();

    public abstract double GetPerimeter();

    public virtual void PrintInfo()
    {
        Console.WriteLine(
            $"{Name}: площа = {GetArea():F2}, периметр = {GetPerimeter():F2}");
    }

    public int CompareTo(Shape? other)
    {
        if (other == null)
        {
            return 1;
        }

        return GetArea().CompareTo(other.GetArea());
    }
}

class InvalidShapeSizeException : Exception
{
    public InvalidShapeSizeException()
    {
    }

    public InvalidShapeSizeException(string message)
        : base(message)
    {
    }

    public InvalidShapeSizeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

class Circle : Shape
{
    private double _radius;

    public double Radius
    {
        get => _radius;
        set
        {
            if (value <= 0)
            {
                throw new InvalidShapeSizeException(
                    "Радіус кола має бути більшим за 0.");
            }

            _radius = value;
        }
    }

    public Circle(double radius)
        : base("Коло")
    {
        Radius = radius;
    }

    public override double GetArea()
    {
        return Math.PI * Radius * Radius;
    }

    public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"{Name}: радіус = {Radius:F2}, площа = {GetArea():F2}, периметр = {GetPerimeter():F2}");
    }
}

class Rectangle : Shape
{
    private double _width;
    private double _height;

    public double Width
    {
        get => _width;
        set
        {
            if (value <= 0)
            {
                throw new InvalidShapeSizeException(
                    "Ширина прямокутника має бути більшою за 0.");
            }

            _width = value;
        }
    }

    public double Height
    {
        get => _height;
        set
        {
            if (value <= 0)
            {
                throw new InvalidShapeSizeException(
                    "Висота прямокутника має бути більшою за 0.");
            }

            _height = value;
        }
    }

    public Rectangle(double width, double height)
        : base("Прямокутник")
    {
        Width = width;
        Height = height;
    }

    public override double GetArea()
    {
        return Width * Height;
    }

    public override double GetPerimeter()
    {
        return 2 * (Width + Height);
    }

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"{Name}: ширина = {Width:F2}, висота = {Height:F2}, площа = {GetArea():F2}, периметр = {GetPerimeter():F2}");
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        List<Shape> shapes = new List<Shape>();

        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== Геометричні фігури ===");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("1. Додати коло");
            Console.WriteLine("2. Додати прямокутник");
            Console.WriteLine("3. Показати всі фігури");
            Console.WriteLine("4. Відсортувати фігури за площею");
            Console.WriteLine("5. Показати сумарну площу");
            Console.WriteLine("6. Показати сумарний периметр");

            int choice = ReadInt("\nВаш вибір: ", 0, 6);

            switch (choice)
            {
                case 0:
                    isRunning = false;
                    break;

                case 1:
                    AddCircle(shapes);
                    break;

                case 2:
                    AddRectangle(shapes);
                    break;

                case 3:
                    ShowShapes(shapes);
                    break;

                case 4:
                    SortShapes(shapes);
                    break;

                case 5:
                    ShowTotalArea(shapes);
                    break;

                case 6:
                    ShowTotalPerimeter(shapes);
                    break;
            }
        }

        Console.WriteLine("Роботу завершено.");
    }

    static void AddCircle(List<Shape> shapes)
    {
        try
        {
            double radius = ReadDouble("Введіть радіус кола: ");

            Shape circle = new Circle(radius);
            shapes.Add(circle);

            Console.WriteLine("Коло успішно додано.");
        }
        catch (InvalidShapeSizeException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }
    }

    static void AddRectangle(List<Shape> shapes)
    {
        try
        {
            double width = ReadDouble("Введіть ширину прямокутника: ");
            double height = ReadDouble("Введіть висоту прямокутника: ");

            Shape rectangle = new Rectangle(width, height);
            shapes.Add(rectangle);

            Console.WriteLine("Прямокутник успішно додано.");
        }
        catch (InvalidShapeSizeException ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }
    }

    static void ShowShapes(List<Shape> shapes)
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("Список фігур порожній.");
            return;
        }

        Console.WriteLine("\nСписок фігур:");

        for (int i = 0; i < shapes.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            shapes[i].PrintInfo();
        }
    }

    static void SortShapes(List<Shape> shapes)
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("Список фігур порожній.");
            return;
        }

        shapes.Sort();

        Console.WriteLine("Фігури відсортовано за площею.");
    }

    static void ShowTotalArea(List<Shape> shapes)
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("Список фігур порожній.");
            return;
        }

        double totalArea = 0;

        foreach (Shape shape in shapes)
        {
            totalArea += shape.GetArea();
        }

        Console.WriteLine($"Сумарна площа: {totalArea:F2}");
    }

    static void ShowTotalPerimeter(List<Shape> shapes)
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("Список фігур порожній.");
            return;
        }

        double totalPerimeter = 0;

        foreach (Shape shape in shapes)
        {
            totalPerimeter += shape.GetPerimeter();
        }

        Console.WriteLine($"Сумарний периметр: {totalPerimeter:F2}");
    }

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int value) &&
                value >= min &&
                value <= max)
            {
                return value;
            }

            Console.WriteLine(
                $"Некоректне значення. Введіть ціле число від {min} до {max}.");
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double value))
            {
                return value;
            }

            Console.WriteLine(
                "Некоректне значення. Введіть число.");
        }
    }
}