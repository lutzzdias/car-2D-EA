using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Infrastructure.Framework.Texts;
using GeneticSharp.Runner.UnityApp.Car;
using UnityEngine;

public class ParentSelection : SelectionBase
{
    private static int tournamentSize = 2;

    public ParentSelection() : base(2)
    {
    }

    protected override IList<IChromosome> PerformSelectChromosomes(int number, Generation generation)
    {
        IList<CarChromosome> population = generation.Chromosomes.Cast<CarChromosome>().ToList(); // Current Population: We will select individuals from here
        IList<IChromosome> parents = new List<IChromosome>(); //List that will return the individuals that will mate, i.e. that will undergo variation

        // Tournament parent selection
        for (int i = 0; i < number; i++){
            // Get random sample of individuals from the population
            List<CarChromosome> pool = new List<CarChromosome>();
            IList<int> randomIndexes = RandomizationProvider.Current.GetUniqueInts(tournamentSize, 0, population.Count).ToList();

            foreach (int index in randomIndexes) {
                pool.Add(population[index]);
            }

            // Select the best individual from the sample
            var individual = pool.OrderByDescending(p => p.Fitness).First();
            parents.Add(individual);
        }

        return parents;
    }
}
