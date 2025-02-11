class Program {

    static void ProcessCollection(object? collection)
    {
        if (collection is IEnumerable<object> items)
        {
            for (int i = 0; i < items.Count(); i++)
            {
                Console.WriteLine(items.ElementAt(i).ToString());
            }
        }
    }

    static void Main()
    {

        List<string> items = new List<string> {
            "message 1", "message 2", "message 3", "message 4"
        };


        Thread thread1 = new Thread(new ParameterizedThreadStart(ProcessCollection));
        thread1.Start(items);
    }
}








