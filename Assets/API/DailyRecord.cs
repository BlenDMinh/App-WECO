
using System;

public class DailyRecord  {
    public DateTime date;
    public int taskCount;
    public int fishCount;

    public DailyRecord() {
        date = DateTime.Today;
        taskCount = 0;
        fishCount = 0;
    }

    public DailyRecord(DateTime _date, int _taskCount, int _fishCount) {
        date = _date;
        taskCount = _taskCount;
        fishCount = _fishCount;
    }
}
