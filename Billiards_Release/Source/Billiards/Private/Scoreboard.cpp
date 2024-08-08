// Fill out your copyright notice in the Description page of Project Settings.


#include "Scoreboard.h"

AScoreboard::AScoreboard()
{
}

void AScoreboard::DrawHUD()
{
	Super::DrawHUD();
}

void AScoreboard::BeginPlay()
{
	Super::BeginPlay();

	//Here we display info from the ScoreWidget
	if (ScoreWidgetClass)
	{
		ScoreWidget = CreateWidget<UScoreWidget>(GetWorld(), ScoreWidgetClass);
		if (ScoreWidget)
		{
			ScoreWidget->AddToViewport();
		}
	}
}

void AScoreboard::Tick(float DeltaSeconds)
{
	Super::Tick(DeltaSeconds);

	//For updating information on ScoreWidget
	if (ScoreWidget)
	{
		ScoreWidget->UpdatePlayerInfo();
	}
}


