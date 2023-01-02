//ECOBee
namespace Eco.Mods.TechTree
{
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using System;
    using System.Collections.Generic;

    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    public partial class MixerObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(MixerItem);
        public override LocString DisplayName => Localizer.DoStr("Mixer");
        public override TableTextureMode TableTexture => TableTextureMode.Wood;

        protected override void Initialize()
        {
            this.GetComponent<PowerConsumptionComponent>().Initialize(1000);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.ModsPreInitialize();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Crafting"));
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Mixer")]
    [Ecopedia("Work Stations", "Craft Tables", createAsSubPage: true)]
    public partial class MixerItem : WorldObjectItem<MixerObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("Electric mixer for speeding up road production.");

        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0
                    | DirectionAxisFlags.Down
                ;
    }

    [RequiresSkill(typeof(MechanicsSkill), 4)]
    public partial class MixerRecipe : RecipeFamily
    {
        public MixerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "Mixer",
                Localizer.DoStr("Mixer"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(GearboxItem), 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement(typeof(PistonItem), 5, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement(typeof(IronBarItem), 25, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<MixerItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(MechanicsSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MixerRecipe), 100, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Mixer"), typeof(MixerRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }

        partial void ModsPreInitialize();
        
        partial void ModsPostInitialize();
    }
}
