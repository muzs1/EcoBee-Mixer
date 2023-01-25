//ECOBee
namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using System.Collections.Generic;

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]
    public partial class MixerStoneRoadRecipe : RecipeFamily
    {
        public MixerStoneRoadRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "MixerStoneRoad",
                Localizer.DoStr("MixerStoneRoad"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(MortarItem), 300, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    new IngredientElement("CrushedRock", 200, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<StoneRoadItem>(100)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(1500);
            this.CraftMinutes = CreateCraftTimeValue(23f);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(StoneRoadRecipe), start: 30, skillType: typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Mixed Stone Road"), typeof(MixerStoneRoadRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MixerObject), this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}