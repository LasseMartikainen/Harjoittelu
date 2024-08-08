// Fill out your copyright notice in the Description page of Project Settings.


#include "BasicBall.h"

// Sets default values
ABasicBall::ABasicBall()
{
	// Set this actor to call Tick() every frame.  You can turn this off to improve performance if you don't need it.
	PrimaryActorTick.bCanEverTick = true;

	//Here we give the ball a shape
	SM_Ball = CreateDefaultSubobject<UStaticMeshComponent>(TEXT("SM_Ball"));
	UStaticMesh* ballMesh = Cast<UStaticMesh>(StaticLoadObject(UStaticMesh::StaticClass(), NULL, TEXT("/Script/Engine.StaticMesh'/Game/BilliardsAssets/SM_Ball.SM_Ball'")));
	if (ballMesh)
	{
		SM_Ball->SetStaticMesh(ballMesh);
	}
	RootComponent = SM_Ball;

}

// Called when the game starts or when spawned
void ABasicBall::BeginPlay()
{
	Super::BeginPlay();
}

// After start we give the ball mass and physics
void ABasicBall::PostInitializeComponents()
{
	Super::PostInitializeComponents();

	//set up physics and put these available in the editor
	SM_Ball->SetSimulatePhysics(true);
	SM_Ball->SetMassOverrideInKg(NAME_None, 0.17);
	SM_Ball->SetLinearDamping(0.01); //friction
	SM_Ball->SetAngularDamping(0.35); //stops rotation faster

}

// Called every frame
void ABasicBall::Tick(float DeltaTime)
{
	Super::Tick(DeltaTime);
}

// A function to set the number of the ball
int ABasicBall::SetBallNumber(int32 NewValue)
{
	BallNumber = NewValue;
	return BallNumber;
}

// A function to get the number of the ball
int ABasicBall::GetBallNumber() const
{
	return BallNumber;
}

/// <summary>
/// This checks if the ball is of single color
/// </summary>
/// <param name="ball"></param>
/// <returns></returns>
bool ABasicBall::isSolid(int32 BallNbr) const
{
	if (1 <= BallNumber && BallNumber <= 7) return true;
	else return false;
}

/// <summary>
/// This checks if the ball is of striped colors
/// </summary>
/// <param name="ball"></param>
/// <returns></returns>
bool ABasicBall::isStriped(int32 BallNbr) const
{
	if (9 <= BallNumber && BallNumber <= 15) return true;
	else return false;
}