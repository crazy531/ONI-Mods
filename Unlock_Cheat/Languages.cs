using STRINGS;

namespace Unlock_Cheat
{
    public static class Languages
    {
        public class UI
        {
            // Token: 0x02000011 RID: 17
            public class USERMENUACTIONS
            {
                // Token: 0x02000012 RID: 18
                public class MUTATOR
                {
                    // Token: 0x0400005B RID: 91
                    public static LocString NAME = "变异";

                    // Token: 0x0400005C RID: 92
                    public static LocString TOOLTIP = "将种子或植物随机变异.";
                }

                // Token: 0x02000013 RID: 19
                public class IDENTIFY_MUTATION
                {
                    // Token: 0x0400005D RID: 93
                    public static LocString NAME = "分析";

                    // Token: 0x0400005E RID: 94
                    public static LocString TOOLTIP = "分析种子不需要" + STRINGS.UI.FormatAsLink("植物分析仪", "GENETICANALYSISSTATION");
                }
            }
        }
    }
}