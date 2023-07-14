public class Task
{
    private const int MIN_FOOD_AMT = 1;
    private const int MAX_FOOD_AMT = 5;

    public FoodTypes FoodToCollect { private set; get; }
    public int FoodAmt { private set; get; }
    private static Task s_instance;
    private Task() { }

    public static Task Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new Task();
                s_instance.GenerateTask();
            }
            return s_instance;
        }
    }

    public override string ToString()
    {
        string result = $"Collect {FoodAmt} {FoodToCollect}";
        if (FoodAmt > 1)
            result += "s";
        return result;
    }

    private void GenerateTask()
    {
        FoodToCollect = (FoodTypes)RandomFromEnumFinder.GetRandomFromEnum<FoodTypes>();
        System.Random rnd = new();
        FoodAmt = rnd.Next(MIN_FOOD_AMT, MAX_FOOD_AMT + 1);
    }
}
