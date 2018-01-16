using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArkSpawnTool
{
    class PrepareRockwell : SpeechCommand
    {
        public const String SPAwN_COMMAND = "prepare rockwell";
        public static PrepareRockwell Instance;

        static PrepareRockwell()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(SPAwN_COMMAND);
            Instance = new PrepareRockwell(gb);
        }

        private PrepareRockwell(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            List<String> commands = new List<String> {
                //artifact of the stalker
                "admincheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Artifacts/PrimalItemArtifactAB_3.PrimalItemArtifactAB_3'\" 1 0 0",
                //artifact of the depths
                "admincheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Artifacts/PrimalItemArtifactAB.PrimalItemArtifactAB'\" 1 0 0",
                //Artifact of the Shadows
                "admincheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Artifacts/PrimalItemArtifactAB_2.PrimalItemArtifactAB_2'\" 1 0 0",
                //basil scale
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Basilisk.PrimalItemResource_ApexDrop_Basilisk'\" 8 0 0",
                //venom
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemConsumable_NamelessVenom.PrimalItemConsumable_NamelessVenom'\" 20 0 0",
                //pheromone
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_XenomorphPheromoneGland.PrimalItemResource_XenomorphPheromoneGland'\" 7 0 0",
                //rockdrake feather
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_RockDrake.PrimalItemResource_ApexDrop_RockDrake'\" 7 0 0",
                //alpha king barb
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_ReaperBarb.PrimalItemResource_ApexDrop_ReaperBarb'\" 1 0 0",
                //alpha crab claw
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_CrabClaw.PrimalItemResource_ApexDrop_CrabClaw'\" 1 0 0",
                //alpha basil fang
                "admincheat giveitem \"Blueprint'/Game/Aberration/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Basilisk_Alpha.PrimalItemResource_ApexDrop_Basilisk_Alpha'\" 1 0 0"
            };

            MainWindow.sendMultipleCommands(commands);
        }
    }
}