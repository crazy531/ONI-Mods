using System.Collections.Generic;

using UnityEngine;
using Klei.AI;

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

            List<string> strings = new List<string> { };
            if (mutant != null)
            {

                //mutant.Mutate();
                strings.Add(Db.Get().PlantMutations.GetRandomMutation(mutant.PrefabID().Name).Id);
                if (mutant.MutationIDs != null && mutant.MutationIDs.Contains("SelfHarvest"))
                { strings.Add("SelfHarvest"); }
                mutant.SetSubSpecies(strings);

                ApplyMutator(mutant);
            }
        }

        internal static void Mutator(this MutantPlant mutant, List<string> mutationIDs)
        {
            if (mutant != null)
            {

                mutant.SetSubSpecies(mutationIDs);
                ApplyMutator(mutant);
            }
        }
        internal static void SelfHarvest(this MutantPlant mutant)
        {


            List<string> strings = null;
            if (mutant != null)
            {
              

                if (mutant.MutationIDs != null && mutant.MutationIDs.Contains("SelfHarvest"))
                {
                    strings = mutant.MutationIDs;
                    strings.Remove("SelfHarvest");
                    mutant.SetSubSpecies(strings);
                    Attributes attributes = mutant.GetAttributes();
                    attributes.Remove(new AttributeModifier(Db.Get().Amounts.OldAge.maxAttribute.Id, -0.999999f, Strings.Get(new StringKey("STRINGS.CREATURES.PLANT_MUTATIONS." + "heavyFruit".ToUpper() + ".NAME")), true, false, true));
                 //   Debug.Log("关闭自动收货");

                }
                else
                {

                    strings = mutant.MutationIDs ?? new List<string> { };
                    strings.Add("SelfHarvest");
                    mutant.SetSubSpecies(strings);

                    Db.Get().PlantMutations.Get("SelfHarvest").ApplyTo(mutant);
                    mutant.IdentifyMutation();
                //    Debug.Log("启用自动收货");


                }


              
            }
          
        }
        internal static void ApplyMutator( MutantPlant mutant)
        {
            if (mutant != null)
            {

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
