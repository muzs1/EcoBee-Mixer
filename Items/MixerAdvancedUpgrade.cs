using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

namespace EcoBee.Mixer.Items
{
    [RequiresSkill(typeof(BasicEngineeringSkill), 7)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", subPageName: "MixerAdvancedUpgradeItem")]
    public partial class MixerAdvancedUpgradeRecipe : RecipeFamily
    {
        public MixerAdvancedUpgradeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "MixerAdvancedUpgrade",  //noloc
                displayName: Localizer.DoStr("Mixer Advanced Upgrade"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(AdvancedUpgradeLvl4Item), 1, true),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<MixerAdvancedUpgradeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4; // Defines how much experience is gained when crafted.

            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(6000, typeof(BasicEngineeringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(MixerAdvancedUpgradeRecipe), start: 15, skillType: typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Mixer Advanced Upgrade"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Mixer Advanced Upgrade"), recipeType: typeof(MixerAdvancedUpgradeRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(MixerObject), recipe: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Mixer Advanced Upgrade")]
    [Weight(1)]
    [Ecopedia("Upgrade Modules", "Specialty Upgrades", createAsSubPage: true)]                                                                      //_If_EcopediaPage_
    [Tag("Upgrade", 1)]
    public partial class MixerAdvancedUpgradeItem :
        EfficiencyModule
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Advanced Upgrade that greatly increases efficiency when crafting Upgrade recipes."); } }

        public MixerAdvancedUpgradeItem() : base(
            ModuleTypes.ResourceEfficiency | ModuleTypes.SpeedEfficiency,
            0.5f + 0.05f,
            typeof(BasicEngineeringSkill),
            0.5f
        )
        { }
    }
}
