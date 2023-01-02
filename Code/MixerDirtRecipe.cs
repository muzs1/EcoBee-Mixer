//ECOBee
namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using System.Collections.Generic;

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
                    new IngredientElement(typeof(CementItem), 24,true),
                    new IngredientElement(typeof(SandItem), 47,true),
                    new IngredientElement("CrushedRock", 119,true),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<AsphaltConcreteItem>(100)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(1000);
            this.CraftMinutes = CreateCraftTimeValue(15f);
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Mixed Asphalt Concrete"), typeof(MixerAsphaltConcreteRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(MixerObject), this);
        }

        partial void ModsPreInitialize();
        
        partial void ModsPostInitialize();
    }
}
