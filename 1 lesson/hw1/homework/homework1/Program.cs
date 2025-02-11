

class Bank
{
    private int money;
    private string name;
    private int percent;

    public int Money
    {
        get => money;
        set
        {
            money = value;
            new Thread(LogToFile).Start();
        }
    }
    public string Name
    {
        get => name;
        set
        {
            name = value;
            new Thread(LogToFile).Start();
        }
    }
    public int Percent
    {
        get => percent;
        set
        {
            percent = value;
            new Thread(LogToFile).Start();
        }
    }

    public Bank(string name, int money, int percent)
    {
        this.name = name;
        this.money = money;
        this.percent = percent;

        new Thread(LogToFile).Start();
    }

    private void LogToFile()
    {
        string filePath = "bank_log.txt";
        string logEntry = $"{DateTime.Now}: Name={Name}, Money={Money}, Percent={Percent}";

        lock (this)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(logEntry);
            }
        }
    }
}

class Program {
    static void Main() {
        Bank bank = new Bank("PrivatBank", 2500, 3);

        bank.Money = 2000;
        bank.Name = "Oshad";
        bank.Percent = 5;

        Console.WriteLine("Данные записаны в файл.");
    }
}