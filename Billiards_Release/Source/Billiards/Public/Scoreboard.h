// Fill out your copyright notice in the Description page of Project Settings.

#pragma once


#include "CoreMinimal.h"
#include "GameFramework/HUD.h"

#include "Components/WidgetComponent.h"
#include "ScoreWidget.h"

#include "Scoreboard.generated.h"

/**
 * 
 */
UCLASS()
class BILLIARDS_API AScoreboard : public AHUD
{
	GENERATED_BODY()


public:
	AScoreboard();

	virtual void DrawHUD() override;

	virtual void BeginPlay() override;

	virtual void Tick(float DeltaSeconds) override;


	UPROPERTY(EditDefaultsOnly, Category = "Widgets")
	TSubclassOf<UUserWidget> ScoreWidgetClass;

private:
	UScoreWidget* ScoreWidget;
};
