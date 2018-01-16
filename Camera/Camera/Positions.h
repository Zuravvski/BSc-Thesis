#pragma once
#include <vector>
#include "Position.h"

using namespace nlohmann;

namespace Zuravvski
{
	struct Positions
	{
		std::vector<Positions> positions;
	};
}
