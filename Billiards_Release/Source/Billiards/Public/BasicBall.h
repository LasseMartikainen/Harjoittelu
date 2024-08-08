// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "GameFramework/Actor.h"
#include "BasicBall.generated.h"


UCLASS()
class BILLIARDS_API ABasicBall : public AActor
{
	GENERATED_BODY()

public:

	// Sets default values for this actor's properties
	UPROPERTY(BlueprintReadWrite) //Can be edited in blueprint
	UStaticMeshComponent* SM_Ball;

	UPROPERTY(BlueprintReadWrite, Category = "Variables")
	int32 BallNumber = 0;

	//Functions to determine ball number and category
	UFUNCTION(BlueprintPure, Category = "Set")
	int32 SetBallNumber(int NewValue);

	UFUNCTION(BlueprintPure, Category = "Get")
	int32 GetBallNumber() const;

	UFUNCTION(BlueprintPure)
	bool isSolid(int32 BallNbr) const;

	UFUNCTION(BlueprintPure)
	bool isStriped(int32 BallNbr) const;

	


protected:
	// Called when the game starts or when spawned
	virtual void BeginPlay() override;
	virtual void PostInitializeComponents() override;

public:
	// Called every frame
	ABasicBall();
	virtual void Tick(float DeltaTime) override;

};
