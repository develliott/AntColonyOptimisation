using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace ACO
{
    public class Ant
    {
        private readonly List<Job> _tabuJobs = new List<Job>();
        public readonly List<Job> _allJobs;
        private int _timeEslasped = 0;
        private Job _currentJob;
        private readonly JobGraph _jobGraph;

        public Ant(ref List<Job> allJobs, Job startingJob, ref JobGraph jobGraph)
        {
            _allJobs = allJobs;
            _currentJob = startingJob;
            _jobGraph = jobGraph;
        }

        public void TravelAllNodes()
        {
            while (_tabuJobs.Count <= _allJobs.Count)
            {
                // Perform Job and add prevent it from being selected again.
                _timeEslasped += _currentJob.RequiredTimeToComplete;
                _tabuJobs.Add(_currentJob);

                List<Job> allowedJobs = GetAllowedJobs();
                
                double highestProbability = 0;
                Job nextJob = null;
                allowedJobs.ForEach(job =>
                {
                    double probability = CalculateProbabilityOfMovingToNode(job);

                    if (probability > highestProbability)
                    {
                        highestProbability = probability;
                        nextJob = job;
                    }
                });

                double evaporationRate = 1;
                
                // Deposit Pheromone
                double totalDesirability = 0;
                _tabuJobs.ForEach(job => totalDesirability += CalculateDesirabilityOfMove(job));

                
                
                double pheromoneLevel = ((1 - evaporationRate) * (CalculateDesirabilityOfMove(_currentJob)) + totalDesirability; 
                
                _jobGraph.SetPheromoneBetweenTwoJobs(_currentJob.JobIndex, nextJob.JobIndex, pheromoneLevel);
                
                _currentJob = nextJob;
            }
            
            
        }

//        private Job GetNextJobBasedOnProbability()
//        {
//            float currentShortestVisibility = 0;
//            Job currentShortestJob = null;
//            
//            // Create a list of all nodes not on the Tabu List.
//            List<Job> allowedNodes = _allJobs.Except(_tabuJobs).ToList();
//            
//            allowedNodes.ForEach(node =>
//            {
//                float visibilityOfNode = GetVisibilityOfNode(node);
//                
//                if ( visibilityOfNode < currentShortestVisibility)
//                {
//                    currentShortestVisibility = visibilityOfNode;
//                    currentShortestJob = node;
//                }
//
//                int pheromoneDeposit = _jobGraph.GetPheromoneBetweenTwoJobs(_currentJob.JobIndex, node.JobIndex);
//
//                float top = pheromoneDeposit * visibilityOfNode;
//                float bottom = 
//
//            });
//            
//
//            return currentShortestJob;
//        }
        
        private float GetDesirabilityOfNode(Job node)
        {
            // Multiply by -1 to inverse the penalty.
            // Now, the smaller the number returned, the more desirable (because lower penalty is more desirable).
            return Math.Max(_timeEslasped  + node.RequiredTimeToComplete - node.DaysTillDue, 0) * -1;
        }

        private int GetPheromoneIntensityBetweenTwoNodes(Job currentNode, Job nextNode)
        {
            return _jobGraph.GetPheromoneBetweenTwoJobs(currentNode.JobIndex, nextNode.JobIndex);
        }

        private double CalculateProbabilityOfMovingToNode(Job nextJob)
        {
            double desirabilityOfMove = CalculateDesirabilityOfMove(_currentJob);
            double trailIntensity = CalculateTrailIntensity(_currentJob, nextJob);


            double totalAlternativeDesirability = 0;
            double totalAlternativeTrailIntensity = 0;
            
            List<Job> allowedJobs = GetAllowedJobs();
            allowedJobs.ForEach(job =>
            {
                totalAlternativeDesirability += CalculateDesirabilityOfMove(job);
                totalAlternativeTrailIntensity += CalculateTrailIntensity(_currentJob, job);
            });

            double probability = (desirabilityOfMove * trailIntensity) /
                                 (totalAlternativeDesirability * totalAlternativeTrailIntensity);

            return probability;
        }

        private double CalculateDesirabilityOfMove(Job currentJob)
        {
            int desirabilityOfMoveInfluence = 1;
            return Math.Pow(1 / GetDesirabilityOfNode(_currentJob), desirabilityOfMoveInfluence);
        }
        
        private double CalculateTrailIntensity(Job currentJob, Job nextJob)
        {
            int trailIntensityInfluence = 1;
            return Math.Pow( GetPheromoneIntensityBetweenTwoNodes(_currentJob, nextJob), trailIntensityInfluence);
        }

        private List<Job> GetAllowedJobs()
        {
            // Create a list of all node not on the Tabu List.
            return _allJobs.Except(_tabuJobs).ToList();
        }

    }
}