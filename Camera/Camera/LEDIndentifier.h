#pragma once
#include "IIdentifier.h"

struct LEDIndentifier : IIdentifier
{
	void Identify(std::vector<RobotFrame>& robots) override;
};

