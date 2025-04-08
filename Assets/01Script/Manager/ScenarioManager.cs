using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : SingletonDestroy<ScenarioManager>
{
    private Dictionary<int, ChatData> story;
    private int page = 0;

    public void StartStory(string typeName)
    {
        var type = StoryType.ContaminatedMushrooms;
        switch (typeName)
        {
            case "ContaminatedMushrooms": type = StoryType.ContaminatedMushrooms     ;             break;
            case "Virus                ": type = StoryType.Virus                     ;             break;
            case "MysteriousTree       ": type = StoryType.MysteriousTree            ;             break;
            case "DollClawMachine      ": type = StoryType.DollClawMachine           ;             break;
            case "RabbitDoll           ": type = StoryType.RabbitDoll                ;             break;
            case "FoodTruck            ": type = StoryType.FoodTruck                 ;             break;
            case "FallenLeaves         ": type = StoryType.FallenLeaves              ;             break;
            case "Log                  ": type = StoryType.Log                       ;             break;
            case "RumiHouse            ": type = StoryType.RumiHouse                 ;             break;
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
