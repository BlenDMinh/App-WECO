
using System;
using System.Collections.Generic;

public class DailyRecord  {
    public System.DateTime date;
    public int taskCount;

    public DailyRecord() {
        date = System.DateTime.Today;
        taskCount = 0;
    }
}
