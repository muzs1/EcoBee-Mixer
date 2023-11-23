using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using EcoBee.Mixer.Items;

namespace EcoBee.Mixer.Recipes
{
    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]
    public partial class MixerAsphaltConcreteRecipe : RecipeFamily
    {
        public MixerAsphaltConcreteRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "MixerAsphaltConcrete",
                Localizer.DoStr("MixerAsphaltConcrete"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(CementItem), 50, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    new IngredientElement(typeof(SandItem), 100, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                    new IngredientElement("CrushedRock",250, typeof(BasicEngineeringSkill), typeof(BasicEngineeringLavishResourcesTalent)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<AsphaltConcreteItem>(100)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2.5f;
            this.LaborInCalories = CreateLaborInCaloriesValue(1500);
            //this.CraftMinutes = CreateCraftTimeValue(23f);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(AsphaltConcreteRecipe), start: 30, skillType: typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Mixed Asphalt Concrete"), typeof(MixerAsphaltConcreteRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MixerObject), this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}