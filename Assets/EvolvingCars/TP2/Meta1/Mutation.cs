using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Runner.UnityApp.Car;
using GeneticSharp.Domain.Randomizations;
using UnityEngine;
using System.Collections;
using System;

public class Mutation : IMutation
{
    public bool IsOrdered { get; private set; } // indicando se o operador é ordenado (se pode manter a ordem do cromossomo).

    public Mutation()
    {
        IsOrdered = true;
    }

    public void Mutate(IChromosome chromosome, float probability)
    {

        if (RandomizationProvider.Current.GetDouble() <= probability)
        {
            var castedChromosome = ((CarChromosome)chromosome);
            var config = castedChromosome.getConfig();

            int index1 = RandomizationProvider.Current.GetInt(1, castedChromosome.Length);

            int multiplication = RandomizationProvider.Current.GetInt(1, 14);

            int index2 = ((multiplication * 4) * index1) % castedChromosome.Length;

            var gene1 = castedChromosome.GetGene(index1);
            var gene2 = castedChromosome.GetGene(index2);

            castedChromosome.ReplaceGene(index1, gene2);
            castedChromosome.ReplaceGene(index2, gene1);
        }
    }
}
