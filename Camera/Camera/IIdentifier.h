#pragma once
#include "RobotFrame.h"

namespace Zuravvski
{
	struct IIdentifier
	{
		virtual ~IIdentifier() = default;
		virtual void Identify(std::vector<RobotFrame>& robots) = 0;
	};
}