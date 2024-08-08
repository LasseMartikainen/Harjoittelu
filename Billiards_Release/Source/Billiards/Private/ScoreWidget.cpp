// Fill out your copyright notice in the Description page of Project Settings.


#include "ScoreWidget.h"

UScoreWidget::UScoreWidget(const FObjectInitializer& ObjectInitializer) : Super(ObjectInitializer)
{
    //player setup
    Player1->startTurn();

    //names should be made changeable in game start
    Player1->setPlayerName("Player 1");
    Player2->setPlayerName("Player 2");
}

void UScoreWidget::NativeConstruct()
{
	Super::NativeConstruct();
}

void UScoreWidget::UpdatePlayerInfo()
{
    ////Check for win
    //if (WinnerInfo)
    //{
    //    if (Player1->isWinner)
    //    {
    //        WinnerInfo->SetText(FText::FromString(
    //            Player1->getPlayerName() + " voitti!"
    //        ));
    //    }

    //    if (Player2->isWinner)
    //    {
    //        WinnerInfo->SetText(FText::FromString(
    //            Player2->getPlayerName() + " voitti!"
    //        ));
    //    }
    //}

    //Check for win using lambda function
    auto updateWinnerInfo = [this](BilliardsPlayer* player) {
        if (player->isWinner) {
            WinnerInfo->SetText(FText::FromString(player->getPlayerName() + " voitti!"));
        }
        };

    updateWinnerInfo(Player1);
    updateWinnerInfo(Player2);

    //display first player information
	if (Player1Info)
	{
        //if it's not your turn the info is dimmer
        if (!Player1->checkTurn())
        {
            Player1Info->SetOpacity(0.25);
        }
        else
        {
            Player1Info->SetOpacity(1);
        }

		Player1Info->SetText(FText::FromString(
			"Nimi: " + Player1->getPlayerName() + "\n" +
			"Pallot: " + UEnum::GetValueAsString(Player1->getTypeOfBall()).RightChop(11) + "\n" +
			"Pussitettu: " + FString::FromInt(Player1->getPottedBalls())
		));
	}

    //display second player information
	if (Player2Info)
	{
        if (!Player2->checkTurn())
        {
            Player2Info->SetOpacity(0.25);
        }
        else
        {
            Player2Info->SetOpacity(1);
        }

		Player2Info->SetText(FText::FromString(
			"Nimi: " + Player2->getPlayerName() + "\n" +
			"Pallot: " + UEnum::GetValueAsString(Player2->getTypeOfBall()).RightChop(11) + "\n" +
			"Pussitettu: " + FString::FromInt(Player2->getPottedBalls())
		));
	}
}

//called when a ball generates overlap event with pot trigger capsule
void UScoreWidget::potting(int32 ball, int32 pot)
{
    EBallType type = checkType(ball);
    BilliardsPlayer* player;
    BilliardsPlayer* otherPlayer;

    //lets see who's turn is it
    if (Player1->checkTurn())
    {
        player = Player1;
        otherPlayer = Player2;
    }
    else
    {
        player = Player2;
        otherPlayer = Player1;
    }

    //couldn't get white ball potting logic to work properly
    if (type == EBallType::white)
    {
/*        TArray<AActor*> FoundActors;
        UGameplayStatics::GetAllActorsOfClass(GetWorld(), ABP_Cue::StaticClass(), FoundActors);
        for (AActor* FoundActor : FoundActors)
        {
            ABP_Cue* ActorInstance = Cast<ABP_Cue>(FoundActor);
            if (ActorInstance)
            {
                ActorInstance->HandBall = true;
            }
}        */
        player->endTurn();
        otherPlayer->startTurn();
    }

    //game ends when black ball is potted
    else if (type == EBallType::black)
    {

        if (player->getTypeOfBall() == EBallType::unknown)
        {
            //this restarts the level
            FString currentLevelName = UGameplayStatics::GetCurrentLevelName(GetWorld(), true);
            UGameplayStatics::OpenLevel(GetWorld(), FName(*currentLevelName));
        }

        else if (player->getPottedBalls() == 7 && player->getFinalPot() == pot) //the winning conditions
        {
            player->winner();
        }
        else
        {
            otherPlayer->winner(); //sudden death
        }

    }

    else if (player->getTypeOfBall() == EBallType::unknown) //if the TypeOfBall is not yet determined
    {
        switch (type)
        {
        case EBallType::striped:
            player->setTypeOfBall(EBallType::striped);
            otherPlayer->setTypeOfBall(EBallType::solid);
            player->increasePottedBalls();
            break;
        case EBallType::solid:
            player->setTypeOfBall(EBallType::solid);
            otherPlayer->setTypeOfBall(EBallType::striped);
            player->increasePottedBalls();
            break;
        default:
            break;
        }
    }
    else
    {
        if (type == player->getTypeOfBall())
        {
            player->increasePottedBalls(); //if the types match the player can continue
            if (player->getPottedBalls() == 7)
            {
                player->setFinalPot(pot);
            }
        }
        else
        {
            //otherwise the turn ends and the other player gets points
            player->endTurn();
            otherPlayer->startTurn();
            otherPlayer->increasePottedBalls();
            if (otherPlayer->getPottedBalls() == 7)
            {
                otherPlayer->setFinalPot(pot);
            }
        }
    }

}


//Function for checking what type the ball is
EBallType UScoreWidget::checkType(int32 ball)
{
    if (ball == 8) return EBallType::black;

    else if (9 <= ball && ball <= 15) return EBallType::striped;
    else if (1 <= ball && ball <= 7) return EBallType::solid;
    else return EBallType::white;
}

