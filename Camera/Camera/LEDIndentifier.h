#pragma once
#include "IIdentifier.h"

namespace Zuravvski
{
	struct LEDIndentifier : IIdentifier
	{
		void Identify(std::vector<RobotFrame>& robots) override;
	};
}

