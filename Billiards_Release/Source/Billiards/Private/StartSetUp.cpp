// Fill out your copyright notice in the Description page of Project Settings.


#include "StartSetUp.h"
#include <stdlib.h>
#include <time.h>
#include <SlateRHIRenderer/Private/SlateMaterialResource.h>
#include "Engine/Engine.h"
#include "Materials/MaterialInstanceConstant.h"

//using namespace std;

// Sets default values
AStartSetUp::AStartSetUp()
{
 	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

}


// Called when the game starts or when spawned
void AStartSetUp::BeginPlay()
{
	Super::BeginPlay();
	
}

// Called every frame
void AStartSetUp::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);

}

/// <summary>
/// This checks if the ball is of striped colors
/// </summary>
/// <param name="ball"></param>
/// <returns></returns>
bool AStartSetUp::isStriped(int ball)
{
    if (9 <= ball && ball <= 15) return true;
    else return false;
}


/// <summary>
/// This allocates starting positions in triangle for the balls, 1 = tip towards the cue ball
/// </summary>
/// <param name="balls"></param>
/// <returns></returns>
TArray<int> AStartSetUp::startLocations()
{
    srand(time(nullptr)); //random seed
    short randValue = rand();

    //list of balls in random order
    TArray<int> balls;
    while (balls.Num() < 15)
    {
        int newBall = rand() % 15 + 1;
        balls.AddUnique(newBall);
    }
          
    //8-ball must be in the middle
    for (int i = 0; i != balls.Num(); ++i)
    {
        if (balls[i] == 8)
        {
            balls[i] = balls[4];
            balls[4] = 8;
        }
    }

    //low corner balls must be of different style
    while (isStriped(balls[10]) == isStriped(balls[14]))
    {
        for (int i = 0; i < 15; i++)
        {
            if (balls[i] != 8)
            {
                int tmp = balls[i];
                balls[i] = balls[10];
                balls[10] = tmp;
            }
        }
    }

    return balls;
}


/// <summary>
/// Create array of ball materials to blueprint use
/// </summary>
/// <returns></returns>
TArray<UMaterialInstanceConstant*> AStartSetUp::ballMaterials()
{
    TArray<UMaterialInstanceConstant*> materials;

    for (int i = 1; i <= 15; i++)
    {

        // Path to the Material Instance in the Content Browser
        FString materialPath = FString::Printf(TEXT("/Game/BilliardsAssets/M_ball_Inst%d.M_ball_Inst%d"), i, i);

        // Load the Material Instance
        UMaterialInstanceConstant* materialInstance = Cast<UMaterialInstanceConstant>(StaticLoadObject(UMaterialInstanceConstant::StaticClass(), NULL, *materialPath));

        if (materialInstance)
        {
            // Material Instance successfully fetched
            materials.Add(materialInstance);
        }
    }

    return materials;
}
