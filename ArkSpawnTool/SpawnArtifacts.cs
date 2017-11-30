using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArkSpawnTool
{
    class SpawnArtifacts : SpeechCommand
    {
        public const String SPAwN_COMMAND = "spawn artifacts";
        public static SpawnArtifacts Instance;

        static SpawnArtifacts()
        {
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(SPAwN_COMMAND);
            Instance = new SpawnArtifacts(gb);
        }

        private SpawnArtifacts(GrammarBuilder builder) : base(builder) { }

        public override void onRecognized(SpeechRecognitionEngine engine, SpeechRecognizedEventArgs e)
        {
            List<String> commands = new List<String>();

            //hunter
            commands.Add("cheat giveitemnum 148 1 0 0");

            //pack
            commands.Add("cheat giveitemnum 149 1 0 0");

            //massive
            commands.Add("cheat giveitemnum 150 1 0 0");

            //devious
            commands.Add("cheat giveitemnum 151 1 0 0");

            //clever
            commands.Add("cheat giveitemnum 152 1 0 0");

            //skylord
            commands.Add("cheat giveitemnum 153 1 0 0");

            //devourer
            commands.Add("cheat giveitemnum 154 1 0 0");

            //immune
            commands.Add("cheat giveitemnum 155 1 0 0");

            //strong
            commands.Add("cheat giveitemnum 156 1 0 0");

            //cunning
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Artifacts/PrimalItemArtifact_11.PrimalItemArtifact_11'\" 1 0 0");

            //brute
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Artifacts/PrimalItemArtifact_12.PrimalItemArtifact_12'\" 1 0 0");

            //trex arms
            commands.Add("cheat giveitemnum 160 10 0 0");

            //teeth
            commands.Add("cheat giveitemnum 159 10 0 0");

            //talon
            commands.Add("cheat giveitemnum 158 10 0 0");

            //vertebra
            commands.Add("cheat giveitemnum 161 10 0 0");

            //tentacle
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Tuso.PrimalItemResource_ApexDrop_Tuso'\" 10 0 0");

            //allo brain
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Allo.PrimalItemResource_ApexDrop_Allo'\" 10 0 0");

            //basilo blubber
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Basilo.PrimalItemResource_ApexDrop_Basilo'\" 10 0 0");

            //giga heart
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_apexdrop_giga.primalitemresource_apexdrop_giga'\" 10 0 0");

            //yuty lungs
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_apexdrop_Yuty.primalitemresource_apexdrop_yuty'\" 10 0 0");

            //mega toxins
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Megalania.PrimalItemResource_ApexDrop_Megalania'\" 10 0 0");

            //spino sail
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Spino.PrimalItemResource_ApexDrop_Spino'\" 10 0 0");

            //therizi claw
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_apexdrop_theriz.primalitemresource_apexdrop_theriz'\" 10 0 0");

            //thylaco hook claw
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_apexdrop_thylaco.primalitemresource_apexdrop_thylaco'\" 10 0 0");

            //sarcosucus skin
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Sarco.PrimalItemResource_ApexDrop_Sarco'\" 10 0");

            //titanoboa venom
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_Boa.PrimalItemResource_ApexDrop_Boa'\" 10 0 0");

            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Trophies/PrimalItemTrophy_Dragon_Alpha.PrimalItemTrophy_Dragon_Alpha'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Trophies/PrimalItemTrophy_Broodmother_Alpha.PrimalItemTrophy_Broodmother_Alpha'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Items/Trophies/PrimalItemTrophy_Gorilla_ALpha.PrimalItemTrophy_Gorilla_Alpha'\" 1 0 0");

            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaCarno.PrimalItemResource_ApexDrop_AlphaCarno'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaLeeds.PrimalItemResource_ApexDrop_AlphaLeeds'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaMegalodon.PrimalItemResource_ApexDrop_AlphaMegalodon'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaMosasaur.PrimalItemResource_ApexDrop_AlphaMosasaur'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaRaptor.PrimalItemResource_ApexDrop_AlphaRaptor'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaTuso.PrimalItemResource_ApexDrop_AlphaTuso'\" 1 0 0");
            commands.Add("cheat giveitem \"Blueprint'/Game/PrimalEarth/CoreBlueprints/Resources/PrimalItemResource_ApexDrop_AlphaRex.PrimalItemResource_ApexDrop_AlphaRex'\" 1 0 0");

            MainWindow.sendMultipleCommands(commands);
        }
    }
}