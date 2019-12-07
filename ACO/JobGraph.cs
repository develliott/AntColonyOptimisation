using System;
using System.Collections.Generic;

namespace ACO
{
    public class JobGraph
    {
        public double[][] _pheromoneData;
        public JobGraph(int jobCount)
        {
            _pheromoneData = new double[jobCount][];
            
            InitPheromoneLevel(jobCount);
        }

        private void InitPheromoneLevel(int jobCount)
        {
            for (var i = 0; i < _pheromoneData.Length; i++)
            {
                // The number of Jobs that can be transitioned to from a job
                // is Total Number of Jobs - 1. To exclude itself from the list of available jobs.
                _pheromoneData[i] = new double[jobCount - 1];
            }
        }

        public void SetPheromoneBetweenTwoJobs(int currentJobIndex, int nextJobIndex, double pheromoneLevel)
        {
            if (currentJobIndex == nextJobIndex)
            {
                throw new Exception("You cannot set a pheromone amount To and From the same node.");
            }
            _pheromoneData[currentJobIndex][nextJobIndex] = pheromoneLevel;
        }
        
        public double GetPheromoneBetweenTwoJobs(int currentJobIndex, int nextJobIndex)
        {
            if (currentJobIndex == nextJobIndex)
            {
                throw new Exception("You cannot get a pheromone amount To and From the same node.");
            }
            
            return _pheromoneData[currentJobIndex][nextJobIndex];
        }
    }
}