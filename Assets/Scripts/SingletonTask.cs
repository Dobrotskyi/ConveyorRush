using System;

public class SingletonTask
{
    private const int MIN_FOOD_AMT = 1;
    private const int MAX_FOOD_AMT = 5;

    public FoodTypes FoodToCollect { private set; get; }
    public event Action TaskCompleted;
    public event Action FoodAmtUpdated;

    public int FoodAmt { private set; get; }

    private static SingletonTask s_instance;
    private SingletonTask() { }

    public static SingletonTask Instance
    {
        get
        {
            if (s_instance == null)
            {
                s_instance = new SingletonTask();
                s_instance.GenerateTask();
                FoodBucket.ItemStored += s_instance.OnItemStored;
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

    public void OnItemStored()
    {
        if (FoodAmt > 0)
        {
            FoodAmt--;
            FoodAmtUpdated?.Invoke();
        }
        if (FoodAmt == 0)
        {
            TaskCompleted?.Invoke();
            GenerateTask();
        }
    }

    private void GenerateTask()
    {
        FoodToCollect = (FoodTypes)RandomFromEnumFinder.GetRandomFromEnum<FoodTypes>();
        Random rnd = new();
        FoodAmt = rnd.Next(MIN_FOOD_AMT, MAX_FOOD_AMT + 1);
    }
}
