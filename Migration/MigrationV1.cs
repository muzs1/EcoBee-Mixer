namespace Eco.Gameplay.Migrations.V0_9_7
{
    using System.Collections.Generic;
    using Eco.Core.Serialization.Migrations;
    using Eco.Core.Serialization.Migrations.Attributes;
    using Eco.Gameplay.Migrations.Common;

    [Migration(SinceVersion = 0_09.07_06_00)]
    public class MixerMigrationV1 : ClassRenameMigration
    {
        public MixerMigrationV1() : base(new Dictionary<string, string>
        {
            {"Eco.Mods.TechTree.MixerAdvancedUpgradeItem", "EcoBee.Mixer.Items.MixerAdvancedUpgradeItem" },
            {"Eco.Mods.TechTree.MixerItem", "EcoBee.Mixer.Items.MixerItem" },
            {"Eco.Mods.TechTree.MixerObject", "EcoBee.Mixer.Items.MixerObject" },
        })
        { }
    }
}