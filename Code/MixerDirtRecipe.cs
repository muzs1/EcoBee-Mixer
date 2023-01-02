//ECOBee
namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Shared.Localization;
    using System.Collections.Generic;

    public partial class DirtRecipe : RecipeFamily
    {
        public DirtRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Dirt Recipe",
                Localizer.DoStr("Dirt Recipe"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(CompostItem), 7,true),
                    new IngredientElement(typeof(SandItem), 1,true),
                    new IngredientElement(typeof(ClayItem), 1,true),
                    new IngredientElement("CrushedRock", 1,true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<DirtItem>(10)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(10);
            this.CraftMinutes = CreateCraftTimeValue(0.5f);
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Dirt Recipe"), typeof(DirtRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MixerObject), this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
