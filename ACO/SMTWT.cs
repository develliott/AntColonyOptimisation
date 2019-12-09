using System.Collections.Generic;

namespace ACO
{
    public class SMTWT
    {
        private List<Job> _allJobs = new List<Job>
        {
            new Job(2, 5, 1),
            new Job(4, 7, 6),
            new Job(1, 11, 2),
            new Job(3, 9, 3),
            new Job(3, 8, 2)
        };
        private JobGraph _jobGraph;

        private List<Ant> _allAnts = new List<Ant>();
        
        public SMTWT()
        { 
            _jobGraph = new JobGraph(_allJobs.Count);
        }

        public void ExecuteAlgorithm()
        {
            int currentIterations = 0;
            int maxIterations = 100;

            while (currentIterations < maxIterations)
            {

                // Assign each ant a reference of the jobs list and a unique starting job
                for (int i = 0; i < _allJobs.Count; i++)
                {
                    _allAnts.Add( new Ant(ref _allJobs, _allJobs[i], ref _jobGraph));
                }

                foreach (Ant ant in _allAnts)
                {
                    ant.TravelAllNodes();
                }
                
                // TODO: Deposit pheromone 
                currentIterations++;

            }
        }
        
        private float CalculateTotalWeightedTardiness(Ant ant)
        {
            float totalWeightedTardiness = 0;
            int timeElapsed = 0;
            
            foreach (Job job in ant._allJobs)
            {
                totalWeightedTardiness += job.DelayPenalty * job.CalculateTardiness(timeElapsed);
                timeElapsed += job.RequiredTimeToComplete;
            }

            return totalWeightedTardiness;
        }
    }
}