using System;
using System.Collections.Generic;

public class CharacterQuests
{
    private List<CharacterQuest> _currentQuests;
    private List<string> _completedQuests;

    public void AddQuest(string questId)
    {
        _currentQuests.Add(new CharacterQuest()
        {
            QuestId = questId,
            CurrentStep = 1,
            CurrentProgress = 0
        });
    }

    public void CompleteQuest(string questId)
    {
        _completedQuests.Add(questId);
    }

    [Serializable]
    public class CharacterQuest
    {
        public string QuestId;
        public int CurrentStep;
        public int CurrentProgress;
    }
}