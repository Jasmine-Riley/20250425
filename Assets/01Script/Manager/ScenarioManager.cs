using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : SingletonDestroy<ScenarioManager>
{
    private Dictionary<int, ChatData> story;
    private int page = 0;

    public void StartStory(string typeName)
    {
        var type = StoryType.Story;
        switch (typeName)
        {
            case "Story":
                type = StoryType.Story;
                break;
            case "FindDevil":
                type = StoryType.FindDevil;
                break;
            default:
                break;
        }

        StartStory(type);
    }

    public void StartStory(StoryType type)
    {
        story = DataManager.Instance.GetStoryData(type);
        page = 0;

        var imgSprite = Resources.Load<Sprite>(story[page].imgSrc);
        UIManager.Instance.SetScenarioPannel(imgSprite, story[page].name, story[page].chat);
        UIManager.Instance.OpenScenarioPannel();
    }

    public void StopStory()
    {
        story = null;
        page = 0;
        UIManager.Instance.CloseScenarioPannel();
    }

    public void NextPage()
    {
        if (page + 1 < story.Count)
        {
            page++;
            var imgSprite = Resources.Load<Sprite>(story[page].imgSrc);

            UIManager.Instance.SetScenarioPannel(imgSprite, story[page].name, story[page].chat);
        }
        else
            StopStory();

    }
}
