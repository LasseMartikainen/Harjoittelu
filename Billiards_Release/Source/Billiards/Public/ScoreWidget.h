// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include <BilliardsPlayer.h>
#include "CoreMinimal.h"
#include "Blueprint/UserWidget.h"
#include "Runtime/UMG/Public/UMG.h"
#include "Kismet/GameplayStatics.h"
#include "ScoreWidget.generated.h"


/**
 * 
 */
UCLASS()
class BILLIARDS_API UScoreWidget : public UUserWidget
{
	GENERATED_BODY()
	
public:
	UScoreWidget(const FObjectInitializer& ObjectInitializer);

	virtual void NativeConstruct() override;

	void UpdatePlayerInfo();

	UPROPERTY(EditAnywhere, BlueprintReadWrite, meta = (BindWidget))
	class UTextBlock* Player1Info;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, meta = (BindWidget))
	class UTextBlock* Player2Info;

	UPROPERTY(EditAnywhere, BlueprintReadWrite, meta = (BindWidget))
	class UTextBlock* WinnerInfo;

	BilliardsPlayer* Player1 = new BilliardsPlayer();
	BilliardsPlayer* Player2 = new BilliardsPlayer();

	UFUNCTION(BlueprintCallable)
	void potting(int32 ball, int32 pot);

	UFUNCTION()
	EBallType checkType(int32 ball);

};
