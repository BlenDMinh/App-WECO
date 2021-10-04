
using System;

/// <summary>
/// DailyRecord: Là thông tin bao gồm về ngày làm task (ngày làm của tất cả
/// các task), số task làm được, số cá cứu được 
/// </summary>

public class DailyRecord  {
    public DateTime date;
    public int taskCount;
    public int fishCount;
    ///
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
