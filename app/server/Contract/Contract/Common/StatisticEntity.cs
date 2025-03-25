namespace Contract.Common;

public class StatisticEntity
{
    public int Number { get; set; }
}

public class HourStatisticEntity : StatisticEntity
{
    public string Hour { get; set; } = null!;
}

public class DateStatisticEntity : StatisticEntity
{
    public string Date { get; set; } = null!;
}

public class MonthStatisticEntity : StatisticEntity
{
    public string Month { get; set; } = null!;
}
