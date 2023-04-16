using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using EcoBee.Mixer.Items;

namespace EcoBee.Mixer.Recipes
{
    public partial class DirtRecipe : RecipeFamily
    {
        public DirtRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Dirt",
                Localizer.DoStr("Dirt"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(CompostItem), 15,true),
                    new IngredientElement("CrushedRock", 5,true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<DirtItem>(10)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(10);
            //this.CraftMinutes = CreateCraftTimeValue(0.5f);
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(DirtRecipe), start: 1, skillType: typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Dirt"), typeof(DirtRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MixerObject), this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
