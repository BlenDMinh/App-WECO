
using System;

public class DailyRecord  {
    public DateTime date;
    public int taskCount;

    public DailyRecord() {
        date = DateTime.Today;
        taskCount = 0;
    }

    public DailyRecord(DateTime _date, int _taskCount) {
        date = _date;
        taskCount = _taskCount;
    }
}
