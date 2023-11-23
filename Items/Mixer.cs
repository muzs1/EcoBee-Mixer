using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Items;
using Eco.Gameplay.Modules;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Skills;
using Eco.Shared.Math;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.Shared.Items;
using Eco.Gameplay.Systems.NewTooltip;
using Eco.Core.Controller;
using Eco.Mods.TechTree;
using Eco.Gameplay.Occupancy;
using Eco.Gameplay.Items.Recipes;

namespace EcoBee.Mixer.Items
{
    [Serialized]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerConsumptionComponent))]
    [RequireComponent(typeof(PluginModulesComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [Ecopedia("Work Stations", "Craft Tables", subPageName: "Mixer Item")]
    public partial class MixerObject : WorldObject, IRepresentsItem
    {
        public virtual Type RepresentedItemType => typeof(MixerItem);
        public override LocString DisplayName => Localizer.DoStr("Mixer");
        public override TableTextureMode TableTexture => TableTextureMode.Metal;

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Crafting"));
            this.GetComponent<PowerConsumptionComponent>().Initialize(1000);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.ModsPostInitialize();
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [Serialized]
    [LocDisplayName("Mixer")]
    [LocDescription("Electric mixer for speeding up road production.")]
    [Ecopedia("Work Stations", "Craft Tables", createAsSubPage: true)]
    [AllowPluginModules(Tags = new[] { "AdvancedUpgrade" }, ItemTypes = new[] { typeof(MixerAdvancedUpgradeItem) })] //noloc
    public partial class MixerItem : WorldObjectItem<MixerObject>, IPersistentData
    {
        protected override OccupancyContext GetOccupancyContext => new SideAttachedContext(0 | DirectionAxisFlags.Down, WorldObject.GetOccupancyInfo(this.WorldObjectType));

        [NewTooltip(CacheAs.SubType, 7)] public static LocString PowerConsumptionTooltip() => Localizer.Do($"Consumes: {Text.Info(1000)}w of {new ElectricPower().Name} power");
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)] public object PersistentData { get; set; }
    }

    [RequiresSkill(typeof(MechanicsSkill), 4)]
    [Ecopedia("Work Stations", "Craft Tables", subPageName: "Mixer Item")]
    public partial class MixerRecipe : RecipeFamily
    {
        public MixerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Mixer",
                displayName: Localizer.DoStr("Mixer"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(GearboxItem), 10, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement(typeof(PistonItem), 5, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                    new IngredientElement(typeof(IronBarItem), 25, typeof(MechanicsSkill), typeof(MechanicsLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<MixerItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 20;
            this.LaborInCalories = CreateLaborInCaloriesValue(600, typeof(MechanicsSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MixerRecipe), 100, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Mixer"), recipeType: typeof(MixerRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(AssemblyLineObject), recipe: this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}
