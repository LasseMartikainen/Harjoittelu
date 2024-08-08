// Fill out your copyright notice in the Description page of Project Settings.


#include "BilliardsPlayer.h"

//Function names give explanation to the function

FString BilliardsPlayer::getPlayerName() const
{
    return playerName;
}

void BilliardsPlayer::setPlayerName(FString name)
{
    playerName = name;
}

int32 BilliardsPlayer::getPottedBalls() const
{
    return pottedBalls;
}

void BilliardsPlayer::increasePottedBalls()
{
        pottedBalls++;
}

void BilliardsPlayer::setTypeOfBall(EBallType type)
{
    typeOfBall = type;
}

EBallType BilliardsPlayer::getTypeOfBall() const
{
    return typeOfBall;
}

int32 BilliardsPlayer::getFinalPot() const
{
    return finalPot;
}

void BilliardsPlayer::setFinalPot(int pot)
{
    finalPot = pot;
}

void BilliardsPlayer::endTurn()
{
    hasTurn = false;
}

void BilliardsPlayer::startTurn()
{
    hasTurn = true;
}

bool BilliardsPlayer::checkTurn() const
{
    return hasTurn;
}

void BilliardsPlayer::winner()
{
    isWinner = true;
}

BilliardsPlayer::BilliardsPlayer()
{
}

BilliardsPlayer::~BilliardsPlayer()
{
}
