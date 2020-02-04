﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario_Event : Scenario {

    public override void OnCloseScenario() {
        if(scenarioName == "Day12_NewIngredient") {
            IngredientManager IM = IngredientManager.instance;
            IM.meatDic[404].IsAvailable = true;
            IM.meatDic[405].IsAvailable = true;
            IM.meatDic[406].IsAvailable = true;

            IM.meatDic[404].Count++;
            IM.meatDic[405].Count++;
            IM.meatDic[406].Count++;
        }
        else if (scenarioName == "Day30_NewIngredient") {
            IngredientManager IM = IngredientManager.instance;
            IM.powderDic[504].IsAvailable = true;
            IM.powderDic[505].IsAvailable = true;
            IM.powderDic[506].IsAvailable = true;
            IM.sauceDic[604].IsAvailable = true;
            IM.sauceDic[605].IsAvailable = true;
            IM.sauceDic[606].IsAvailable = true;

            IM.powderDic[504].Count++;
            IM.powderDic[505].Count++;
            IM.powderDic[506].Count++;
            IM.sauceDic[604].Count++;
            IM.sauceDic[605].Count++;
            IM.sauceDic[606].Count++;
        }
    }
}