using System;

namespace D2Bot;

public class Period
{
	public int Hour;

	public int Minute;

	public Period(int hour, int minute)
	{
		Hour = hour;
		Minute = minute;
	}

	public TimeSpan GetPeriod()
	{
		return new TimeSpan(Hour, Minute, 0);
	}

	public Period DeepCopy()
	{
		return (Period)MemberwiseClone();
	}
}
