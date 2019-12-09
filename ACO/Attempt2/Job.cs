using System;

namespace ACO.Attempt2
{
    public class Job
    {
        public int RequiredTimeToComplete { get; }
        public int DaysTillDue { get; }
        public int DelayPenalty { get; }
        
        public int Tardiness { get; }

        public int JobIndex { get; }
        private static int Count = 0;

        public Job(int requiredTimeToComplete, int daysTillDue, int delayPenalty)
        {
            JobIndex = Count;
            Count++; 
            
            RequiredTimeToComplete = requiredTimeToComplete;
            DaysTillDue = daysTillDue;
            DelayPenalty = delayPenalty;
        }

        public int CalculateTardiness(int jobStartTime)
        {
            return Math.Max( jobStartTime + RequiredTimeToComplete - DaysTillDue, 0 );
        }
    }
}