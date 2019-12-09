using System.Collections.Generic;

namespace ACO.Attempt2
{
    public class Algorithm
    {
        private int maxIterations = 100;
        private int currentIterations = 0;
        
        private JobGraph _pheromoneGraph;
        private List<Job> _allJobs = new List<Job>
        {
            new Job(2, 5, 1),
            new Job(4, 7, 6),
            new Job(1, 11, 2),
            new Job(3, 9, 3),
            new Job(3, 8, 2)
        };
        
        private List<Ant> _allAnts = new List<Ant>();

        public Ant Execute( int antSize, float desirabilityInfluence, float pheromoneInfluence, float greedyInfluence)
        {
            _pheromoneGraph = new JobGraph(_allJobs.Count);

            for (int index = 0; index < _allJobs.Count; index++)
            {
                _allAnts.Add( new Ant( ref _allJobs, _allJobs[index], ref _pheromoneGraph));
            }

            // Assign an ant as best, as per the algorithm
            Ant bestAnt = _allAnts[0];
            float currentBestFitness = 0;
            
            while (!StopCondition())
            {
                _allAnts.ForEach(ant =>
                {
                    ant.TravelAllNodes();
                    float antFitness = CalculateTotalWeightedTardiness(ant);

                    if (antFitness > currentBestFitness)
                    {
                        currentBestFitness = antFitness;
                        bestAnt = ant;
                    }
                    
                    //Locally Update And Decay Pheromone
                    _pheromoneGraph.SetPheromoneBetweenTwoJobs();
                    
                });
            }
            

        }

        private bool StopCondition()
        {
            currentIterations++;
            return currentIterations <= maxIterations;
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