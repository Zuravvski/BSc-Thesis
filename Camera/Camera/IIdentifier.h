#pragma once
#include "RobotFrame.h"

struct IIdentifier
{
	virtual ~IIdentifier() = default;
	virtual void Identify(std::vector<RobotFrame>& robots) = 0;
};
