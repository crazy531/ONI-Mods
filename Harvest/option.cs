using System;
using Newtonsoft.Json;
using PeterHan.PLib.Options;
using KMod;
using Klei;

namespace crazyxyr_mod
{

    [JsonObject(MemberSerialization.OptIn)]
    [ModInfo("", null, false)]
    [ConfigFile("config.json", true, true)]
    [RestartRequired]
    internal class options : SingletonOptions<options>
    {

        [JsonProperty]
        [Option("钻头前锥容量", "NoseconeHarvest.", null)]
        public bool NoseconeHarvest { get; set; }


        public options()
        {
            this.NoseconeHarvest = false;
        }
    }
}
