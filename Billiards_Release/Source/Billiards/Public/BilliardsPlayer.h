// Fill out your copyright notice in the Description page of Project Settings.

#pragma once

#include "CoreMinimal.h"
#include "UObject/NoExportTypes.h"

UENUM(BlueprintType)
enum class EBallType : uint8 { black, white, solid, striped, unknown };
/**
 * 
 */
class BILLIARDS_API BilliardsPlayer
{

private:
    //set up default properties
    UPROPERTY(BlueprintReadWrite, Category = "Variables")
    FString playerName = "Name"; //for keeping track who is who

    UPROPERTY(BlueprintReadWrite, Category = "Variables")
    EBallType typeOfBall = EBallType::unknown; //gets updated when first ball is potted
    
    UPROPERTY(BlueprintReadWrite, Category = "Variables")
    int32 pottedBalls = 0; //for counting

    UPROPERTY(BlueprintReadWrite, Category = "Variables")
    int32 finalPot = 0; //here is determined the pot where black-8 must be potted finally
    
    UPROPERTY(BlueprintReadWrite, Category = "Variables")
    bool hasTurn = false; //to check whose turn is it

public:
    //this property is public for easier check
    UPROPERTY(BlueprintReadWrite)
    bool isWinner = false;

    //functions for checking or manipulating properties
    UFUNCTION(BlueprintPure, Category = "Get")
    FString getPlayerName() const;

    UFUNCTION(BlueprintPure, Category = "Set")
    void setPlayerName(FString name);

    UFUNCTION(BlueprintPure, Category = "Get")
    int32 getPottedBalls() const;

    UFUNCTION(BlueprintPure)
    void increasePottedBalls();

    UFUNCTION(BlueprintPure, Category = "Set")
    void setTypeOfBall(EBallType type);

    UFUNCTION(BlueprintPure, Category = "Get")
    EBallType getTypeOfBall() const;

    UFUNCTION(BlueprintPure, Category = "Get")
    int32 getFinalPot() const;

    UFUNCTION(BlueprintPure, Category = "Set")
    void setFinalPot(int pot);

    UFUNCTION(BlueprintPure)
    void endTurn();

    UFUNCTION(BlueprintPure)
    void startTurn();

    UFUNCTION(BlueprintPure)
    bool checkTurn() const;

    UFUNCTION(BlueprintPure)
    void winner();

	BilliardsPlayer();
	~BilliardsPlayer();
};
