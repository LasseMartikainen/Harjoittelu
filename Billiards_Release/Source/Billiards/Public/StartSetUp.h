// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "StartSetUp.generated.h"

UCLASS()
class BILLIARDS_API AStartSetUp : public AActor
{
	GENERATED_BODY()
	
public:	
	AStartSetUp();

	//to check ball type
	UFUNCTION(BlueprintPure)
	bool isStriped(int ball);

	//array of start locations
	UFUNCTION(BlueprintPure)
	TArray<int> startLocations();

	//function to generate array of textures for different balls
	UFUNCTION(BlueprintPure)
	TArray<UMaterialInstanceConstant*> ballMaterials();

protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;

public:	
	// Called every frame
	virtual void Tick(float DeltaTime) override;

};

