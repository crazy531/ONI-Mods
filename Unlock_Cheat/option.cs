using System;
using Newtonsoft.Json;
using PeterHan.PLib.Options;
using KMod;
using Klei;

namespace Unlock_Cheat
{

    [JsonObject(MemberSerialization.OptIn)]
    [ModInfo("", null, false)]
    [ConfigFile("config.json", true, true)]
    [RestartRequired]
    internal  sealed class Options : SingletonOptions<Options>
    {

        [JsonProperty]
        [Option("成就解锁", "debug 沙盒能解锁成就.", null)]
        public bool Achievement { get; set; }

        [JsonProperty]
        [Option("皮肤解锁", "解锁全皮肤.", null)]
        public bool Skin { get; set; }

        [JsonProperty]
        [Option("管道相变", "禁止气液管道内容物相变.", null)]
        public bool Conduit { get; set; }


        public Options()
        {
            this.Achievement = true;
            this.Skin = true;

            this.Conduit = true;

        }
    }
}
