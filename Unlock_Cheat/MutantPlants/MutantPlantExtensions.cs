using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unlock_Cheat.MutantPlants
{
    internal static class MutantPlantExtensions
    {
        private static void DiscoverSilentlyAndIdentifySubSpecies(PlantSubSpeciesCatalog.SubSpeciesInfo speciesInfo)
        {
            List<PlantSubSpeciesCatalog.SubSpeciesInfo> allSubSpeciesForSpecies = PlantSubSpeciesCatalog.Instance.GetAllSubSpeciesForSpecies(speciesInfo.speciesID);
            if (allSubSpeciesForSpecies != null && !allSubSpeciesForSpecies.Contains(speciesInfo))
            {
                allSubSpeciesForSpecies.Add(speciesInfo);
                PlantSubSpeciesCatalog.Instance.IdentifySubSpecies(speciesInfo.ID);
                SaveGame.Instance.ColonyAchievementTracker.LogAnalyzedSeed(speciesInfo.speciesID);
            }
        }

        internal static void Mutator(this MutantPlant mutant)
        {
            if (mutant != null)
            {
                mutant.Mutate();
                mutant.ApplyMutations();
                mutant.AddTag(GameTags.MutatedSeed);
                if (mutant.HasTag(GameTags.Plant))
                {
                    MutantPlantExtensions.DiscoverSilentlyAndIdentifySubSpecies(mutant.GetSubSpeciesInfo());
                }
                else
                {
                    PlantSubSpeciesCatalog.Instance.DiscoverSubSpecies(mutant.GetSubSpeciesInfo(), mutant);
                }
                PlantBranchGrower.Instance smi = mutant.GetSMI<PlantBranchGrower.Instance>();
                if (!smi.IsNullOrStopped())
                {
                    smi.ActionPerBranch(delegate (GameObject go)
                    {
                        MutantPlant mutantPlant;
                        if (go.TryGetComponent<MutantPlant>(out mutantPlant))
                        {
                            mutant.CopyMutationsTo(mutantPlant);
                            mutantPlant.ApplyMutations();
                            MutantPlantExtensions.DiscoverSilentlyAndIdentifySubSpecies(mutantPlant.GetSubSpeciesInfo());
                        }
                    });
                }
                DetailsScreen.Instance.Trigger(-1514841199, null);
            }
        }

        internal static void IdentifyMutation(this MutantPlant mutant)
        {
            if (mutant != null)
            {
                mutant.Analyze();

                PlantSubSpeciesCatalog.Instance.IdentifySubSpecies(mutant.SubSpeciesID);
                SaveGame.Instance.ColonyAchievementTracker.LogAnalyzedSeed(mutant.SpeciesID);

                DetailsScreen.Instance.Trigger(-1514841199, null);
            }
        }
    }

}
