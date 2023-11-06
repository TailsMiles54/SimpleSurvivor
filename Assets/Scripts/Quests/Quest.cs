using System.Collections.Generic;

namespace Quests
{
    public class Quest
    {
        public string Id;
        public string NpcId;
        public int RequiredLevel;
        public List<QuestStep> Steps;
        public string Rewards;//todo
        public string Location;//todo
        
        public struct QuestStep
        {
            public string Task;//todo
            public int Progress;
        }
    }
}
